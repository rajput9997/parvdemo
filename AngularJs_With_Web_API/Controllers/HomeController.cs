using AngularJs_With_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace AngularJs_With_Web_API.Controllers
{
    public class HomeController : Controller
    {

        private static string Username = ConfigurationManager.AppSettings["pbiUsername"];
        private static string Password = ConfigurationManager.AppSettings["pbiPassword"];
        private static readonly string AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];
        private static readonly string ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];
        private static readonly string ApplicationId = ConfigurationManager.AppSettings["ApplicationId"];
        private static readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
        private static readonly string WorkspaceId = ConfigurationManager.AppSettings["workspaceId"];
        private static readonly string ReportId = ConfigurationManager.AppSettings["reportId"];

        const string CLIENTID = "2a21b39e-8299-4a44-9a04-f99adc58bbbf";              // move this to app.config
        const string CLIENTSECRET = "y4my5SIkFw0ttzYvr6ngeSpKlTmInj0GzZsGmJgpH8I=";      // move this to app.config
        const string SECRETUsername = "https://powerappkeycred.vault.azure.net/secrets/ReportUserName";  // move this to app.config
        const string SECRETPassword = "https://powerappkeycred.vault.azure.net/secrets/ReportPassword";  // move this to app.config

        //
        // GET: /Home/
        [AuthorizationFilter]
        public async Task<ActionResult> Index()
        {
           /* KeyVaultClient kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            var secret = kvc.GetSecretAsync(SECRETUsername);
            Username = secret.Result.Value;

            var secretpwd = kvc.GetSecretAsync(SECRETPassword);
            Password = secretpwd.Result.Value; */

            // var result = new EmbedConfig();
            var result = await EmbedReport(string.Empty, string.Empty);
            return View(result);
        }

        // thanks to_ https://docs.microsoft.com/en-us/azure/key-vault/key-vault-use-from-web-application
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(CLIENTID, CLIENTSECRET);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        public async Task<EmbedConfig> EmbedReport(string username, string roles)
        {

            var result = new EmbedConfig();
            try
            {
                result = new EmbedConfig { Username = username, Roles = roles };
                var error = GetWebConfigErrors();
                if (error != null)
                {
                    result.ErrorMessage = error;
                    // return View(result);
                    return result;
                }

                // Create a user password cradentials.
                var credential = new UserPasswordCredential(Username, Password);

                // Authenticate using created credentials
                var authenticationContext = new AuthenticationContext(AuthorityUrl);
                var authenticationResult = await authenticationContext.AcquireTokenAsync(ResourceUrl, ApplicationId, credential);

                if (authenticationResult == null)
                {
                    result.ErrorMessage = "Authentication Failed.";
                    // return View(result);
                    return result;
                }

                var tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");

                // Create a Power BI Client object. It will be used to call Power BI APIs.
                using (var client = new PowerBIClient(new Uri(ApiUrl), tokenCredentials))
                {
                    // Get a list of reports.
                    var reports = await client.Reports.GetReportsInGroupAsync(WorkspaceId);

                    // No reports retrieved for the given workspace.
                    if (reports.Value.Count() == 0)
                    {
                        result.ErrorMessage = "No reports were found in the workspace";
                        // return View(result);
                        return result;
                    }

                    Report report;
                    if (string.IsNullOrWhiteSpace(ReportId))
                    {
                        // Get the first report in the workspace.
                        report = reports.Value.FirstOrDefault();
                    }
                    else
                    {
                        report = reports.Value.FirstOrDefault(r => r.Id == ReportId);
                    }

                    if (report == null)
                    {
                        result.ErrorMessage = "No report with the given ID was found in the workspace. Make sure ReportId is valid.";
                        // return View(result);
                        return result;
                    }

                    var datasets = await client.Datasets.GetDatasetByIdInGroupAsync(WorkspaceId, report.DatasetId);
                    result.IsEffectiveIdentityRequired = datasets.IsEffectiveIdentityRequired;
                    result.IsEffectiveIdentityRolesRequired = datasets.IsEffectiveIdentityRolesRequired;
                    GenerateTokenRequest generateTokenRequestParameters;
                    // This is how you create embed token with effective identities
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        var rls = new EffectiveIdentity(username, new List<string> { report.DatasetId });
                        if (!string.IsNullOrWhiteSpace(roles))
                        {
                            var rolesList = new List<string>();
                            rolesList.AddRange(roles.Split(','));
                            rls.Roles = rolesList;
                        }
                        // Generate Embed Token with effective identities.
                        generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view", identities: new List<EffectiveIdentity> { rls });
                    }
                    else
                    {
                        // Generate Embed Token for reports without effective identities.
                        generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
                    }

                    var tokenResponse = await client.Reports.GenerateTokenInGroupAsync(WorkspaceId, report.Id, generateTokenRequestParameters);

                    if (tokenResponse == null)
                    {
                        result.ErrorMessage = "Failed to generate embed token.";
                        // return View(result);
                        return result;
                    }

                    if (Session["UserID"] != null)
                    {
                        using (callcenterEntities db = new callcenterEntities())
                        {
                            int userid = Convert.ToInt32(Session["UserID"]);
                            var validuser = db.userinfoes.FirstOrDefault(i => i.UserId == userid);
                            result.CanEdit = validuser.CanEdit;
                            result.CanFilter = validuser.CanFilter;
                        }
                    }

                    // Generate Embed Configuration.
                    result.EmbedToken = tokenResponse;
                    result.EmbedUrl = report.EmbedUrl;
                    result.Id = report.Id;

                    // return View(result);
                    return result;
                }
            }
            catch (HttpOperationException exc)
            {
                result.ErrorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.ToString();
            }

            // return View(result);
            return result;
        }

        /// <summary>
        /// Check if web.config embed parameters have valid values.
        /// </summary>
        /// <returns>Null if web.config parameters are valid, otherwise returns specific error string.</returns>
        private string GetWebConfigErrors()
        {
            // Application Id must have a value.
            if (string.IsNullOrWhiteSpace(ApplicationId))
            {
                return "ApplicationId is empty. please register your application as Native app in https://dev.powerbi.com/apps and fill client Id in web.config.";
            }

            // Application Id must be a Guid object.
            Guid result;
            if (!Guid.TryParse(ApplicationId, out result))
            {
                return "ApplicationId must be a Guid object. please register your application as Native app in https://dev.powerbi.com/apps and fill application Id in web.config.";
            }

            // Workspace Id must have a value.
            if (string.IsNullOrWhiteSpace(WorkspaceId))
            {
                return "WorkspaceId is empty. Please select a group you own and fill its Id in web.config";
            }

            // Workspace Id must be a Guid object.
            if (!Guid.TryParse(WorkspaceId, out result))
            {
                return "WorkspaceId must be a Guid object. Please select a workspace you own and fill its Id in web.config";
            }

            // Username must have a value.
            if (string.IsNullOrWhiteSpace(Username))
            {
                return "Username is empty. Please fill Power BI username in web.config";
            }

            // Password must have a value.
            if (string.IsNullOrWhiteSpace(Password))
            {
                return "Password is empty. Please fill password of Power BI username in web.config";
            }

            return null;
        }
    }
}

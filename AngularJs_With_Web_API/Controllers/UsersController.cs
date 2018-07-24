using AngularJs_With_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Mvc;

namespace AngularJs_With_Web_API.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public List<Usersdata> Get()
        {
            List<Usersdata> emplist = new List<Usersdata>();
            using (callcenterEntities db = new callcenterEntities())
            {
                var results = db.userinfoes.ToList();
                /*var results = db.GetAllUsers(0, "").ToList();
                //var results = db.sp_InsUpdDelEmployee(0, "", "", "", "", "", "Get").ToList(); */
                foreach (var result in results)
                {
                    var employee = new Usersdata()
                    {
                        UserId = result.UserId,
                        LoginName = result.LoginName,
                        UserEmail = result.UserEmail,
                        UserName = result.UserName,
                        IsActive = result.IsActive,
                        IsAdmin = result.IsAdmin
                    };
                    emplist.Add(employee);
                }
                return emplist;
            }
        }

        // GET api/<controller>/5
        public userinfo Get(int id)
        {
            using (callcenterEntities db = new callcenterEntities())
            {
                userinfo employee = db.userinfoes.Find(id);
                if (employee == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return employee;
            }
        }

        // Insert Employee
        public HttpResponseMessage Post(Usersdata user)
        {
            string OutMessage = string.Empty;

            if (ModelState.IsValid)
            {
                using (callcenterEntities db = new callcenterEntities())
                {
                    ObjectParameter Output = new ObjectParameter("responseMessage", typeof(string));

                    //string mssage = string.Empty;
                    var objresults = db.uspAddUser(user.LoginName, Convert.ToString(user.PasswordHash), user.UserName, user.UserEmail, user.IsAdmin, user.IsActive, Output);
                    var emplist = db.userinfoes.ToList();

                    OutMessage = Convert.ToString(Output.Value);

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { emplist, responsemessage = OutMessage });
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // Update Employee
        public HttpResponseMessage Put(Usersdata user)
        {
            string OutMessage = string.Empty;

            List<userinfo> emplist = new List<userinfo>();
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            using (callcenterEntities db = new callcenterEntities())
            {

                try
                {
                    ObjectParameter Output = new ObjectParameter("responseMessage", typeof(string));

                    var users = db.uspAddUser(user.LoginName, Convert.ToString(user.PasswordHash), user.UserName, user.UserEmail, user.IsAdmin, user.IsActive, Output);
                    OutMessage = Convert.ToString(Output.Value);
                    emplist = db.userinfoes.ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { emplist, responsemessage = OutMessage });
        }

        // Delete employee By Id
        public HttpResponseMessage Delete(int Id, string Undelete)
        {
            string OutMessage = string.Empty;
            using (callcenterEntities db = new callcenterEntities())
            {

                List<userinfo> emplist = new List<userinfo>();
                try

                {
                    ObjectParameter Output = new ObjectParameter("responseMessage", typeof(string));
                    string plogin = db.userinfoes.Find(Id).LoginName;
                    db.uspUnDeleteUser(plogin, Output);
                    OutMessage = Convert.ToString(Output.Value);
                    //emplist = db.userinfoes.ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { emplist, responsemessage = OutMessage });
            }
        }

        // Delete employee By Id
        [Route("api/users")]
        [HttpDelete]
        public HttpResponseMessage DeleteMethod(int Id)
        {
            string OutMessage = string.Empty;
            using (callcenterEntities db = new callcenterEntities())
            {
                List<userinfo> emplist = new List<userinfo>();
                try
                {
                    ObjectParameter Output = new ObjectParameter("responseMessage", typeof(string));
                    string plogin = db.userinfoes.Find(Id).LoginName;
                    db.uspDeleteUser(plogin, Output);
                    OutMessage = Convert.ToString(Output.Value);
                    // emplist = db.userinfoes.ToList();
                    //emplist = db.userinfoes.ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { emplist, responsemessage = OutMessage });
            }
        }

        // Prevent Memory Leak
        protected override void Dispose(bool disposing)
        {
            using (callcenterEntities db = new callcenterEntities())
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
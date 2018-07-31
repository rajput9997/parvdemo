// Read embed application token from textbox
var txtAccessToken = "H4sIAAAAAAAEAB2WxQ7sBhJF_-VtHcnQxkhZmJnZOzNDm-1o_n1a2d_VqXOr6t8_VvoMc1r8-fsPW64ofnv7rQxU0ZbvEmpiat3MQcGHcBeFX71hvLQwR4hNMGfw5xlyY0xEcCapNVElRrfDtPwIahLIgGWmnRqDK9UKfaZkkHMRmqkK3zPpP1uVxJdZ-dDKSjeHgjN76JPz0nCORxz-fE7F3fbG83Rsk43PuSb9CyolgHzsyxulgvLKQzsFw3VlX-X8yWwCr8JhToDLuyrnoo8ZAqV4cFkU7OhU-_CuPeX7plpDAnpaxRItTNDHzOlyuQRwiTPsezPdiZNBeooxjx20_KOlLz29zxs02YiSFFGJ2Sd9070GPiCwFkB9N_MjaU5edM_MYSlN9GIMNg1UrPtBtAFZApA4bGtrKZXlVgpuXTVoevXmVXy05biCEnWMH6bCDAK3FGOGOAckXG2W-GRFiueiLgGlFIv3jUVgxNsysdiSoWA0g3T_xFY_zO0o2DPW2PA2p13LYXUQlJwKSHXfkvJZMMG3fa8fY5pBSyVFZf0piscYeNUnIhNdhwrqkW4qWeNdLJ3apMYR-pjP7Iqj6kBwzNhA-WC0c6PjHHcdEgVs38iCH56r5fAz2pNYD-PrmhR2ZLN8zuJsAWC0_fzZqMOWwIq_f8D88kAdeHwBqx68C5UkzAsacuNqgAclfQGaGnbMQCQajZEfDPdsHtBAr7ivEQiMOeqqNqZpkFqe5bGehWlUYTq-XRwc_FHgUzurJY3A9AqncPtiqHd9LJ13ke43CeaQxlwl44XmHKddvpoMHR_nxiP5gX1AeKUguAyl2_EeCF07e7x7BanrCjORztKcRhSkMKmMzdhVjYzGVicgLubUKwkofHTOoEovcuXNV64r4bv2O4rhhpYcUzACLhKiDkhsj9PXqHeVhnLyk6oJ6MZ5qCUjQl3IXrswVKD3axF5KwW39i5703TBj5t9ZHabpBAiAY55A5Wfdumubox_X2ybNEj_xWX7yQc_rqsZgOe4fd3FPrWUCnZmpSKwjAfENurUUneS5CJtWXm4m3pWr-yNrLR5HfZDkJiNvkF7GC6VVQaAXQeU-sKZ22JdWkgK_3A8-eUK_KxyPAZCevTHMURUhTmhvlPuG0HFrW_bHFnqQlgY78mXsQpL2J1mBRGoiBkdXVAY4i6HDK_5-MC22BrOn6FJvJSZVD75dYGQXX8Yx-peVF1e8F0NBxd8J6ZCZTyBA7Zi1Kt-DoNEoH2_X7D4HpZhjjSHWvZm7zGym5gBm8hJXRNkg2lFzpaEwj7ekd0UXxhVq-tr1NPR2xtzX4azJhbILAFX2AC0xFcQMYo7k1Bze_28_eKgDZQAkEQZL5HPM5Dt3ZxZ0VWX4zVfJMLUBVX29MkLT6iA8kIBeW3QDJOxSZxNUiK5PeKsCr4ah_8UqeWRy6PAlbOZOGjwRbkolSSjZwXiiBlUEldxm15_o2SHbaJlpEE0-Y0wlJYq4653Wkd5h-tj8iopU8cnt801ryNWMR1Yfb38qBpb6lTkEQQFB4WyKeMILMJ9iTAX4GlHKZX91nqnVFjizqwP1Ca4IWUMKgboID0jfgv3eVx--N79EFY7ucgvbhclqD62CPFpvapWyH_9EuVYNr53ptYewOjroLOavLREMbDbmTdjXAKBmFbo0HgZw3w8vecZrie6nMgFMKj5QPZrT3tSK3NQ0T7FNZ91EqVfA1GwNndGRQbQMHTGg0AVTmx7utxVVFQL0LHAu5dG8ZpOaEam1FTa5gQpKNHXqAFafIFEoaqtKB6r9yF2viyyAm_WcZnKbGCMk5OIFB-WaDSGR27teqY1tL7jY0TP6Ks_lIlA7cROprAf7e6ZQZ_xWUrgtE5ctr-ejdit48lywYxxReZRF_eJCb9lPijfzTcYAYMhEq5i6LBG8pSjtFfopYqMuQxo1c2uAYgyOwFnY5_TdSpqNY52XFFdFuY4_AQ7-0EUeDxOy0kGXxib8vtCnkOGhnO2201x-Mzn8gFAx3T4gIH_9ohexiqaOTns94SONkh6faeBvA9fN9wIquyBLIhJ-zb-YNOeD8z-gGTGsbSinMmyVTYWw-3-nO_0P3_--sOuz7L_1ufzO_1KfkZExpCHgNFpu06ybeYCPagcHQ8VOBagOrtqQJkZDN4QZoKOIObrBaIth8wI1xTW4_DzEIqsCxZiUnVogUaWTrNWJPgyWqfs0GqtEVgZcdyYFLkiuYZUylFIRi2NdKxeAbeFp-DYArJTnzCMg0i10NBqbnqo8TMm3vbdOBi7dCDye6EfrlDyRZa6aXa9t1vIfTgfncYIoeJrkVawM4I_LIRaZ7Cstrg1m4fE0yfyDEhmmO5J2mYzOQKZmEBBU5vXTZ40WuGE8ixWNTZFA3RujoSqQK8634bCk0LDaOe80fk2NyXsyCvFfr3HIYfABUnbsXmaVmvK6UjgrKyoqIqA7H_-w_wsTbnKwY9y2HVeUm3-V7sIGIoQhpGkhf4v5bb1lO7HWv5ipgx8luUQLjmII58T6lqvbQ7ulo3g51wXNqCnv5fr6Lq5CdS48lTdu4B233M93lTpDuyvdT5FzuYwJn5qwbLkfqN2HgHEZ2Ow-sr55iW81oDIpZiIGmJDI9edk7vx2kxAohv1Z_7Cn-l7s17GJHV_FfFLqjDDTJfCVQBDDNsdowegkl_YgTCqk_P6Tp46AJVQBrlP14UxDLcKOS17GFRGZ1ILXkZLS_SfyI-4-ZrfBMH1NdMCDwF7ho9-T46HSPq4n2hQb4NHnKeGa3lACYiVA4_d2FDKBo391b85Y0hSW0q2m3wUemVXPwDxGPxGqBN__Zmp8d0PpWIF8QzxCbndCRz_1j_M__s_WEGI3u4KAAA="  //  $('#txtAccessToken').val();

var reportId = "8a5b15cc-e026-4d5d-94c0-db5091d65614";
var groupId = "be8908da-da25-452e-b220-163f52476cdd";

// Read embed URL from textbox
var txtEmbedUrl = "https://app.powerbi.com/reportEmbed?reportId=" + reportId + "&groupId=" + groupId //$('#txtReportEmbed').val();

// Read report Id from textbox
var txtEmbedReportId = reportId//"8a5b15cc-e026-4d5d-94c0-db5091d65614" //$('#txtEmbedReportId').val();

// Read embed type from radio
var tokenType = "1"; //$('input:radio[name=tokenType]:checked').val();

// Get models. models contains enums that can be used.
var models = window['powerbi-client'].models;

// We give All permissions to demonstrate switching between View and Edit mode and saving report.
var permissions = models.Permissions.All;

// Generate embed token
var report_Id = "";
var group_Id = "";
generateEmbedToken(reportId, groupId)
    .then(function (Token) {
        var embedToken = Token.token;
        console.log(embedToken);
    });


// Embed configuration used to describe the what and how to embed.
// This object is used when calling powerbi.embed.
// This also includes settings and options such as filters.
// You can find more information at https://github.com/Microsoft/PowerBI-JavaScript/wiki/Embed-Configuration-Details.
var config = {
    type: 'report',
    tokenType: tokenType == '0' ? models.TokenType.Aad : models.TokenType.Embed,
    accessToken: txtAccessToken,
    embedUrl: txtEmbedUrl,
    id: txtEmbedReportId,
    permissions: permissions,
    settings: {
        filterPaneEnabled: true,
        navContentPaneEnabled: true
    }
};

// Get a reference to the embedded report HTML element
var embedContainer = $('#embedContainer')[0];

// Embed the report and display it within the div container.
var report = powerbi.embed(embedContainer, config);

// Report.off removes a given event handler if it exists.
report.off("loaded");

// Report.on will add an event handler which prints to Log window.
report.on("loaded", function () {
    // Log.logText("Loaded");
});

report.on("error", function (event) {
    alert(event.detail);
    report.off("error");
});

report.off("saved");
report.on("saved", function (event) {
    //Log.log(event.detail);
    if (event.detail.saveAs) {
        Log.logText('In order to interact with the new report, create a new token and load the new report');
    }
});

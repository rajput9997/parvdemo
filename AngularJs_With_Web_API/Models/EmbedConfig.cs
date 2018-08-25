﻿using Microsoft.PowerBI.Api.V2.Models;
using System;

namespace AngularJs_With_Web_API.Models
{
    public class EmbedConfig
    {
        public string Id { get; set; }

        public string EmbedUrl { get; set; }

        public EmbedToken EmbedToken { get; set; }

        public int MinutesToExpiration
        {
            get
            {
                var minutesToExpiration = EmbedToken.Expiration.Value - DateTime.UtcNow;
                return minutesToExpiration.Minutes;
            }
        }

        public bool? IsEffectiveIdentityRolesRequired { get; set; }

        public bool? IsEffectiveIdentityRequired { get; set; }

        public bool EnableRLS { get; set; }

        public string Username { get; set; }

        public string Roles { get; set; }

        public bool? CanEdit { get; set; }
        public bool? CanFilter { get; set; }

        public bool? CanNavigate { get; set; }

        public string ErrorMessage { get; internal set; }
    }
}
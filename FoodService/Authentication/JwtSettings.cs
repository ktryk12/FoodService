﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodService.Authentication
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
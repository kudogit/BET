#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> JwtTokenHelper.cs </Name>
//         <Created> 23/04/2018 11:55:27 AM </Created>
//         <Key> b088b53d-5a97-45bc-9300-7a53ac076fff </Key>
//     </File>
//     <Summary>
//         JwtTokenHelper.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.DateTimeUtils;
using Elect.Core.EnvUtils;
using Elect.Core.ObjUtils;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace BETSTS.Core.Utils
{
    public class JwtHelper
    {
        public static string Generate<T>(T data, int expireInSeconds)
        {
            var systemTimeNow = SystemHelper.SystemTimeNow;

            var claims = SystemHelper.GetClaimsIdentity(data);

            var handler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                IssuedAt = systemTimeNow.DateTime,
                NotBefore = systemTimeNow.DateTime,
                Expires = systemTimeNow.AddSeconds(expireInSeconds).DateTime,
                Subject = claims,
                SigningCredentials = GetSigningCredential(),
                
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        public static bool IsValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidation = GetTokenValidation();

                handler.ValidateToken(token, tokenValidation, out _);

                return true;
            }
            catch (Exception e)
            {
                if (EnvHelper.IsDevelopment())
                {
                    Console.WriteLine(e);
                }

                return false;
            }
        }

        public static bool IsExpire(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return true;
            }

            double? expireEpochTime = Get<double?>(token, JwtRegisteredClaimNames.Exp);

            // Not have expire epoch time => never expire

            if (!expireEpochTime.HasValue)
            {
                return false;
            }

            var expireOn = DateTimeHelper.FromTimestamp((long) expireEpochTime.Value);

            var isExpired = expireOn <= SystemHelper.SystemTimeNow;

            return isExpired;
        }

        public static T Get<T>(string token)
        {
            var properties = typeof(T).GetProperties();

            var dictionary = new Dictionary<string, object>();

            foreach (var propertyInfo in properties)
            {
                var key = propertyInfo.Name;

                var value = Get(token, key);

                dictionary.Add(key, value);
            }

            var json = JsonConvert.SerializeObject(dictionary, Models.Constants.Formatting.JsonSerializerSettings);

            var data = JsonConvert.DeserializeObject<T>(json, Models.Constants.Formatting.JsonSerializerSettings);

            return data;
        }

        public static T Get<T>(string token, string key)
        {
            var data = Get(token, key);

            if (data == null)
            {
                return default;
            }

            return data.ConvertTo<T>();
        }

        public static object Get(string token, string key)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (!TryReadTokenPayload(token, out var tokenPayload))
            {
                return null;
            }

            foreach (var tokenKey in tokenPayload.Keys)
            {
                if (!string.Equals(tokenKey, key, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                return tokenPayload[tokenKey];
            }

            return null;
        }

        #region Helper

        private static bool TryReadTokenPayload(string token, out JwtPayload tokenPayload)
        {
            if (!IsValid(token))
            {
                tokenPayload = null;
                return false;
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            tokenPayload = JwtPayload.Base64UrlDeserialize(jwtToken.EncodedPayload);

            return tokenPayload?.Keys.Any() == true;
        }

        private static SigningCredentials GetSigningCredential()
        {
            var securityKey = GetSecurityKey();

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            return signingCredentials;
        }

        private static SymmetricSecurityKey GetSecurityKey()
        {
            var secretKey = SystemHelper.Setting.EncryptKey.ToString("N");

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            return securityKey;
        }

        private static TokenValidationParameters GetTokenValidation()
        {
            var securityKey = GetSecurityKey();

            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // use IsExpire instead of this
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateActor = false,
            };

            return tokenValidationParameters;
        }

        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using static RipOffAPI.Models.AuthModel;
using System.Security.Claims;

namespace RipOffAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.Username = login.Username;
            loginrequest.Password = login.Password;
            string fullname = "";

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            if (login != null)
            {
                using (RipoffRentalsEntities entities = new RipoffRentalsEntities())
                {
                    var user = entities.Users.FirstOrDefault(u => u.Email == loginrequest.Username);
                    if (user == null)
                    {
                        user = entities.Users.FirstOrDefault(u => u.User_Name == loginrequest.Username);
                        if (user == null) {
                            loginResponse.responseMsg.StatusCode = HttpStatusCode.NotFound;
                            response = ResponseMessage(loginResponse.responseMsg);
                            return response;
                        }
                    }
                    fullname = user.Full_Name;
                    loginrequest.Role = user.Permissions;
                    isUsernamePasswordValid = loginrequest.Password == user.Password  ? true : false;
                }
            }
            if (isUsernamePasswordValid)
            {
                string token = createToken(loginrequest.Username, fullname);
                return Ok<string>(token);
            }
            else
            {
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
        }

        private string createToken(string username, string fullname)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Name, fullname)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:57182", audience: "http://localhost:57182",
                    subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


    }
}

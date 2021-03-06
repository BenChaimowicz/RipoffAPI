﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RipOffAPI.Models
{
    public class AuthModel
    {
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class LoginResponse
        {

            public string Token { get; set; }
            public string FullName { get; set; }
            public int Id { get; set; }
            public HttpResponseMessage responseMsg { get; set; }

            public LoginResponse()
            {

                this.Token = "";
                this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
            }
        }
    }
}
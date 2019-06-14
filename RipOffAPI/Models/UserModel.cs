using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipOffAPI.Models
{
    public class UserModel
    {
        public int uid { get; set; }
        public string FullName { get; set; }
        public string IDNumber { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Permissions { get; set; }
        public string DateOfBirth { get; set; }

        public UserModel(
            int id, 
            string fullname, 
            string idnumber, 
            string username,
            string gender,
            string email,
            string permissions,
            string image,
            string dateofbirth)
        {
            uid = id;
            FullName = fullname;
            IDNumber = idnumber;
            UserName = username;
            Gender = gender;
            Email = email;
            Permissions = permissions;
            Image = image;
            DateOfBirth = dateofbirth;
        }
    }

}
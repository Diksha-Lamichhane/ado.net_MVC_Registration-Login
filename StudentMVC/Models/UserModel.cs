using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentMVC.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        public int Student_ID { get; set; }

        [Required(ErrorMessage = "UserName is required..!!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required..!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using AMWP.Models;

namespace AMWP.ViewModels
{
    public class AdminLogin
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請填寫管理員帳號")]
        public string Account { get; set; }

        string password;
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫管理員密碼")]
        [DataType(DataType.Password)]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = BusinessRules.getHashPassword(value);
            }
        }

        
    }
}
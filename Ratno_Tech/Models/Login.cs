﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ratno_Tech.Models
{
    public class Login
    {
       
        public int aid { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        
        
        public string password { get; set; }

        public Nullable<bool> IsActive { get; set; }
    }
}
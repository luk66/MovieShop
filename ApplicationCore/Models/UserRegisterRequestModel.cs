﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserRegisterRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(64)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password should be at least 8 characters and not exceeding 100", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password should have minimun 8 characters and should include one upper, lower, number and a special char")]
        public string  Password { get; set; }
        
        [Required]
        [StringLength(50)]
        public string  FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [DataType(DataType.Date)]
        // minimum age and maximum age

        public DateTime DateOfBirth { get; set; }

    }
}

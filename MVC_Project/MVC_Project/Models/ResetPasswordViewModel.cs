﻿namespace MVC_Project.Models
{
    public class ResetPasswordViewModel
    {
        public string ?Token { get; set; }
        public string ?NewPassword { get; set; }
        public string ?ConfirmPassword { get; set; }

    }
}

using System;
using System.ComponentModel;

namespace PresentationGenerator.Core.Views.Models.Output
{
    public class UserRegisterViewModel
    {
        [DisplayName("Username")]
        public string Username
        {
            get;
            set;
        }

         [DisplayName("Password")]
        public string Password
        {
            get;
            set;
        }

        [DisplayName("Stay logged in")]
        public Boolean StayLoggedIn
        {
            get;
            set;
        }
    }
}
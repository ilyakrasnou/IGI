using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeRock.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}

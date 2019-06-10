using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserAccountInfoEditVM
    {
        public string Id { get; set; }

        public string Name { get; set; } 

        public bool? IsAnonimus { get; set; } 

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public UserAccountInfoEditVM()
        {

        }
    }
}

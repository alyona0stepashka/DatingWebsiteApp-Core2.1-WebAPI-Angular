using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserPhotoCreateVM
    {
        public string Id { get; set; }

        public IFormFile UploadPhoto { get; set; }

        public UserPhotoCreateVM()
        {
                
        }
    }
}

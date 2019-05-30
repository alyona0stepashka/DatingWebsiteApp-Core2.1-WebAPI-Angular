using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class AlbumAddPhotoVM
    {
        public int Id { get; set; } //album_id

        public IFormFile UploadPhoto { get; set; }
    }
}

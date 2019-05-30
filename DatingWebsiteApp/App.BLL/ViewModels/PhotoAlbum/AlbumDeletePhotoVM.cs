using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class AlbumDeletePhotoVM
    {
        public int Id { get; set; } //album_id

        public string file_path { get; set; }
    }
}

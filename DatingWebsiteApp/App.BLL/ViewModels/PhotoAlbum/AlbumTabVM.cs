using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.BLL.ViewModels
{
    public class AlbumTabVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public AlbumTabVM()
        {

        }
        public AlbumTabVM(PhotoAlbum album)
        {
            Id = album.Id;
            Name = album.Name;
            FilePath = null;
            if (album.Files.Any())
            {
                FilePath = album.Files.First().Path;
            }
        }
    }
}

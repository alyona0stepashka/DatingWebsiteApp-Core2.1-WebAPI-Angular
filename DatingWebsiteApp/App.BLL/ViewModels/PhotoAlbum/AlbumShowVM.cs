using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.BLL.ViewModels
{
    public class AlbumShowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> FilePathes { get; set; }
        public AlbumShowVM()
        {

        }
        public AlbumShowVM(PhotoAlbum album)
        {
            Id = album.Id;
            Name = album.Name;
            Description = album.Description;
            FilePathes = new List<string>();
            if (album.Files != null)
            {
                if (album.Files.Any())
                {
                    foreach (var file in album.Files)
                    {
                        FilePathes.Add(file.Path);
                    }
                }
            }
        }
    }
}

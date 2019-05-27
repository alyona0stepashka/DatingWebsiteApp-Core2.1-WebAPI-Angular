using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class PhotoAlbum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<FileModel> Files { get; set; }
    }
}

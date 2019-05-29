using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class FileModel : EntityBase
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        //[ForeignKey("PhotoAlbum")]
        public int? PhotoAlbumId { get; set; }

       // [ForeignKey("Message")]
        public int? MessageId { get; set; }

        public virtual PhotoAlbum PhotoAlbum { get; set; }

        public virtual ChatMessage Message { get; set; }
    }
}

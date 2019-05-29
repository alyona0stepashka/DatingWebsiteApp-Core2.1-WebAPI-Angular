using App.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IFileService
    {
        Task<int> CreatePhotoAsync(IFormFile photo);
        Task<int> CreatePhotoForAlbumAsync(IFormFile photo, PhotoAlbum album /*int album_id*/);
        Task<int> CreatePhotoForMessageAsync(IFormFile photo, ChatMessage message /*int message_id*/);
    }
}

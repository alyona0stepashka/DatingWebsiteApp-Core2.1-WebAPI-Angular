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
        Task<int?> DeletePhotoForAlbumAsync(string file_path, int album_id);
        Task<int?> DeletePhoto(int id);
        Task<int> CreateFileModelDbAsync(IFormFile photo, string path, string file_name, int? message_id, int? album_id);
        void CreateDirectoryIfNotExist(List<string> path_list);
    }
}

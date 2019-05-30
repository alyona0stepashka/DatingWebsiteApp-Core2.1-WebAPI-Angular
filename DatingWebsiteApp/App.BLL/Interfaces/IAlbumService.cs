using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IAlbumService
    {
        Task<PhotoAlbum> GetDbAlbum(int album_id);
        Task<List<AlbumTabVM>> GetAllAlbumsByUserIdAsync(string user_id);
        Task<AlbumShowVM> GetOpenAlbumAsync(int album_id);
        Task<AlbumShowVM> AddPhotoAsync(AlbumAddPhotoVM model);
        Task<AlbumShowVM> DeletePhotoAsync(AlbumDeletePhotoVM model);
        Task<AlbumShowVM> DeleteAlbumAsync(int album_id);
        Task<AlbumShowVM> CreateAlbumAsync(AlbumShowVM model, string user_id);
    }
}

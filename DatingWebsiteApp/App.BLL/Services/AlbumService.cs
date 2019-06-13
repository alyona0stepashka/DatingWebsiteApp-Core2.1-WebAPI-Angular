using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;

        public AlbumService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
            _fileService = fileService;
        }

        public async Task<PhotoAlbum> GetDbAlbum(int album_id)
        {
            try
            {
                var db_album = await _db.PhotoAlbums.GetByIdAsync(album_id);
                if (db_album == null)
                {
                    return null;
                }
                return db_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<AlbumTabVM>> GetAllAlbumsByUserIdAsync(string user_id)
        {
            try
            {
                var ret_albums = new List<AlbumTabVM>();
                var db_albums = (await _userManager.FindByIdAsync(user_id)).PhotoAlbums;
                if (db_albums != null)
                {
                    foreach (var album in db_albums)
                    {
                        ret_albums.Add(new AlbumTabVM(album));
                    }
                }
                return ret_albums;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<AlbumShowVM> GetOpenAlbumAsync(int album_id)
        {
            try
            {
                var album = await _db.PhotoAlbums.GetByIdAsync(album_id);
                if (album == null)
                {
                    return null;
                }
                var ret_album = new AlbumShowVM(album);
                return ret_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<AlbumShowVM> AddPhotoAsync(AlbumAddPhotoVM model)
        {
            try
            {
                var album = await GetDbAlbum(model.Id);
                await _fileService.CreatePhotoForAlbumAsync(model.UploadPhoto, album);
                album = await GetDbAlbum(model.Id);
                var ret_album = new AlbumShowVM(album);
                return ret_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<AlbumShowVM> DeletePhotoAsync(AlbumDeletePhotoVM model)
        {
            try
            {
                var delete_res = await _fileService.DeletePhotoForAlbumAsync(model.file_path, model.Id);
                if (delete_res == null)
                {
                    return null;
                }
                var album = await GetDbAlbum(model.Id);
                if (album == null)
                {
                    return null;
                }
                var ret_album = new AlbumShowVM(album);
                return ret_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<int?> DeletePhotoAsync(int id)
        {
            try
            {
                var photo = _db.FileModels.GetWhere(m => m.Id == id);
                if (photo==null)
                {
                    return null;
                }
                var delete_res = await _fileService.DeletePhoto(id);
                if (delete_res == null)
                {
                    return null;
                } 
                return delete_res;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<AlbumShowVM> DeleteAlbumAsync(int album_id)
        {
            try
            {
                var album = await GetDbAlbum(album_id);
                if (album == null)
                {
                    return null;
                }
                album = await _db.PhotoAlbums.DeleteAsync(album_id);
                var ret_album = new AlbumShowVM(album);
                return ret_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<AlbumShowVM> CreateAlbumAsync(AlbumShowVM model, string user_id)
        {
            try
            {
                var album = await _db.PhotoAlbums.CreateAsync(new PhotoAlbum
                {
                    Name = model.Name,
                    Description = model.Description,
                    UserId = user_id
                });
                var ret_album = new AlbumShowVM(album);
                return ret_album;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

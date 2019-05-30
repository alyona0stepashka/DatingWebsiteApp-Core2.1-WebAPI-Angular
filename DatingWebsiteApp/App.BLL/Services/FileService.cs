using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class FileService : IFileService
    {
        private IUnitOfWork _db { get; set; }
        private readonly IHostingEnvironment _appEnvironment;
        public FileService(IUnitOfWork uow,
            IHostingEnvironment appEnvironment)
        {
            _db = uow;
            _appEnvironment = appEnvironment;
        }

        public async Task<int> CreatePhotoAsync(IFormFile photo)
        {
            var id = 0;
            if (photo != null) //если загрузили фото 
            {
                var path = "/Images/General/" + photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                var file = new FileModel
                {
                    Name = photo.FileName,
                    Path = path
                };
                await _db.FileModels.CreateAsync(file);
                id = file.Id;
            }
            else  //если не загрузили фото
            {
                var photo_no_image = (_db.FileModels.GetWhere(m => m.Name == "no-image.jpg")).FirstOrDefault();  //ищем фото-заглушку
                if (photo_no_image == null)  //если ее нет в бд - создаем
                {
                    photo_no_image = await _db.FileModels.CreateAsync(new FileModel
                    {
                        Name = "no-image.jpg",
                        Path = "/Images/App/no-image.jpg"
                    });
                } 
                id = photo_no_image.Id;
            }
            return id;
        }

        public async Task<int> CreatePhotoForAlbumAsync(IFormFile photo, PhotoAlbum album)
        {
            var id = 0;
            if (photo != null) //если загрузили фото 
            {
                var path = "/Images/Users/" + album.UserId + "/Albums/"+ album.Id + "/" + photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                var file = new FileModel
                {
                    Name = photo.FileName,
                    Path = path,
                    PhotoAlbumId = album.Id
                };
                await _db.FileModels.CreateAsync(file);
                id = file.Id;
            } 
            return id;
        }
        public async Task<int?> DeletePhotoForAlbumAsync(string file_path, int album_id)
        {
            var file = _db.FileModels.GetWhere(m => m.Path == file_path).First();
            if (file==null)
            {
                return null;
            }
            else
            {
                await _db.FileModels.DeleteAsync(file.Id);
                return 0;
            } 
        }

        public async Task<int> CreatePhotoForMessageAsync(IFormFile photo, ChatMessage message)
        {
            var id = 0;
            if (photo != null) //если загрузили фото 
            {
                var path = "/Images/Chats/" + message.ChatId + "/" + photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                var file = new FileModel
                {
                    Name = photo.FileName,
                    Path = path,
                    MessageId = message.Id
                };
                await _db.FileModels.CreateAsync(file);
                id = file.Id;
            } 
            return id;
        }
    }
}

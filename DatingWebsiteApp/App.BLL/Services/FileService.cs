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
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private IUnitOfWork _db { get; set; }
        private readonly IHostingEnvironment _appEnvironment;
        public FileService(IUnitOfWork uow,
            IHostingEnvironment appEnvironment)
        {
            _db = uow;
            _appEnvironment = appEnvironment;
        }

        public void IsValidFile(IFormFile file, int file_max_size)
        {
            try
            {
                if (file.Length == 0)
                {
                    throw new Exception("Empty File");
                }
                if (file.Length > file_max_size * 1024 * 1024)
                {
                    throw new Exception("Max file size exceeded.");
                }
                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName).ToLower()))
                {
                    throw new Exception("Invalid file type.");
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreateFileModelDbAsync(IFormFile photo, string path, string file_name, int? message_id, int? album_id)
        {
            try
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                var file = new FileModel();
                if (message_id != null)
                {
                    file.Name = file_name;
                    file.Path = path;
                    file.MessageId = message_id; 
                }
                else if (album_id != null)
                {
                    file.Name = file_name;
                    file.Path = path;
                    file.PhotoAlbumId = album_id; 
                }
                else
                {
                    file.Name = file_name;
                    file.Path = path;
                }                
                await _db.FileModels.CreateAsync(file); 
                return file.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> CreatePhotoAsync(IFormFile photo)
        {
            try
            {
                var id = 0;
                if (photo != null)
                {
                    var file_name = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var path = "/Images/Users/" + file_name;
                    var dirs = new List<string>
                    {
                        "Users"
                    };
                    CreateDirectoryIfNotExist(dirs);                     
                    id = await CreateFileModelDbAsync(photo, path, file_name, null, null);
                }
                else
                {
                    var photo_no_image = (_db.FileModels.GetWhere(m => m.Name == "no-image.png")).FirstOrDefault();  //ищем фото-заглушку
                                                                                                                     //if (photo_no_image == null)  //если ее нет в бд - создаем
                                                                                                                     //{
                                                                                                                     //    var path = "/Images/General/no-image.png"; 
                                                                                                                     //    photo_no_image = await _db.FileModels.CreateAsync(new FileModel
                                                                                                                     //    {
                                                                                                                     //        Name = "no-image.png",
                                                                                                                     //        Path = path
                                                                                                                     //    });
                                                                                                                     //} 
                    id = photo_no_image.Id;
                }
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> CreatePhotoForAlbumAsync(IFormFile photo, PhotoAlbum album)
        {
            try
            {
                var id = 0;
                if (photo != null) //если загрузили фото 
                {
                    var file_name = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var path = "/Images/Users/" + album.UserId + "/Albums/" + album.Id + "/" + file_name;
                    var dirs = new List<string>
                    {
                        "Users", album.UserId.ToString(), "Albums", album.Id.ToString()
                    };
                    CreateDirectoryIfNotExist(dirs);
                    id = await CreateFileModelDbAsync(photo, path, file_name, null, album.Id);
                }
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task DeletePhotoForAlbumAsync(string file_path, int album_id)
        {
            try
            {
                var file = _db.FileModels.GetWhere(m => m.Path == file_path && m.PhotoAlbumId == album_id).FirstOrDefault(); 
                if (file == null)
                {
                    throw new Exception("File not found");
                }
                else
                {
                    await _db.FileModels.DeleteAsync(file.Id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeletePhoto(int id)
        {
            try
            {
                var file = await _db.FileModels.GetByIdAsync(id);
                if (file == null)
                {
                    throw new Exception("File not found");
                }
                else
                {
                    await _db.FileModels.DeleteAsync(file.Id); 
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> CreatePhotoForMessageAsync(IFormFile photo, ChatMessage message)
        {
            try
            {
                var id = 0;
                if (photo != null) //если загрузили фото 
                {
                    var file_name = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var path = "/Images/Chats/" + message.ChatId + "/" + file_name;
                    var dirs = new List<string>
                {
                    "Chats", message.ChatId.ToString()
                };
                    CreateDirectoryIfNotExist(dirs);
                    id = await CreateFileModelDbAsync(photo, path, file_name, message.Id, null);
                }
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateDirectoryIfNotExist(List<string> path_list)
        {
            try
            { 
                var cur_path = _appEnvironment.WebRootPath + "\\Images\\";
                foreach (var path in path_list)
                {
                    cur_path = cur_path + path + "\\";
                    if (!Directory.Exists(cur_path))
                    {
                        Directory.CreateDirectory(cur_path);
                    }
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
}
    }
}

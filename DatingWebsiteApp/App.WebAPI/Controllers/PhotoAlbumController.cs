using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IFileService _fileService;

        public PhotoAlbumController(IAlbumService albumService, IFileService fileService)
        {
            _albumService = albumService;
            _fileService = fileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyAlbums()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var albums = await _albumService.GetAllAlbumsByUserIdAsync(user_id);
                if (albums == null)
                {
                    return NotFound(new { message = "Albums not found by user id." });
                }
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAlbumsByUserId(string id)
        {
            try
            {
                var albums = await _albumService.GetAllAlbumsByUserIdAsync(id);
                if (albums == null)
                {
                    return NotFound(new { message = "Albums not found by user id." });
                }
                return Ok(albums);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            try
            {
                var album = await _albumService.GetOpenAlbumAsync(id);
                if (album == null)
                {
                    throw new Exception("Albums not found by id.");
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("photo/{id}")]
        [Authorize]
        public async Task<IActionResult> AddPhoto(int id)
        {
            try
            {
                var UploadPhoto = HttpContext.Request.Form.Files[0];

                if (UploadPhoto == null)
                {
                    throw new Exception("Null File");
                }

                _fileService.IsValidFile(UploadPhoto, 5);

                var album = await _albumService.AddPhotoAsync(new AlbumAddPhotoVM { Id = id, UploadPhoto = UploadPhoto });
                if (album == null)
                {
                    throw new Exception("Album not found by id (or error on create photo).");
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("test")]
        ////[Authorize]
        //public async Task<IActionResult> AddPhoto(IFormCollection formdata)
        //{
        //    int id = 3;
        //    var names = formdata["name"];
        //    var file = formdata.Files;
        //    try
        //    {
        //        var UploadPhoto = HttpContext.Request.Form.Files[0];

        //        if (UploadPhoto == null)
        //        {
        //            throw new Exception("Null File");
        //        }

        //        _fileService.IsValidFile(UploadPhoto, 5);

        //        var album = await _albumService.AddPhotoAsync(new AlbumAddPhotoVM { Id = id, UploadPhoto = UploadPhoto });
        //        if (album == null)
        //        {
        //            throw new Exception("Album not found by id (or error on create photo).");
        //        }
        //        return Ok(album);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            try
            {
                var album = await _albumService.DeleteAlbumAsync(id);
                if (album == null)
                {
                    throw new Exception("Album not found by id.");
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("photo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlbumPhoto(int id)
        {
            try
            {
                await _albumService.DeletePhotoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAlbum([FromBody]AlbumShowVM model)
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var album = await _albumService.CreateAlbumAsync(model, user_id);
                if (album == null)
                {
                    throw new Exception("Album not found by id.");
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
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
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IAlbumService _albumService;

        public PhotoAlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
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
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
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
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
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
                    return NotFound(new { message = "Albums not found by id." });
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
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

                if (UploadPhoto == null) return BadRequest("Null File");
                if (UploadPhoto.Length == 0)
                {
                    return BadRequest("Empty File");
                }
                if (UploadPhoto.Length > 5 * 1024 * 1024) return BadRequest("Max file size exceeded.");
                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(UploadPhoto.FileName).ToLower())) return BadRequest("Invalid file type.");

                var album = await _albumService.AddPhotoAsync(new AlbumAddPhotoVM { Id = id, UploadPhoto = UploadPhoto });
                if (album == null)
                {
                    return NotFound(new { message = "Album not found by id (or error on create photo)." });
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
            }
        }

        //[HttpPost]
        //[Route("photo")]
        //[Authorize]
        //public async Task<IActionResult> DeletePhoto(AlbumDeletePhotoVM model)
        //{
        //    var album = await _albumService.DeletePhotoAsync(model);
        //    if (album == null)
        //    {
        //        return NotFound(new { message = "Album not found by id (or error on delete photo)." });
        //    }
        //    return Ok(album);
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
                    return NotFound(new { message = "Album not found by id." });
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
            }
        }

        [HttpDelete("photo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlbumPhoto(int id)
        {
            try
            {
                var album = await _albumService.DeletePhotoAsync(id);
                if (album == null)
                {
                    return NotFound(new { message = "File not found by id." });
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
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
                    return NotFound(new { message = "Album not found by id." });
                }
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from PhotoAlbumController: " + ex.Message });
            }
        }
    }
}
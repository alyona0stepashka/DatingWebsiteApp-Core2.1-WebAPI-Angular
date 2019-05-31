using System;
using System.Collections.Generic;
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

        public PhotoAlbumController(IAlbumService albumService )
        {
            _albumService = albumService; 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyAlbums()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var albums = await _albumService.GetAllAlbumsByUserIdAsync(user_id);
            if (albums == null)
            {
                return NotFound(new { message = "Albums not found by user id." });
            }
            return Ok(albums);
        }

        [HttpGet, Route("user/{id}")]        
        [Authorize]
        public async Task<IActionResult> GetAlbumsByUserId(string id)
        {
            var albums = await _albumService.GetAllAlbumsByUserIdAsync(id);
            if (albums == null)
            {
                return NotFound(new { message = "Albums not found by user id." });
            }
            return Ok(albums);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            var album = await _albumService.GetOpenAlbumAsync(id);
            if (album == null)
            {
                return NotFound(new { message = "Albums not found by id." });
            }
            return Ok(album);
        }

        [HttpPost]
        [Route("photo")]
        [Authorize]
        public async Task<IActionResult> AddPhoto(AlbumAddPhotoVM model)
        {
            var album = await _albumService.AddPhotoAsync(model);
            if (album == null)
            {
                return NotFound(new { message = "Album not found by id (or error on create photo)." });
            }
            return Ok(album);
        }

        [HttpPost]
        [Route("photo")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(AlbumDeletePhotoVM model)
        {
            var album = await _albumService.DeletePhotoAsync(model);
            if (album == null)
            {
                return NotFound(new { message = "Album not found by id (or error on delete photo)." });
            }
            return Ok(album);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _albumService.DeleteAlbumAsync(id);
            if (album == null)
            {
                return NotFound(new { message = "Album not found by id." });
            }
            return Ok(album);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAlbum([FromBody]AlbumShowVM model)
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var album = await _albumService.CreateAlbumAsync(model, user_id);
            if (album == null)
            {
                return NotFound(new { message = "Album not found by id." });
            }
            return Ok(album);
        }
    }
}
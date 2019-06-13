using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace App.BLL.ViewModels
{
    public class ChatMessageSendVM
    {
        public int ChatId { get; set; } 

        public string Text { get; set; }

        public List<IFormFile> UploadFiles { get; set; }  

        public ChatMessageSendVM()
        {

        }
    }
}

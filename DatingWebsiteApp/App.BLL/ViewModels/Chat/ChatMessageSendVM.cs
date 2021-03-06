﻿using System;
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

        public string ReceiverId { get; set; }

        public string Text { get; set; }

        public IFormFileCollection UploadFiles { get; set; }  

        public ChatMessageSendVM()
        {

        }
    }
}

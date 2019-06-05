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
    public class StaticService : IStaticService
    {
        private IUnitOfWork _db { get; set; } 
        public StaticService(IUnitOfWork uow )
        {
            _db = uow; 
        } 
        //public object GetAll()
        //{
        //    var retVal = new Object();
        //    var retVal;
        //}
    }
}

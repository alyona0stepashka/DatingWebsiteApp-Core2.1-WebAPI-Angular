using App.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.Interfaces
{
    public interface IStaticService
    {
        StaticAllVM GetAll();
    }
}

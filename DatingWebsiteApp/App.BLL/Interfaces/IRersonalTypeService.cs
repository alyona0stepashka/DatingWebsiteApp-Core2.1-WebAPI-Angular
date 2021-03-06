﻿using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IPersonalTypeService
    {
        Task<PersonalType> EditTypeAsync(PersonalType type, UserInfoEditVM model); 
    }
}

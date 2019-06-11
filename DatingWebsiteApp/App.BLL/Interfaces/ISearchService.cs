using App.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface ISearchService
    {
        Task<List<UserTabVM>> StartSearchAsync(SearchVM search, string my_id);
    }
}

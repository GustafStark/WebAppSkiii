using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Model;

namespace WebAppSkiii.Repository.Interface
{
    public interface ISkiRepository
    {
        Task<Ski> GetItemAsync(int length, Style style);
    }
}

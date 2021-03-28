using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Model;

namespace WebAppSkiii.Service.Interface
{
    public interface ISkiService
    {
        Task<Ski> GetSki(int lenght, int age, Style style);
    }
}

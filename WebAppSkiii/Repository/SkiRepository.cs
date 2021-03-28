using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Model;
using WebAppSkiii.Repository.Interface;

namespace WebAppSkiii.Repository
{
    public class SkiRepository : ISkiRepository
    {
        public async Task<Ski> GetItemAsync(int length, Style style) => new Ski { Length = length, Style = style };
    }
}

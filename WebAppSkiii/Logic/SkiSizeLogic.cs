using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Logic.Interface;

namespace WebAppSkiii.Logic
{
    public class SkiSizeLogic : ISkiSizeLogic
    {
        public int SizeClassic(int lenght)
        {
            lenght = lenght + 20;
            if (lenght > 207) lenght = 207;
            return lenght;
        }

        public int SizeFreeStyle(int lenght)
        {
            return lenght + 15;
        }

        public int SizeUnderEight(int lenght, int age)
        {
            if (age > 7) return lenght + 10;
            return lenght + 20;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSkiii.Logic.Interface
{
    public interface ISkiSizeLogic
    {
        int SizeClassic(int lenght);
        int SizeFreeStyle(int lenght);
        int SizeUnderEight(int lenght, int age);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Logic;
using WebAppSkiii.Logic.Interface;
using WebAppSkiii.Model;
using WebAppSkiii.Repository.Interface;
using WebAppSkiii.Service.Interface;

namespace WebAppSkiii.Service
{
    public class SkiService : ISkiService
    {
        private readonly ISkiRepository skiRepository;
        private readonly ISkiSizeLogic skiSizeLogic;

        public SkiService(ISkiRepository skiRepository, ISkiSizeLogic skiSizeLogic)
        {
            this.skiRepository = skiRepository;
            this.skiSizeLogic = skiSizeLogic;
        }
        public async Task<Ski> GetSki(int lenght, int age, Style style)
        {
            Ski ski = await AgeCheck(lenght, age, style);
            if (ski != null) return ski;

            ski = await GetSizeByStyle(lenght, style);
            return ski;
        }

        private async Task<Ski> GetSizeByStyle(int lenght, Style style)
        {
            switch (style)
            {
                case Style.Classic:
                    return await skiRepository.GetItemAsync(skiSizeLogic.SizeClassic(lenght), style);
                case Style.Freestyle:
                    return await skiRepository.GetItemAsync(skiSizeLogic.SizeFreeStyle(lenght), style);
                default:
                    return null;
            }
        }



        private async Task<Ski> AgeCheck(int lenght, int age, Style style)
        {
            if (age <= 4)
            {
                return await skiRepository.GetItemAsync(lenght, style);
            }
            if (age <= 8)
            {
                return await skiRepository.GetItemAsync(skiSizeLogic.SizeUnderEight(lenght, age), style);
            }
            else return null;
        }
    }
}

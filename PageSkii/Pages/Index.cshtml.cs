using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAppSkiii.Logic.Interface;
using WebAppSkiii.Model;
using WebAppSkiii.Repository.Interface;
using WebAppSkiii.Service;

namespace PageSkii.Pages
{
    public class IndexModel : PageModel
    {
        [ViewData]
        public string SizeOfSki { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly ISkiRepository skiRepository;
        private readonly ISkiSizeLogic skiSizeLogic;

        public IndexModel(ILogger<IndexModel> logger, ISkiRepository skiRepository, ISkiSizeLogic skiSizeLogic)
        {
            _logger = logger;
            this.skiRepository = skiRepository;
            this.skiSizeLogic = skiSizeLogic;
        }

        public void OnGet()
        {

        }
        public async Task OnPost()
        {

            var lableLength = Request.Form["lableLength"].ToString();
            var lableAge = Request.Form["lableAge"].ToString();
            var selectStyle = Request.Form["selectStyle"].ToString();

            try
            {
                //Temp Solution
                var answer = GetAsync(lableLength, lableAge, selectStyle);
                //Call API
                // var answer = await GetTaskAsync();
                SizeOfSki = $"Rekommenderad storlek {answer.Length}cm";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to get recommendation: {ex}");
            }

        }
        //public async Task<Ski> GetTaskAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:44322/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = await client.GetAsync("api/Ski");
        //        if (response.IsSuccessStatusCode)
        //        {
        //          return repsonse;
        //        }
        //    }
        //}
        public Ski GetAsync(string length, string age, string selectStyle)
        {
            SkiService skiService = new SkiService(skiRepository, skiSizeLogic);
            var style = (Style)Enum.Parse(typeof(Style), selectStyle);
            var answer = skiService.GetSki(int.Parse(length), int.Parse(age), style).Result;
            return answer;
        }
    }
}

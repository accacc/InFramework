using IF.Core.Data;
using IF.Core.Log;
using IF.Core.Sms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.System.UI.SmsOneToMany.Pages
{

    //[Authorize]

    public class SmsListModel : PageModel
    {

        private readonly ISmsLogService logService;


        public PagedListResponse<SmsModel> SmsList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime BeginDate { get; set; }


        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }



        [BindProperty(SupportsGet = true)]
        public string BulkName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Number { get; set; }

        [BindProperty(SupportsGet = true)]
        public SmsState State { get; set; }



        public SmsListModel(ISmsLogService logservice)
        {
            this.logService = logservice;
            
            this.PageSize = 50;
            this.PageNumber = 1;
            this.BeginDate = DateTime.Now.Date.AddMonths(-6);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnGet()
        {

            await this.SetModel();

        }

        public async Task OnPost()
        {
            await SetModel();
        }



       

        private async Task SetModel()
        {
            this.SmsList = await logService.GetPaginatedSmsListAsync(this.BeginDate,this.EndDate, this.BulkName,this.Number,this.State, PageNumber, PageSize);
        }

      

        public async Task<PartialViewResult> OnGetSmsListPartial(string BulkName)
        {


            await SetModel();


            return new PartialViewResult
            {
                ViewName = "_SmsTable",
                ViewData = new ViewDataDictionary<List<SmsModel>>(ViewData, this.SmsList)
            };
        }
    }

}
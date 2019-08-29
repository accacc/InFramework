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

    public class SmsOneToManyOperationModel : PageModel
    {

        private readonly ISmsLogService logService;

        //public IEnumerable<ApplicationErrorLog> Logs { get; set; }

        public PagedListResponse<IFBulkOperation> Logs { get; set; }

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



        public SmsOneToManyOperationModel(ISmsLogService logservice)
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



        public async Task<PartialViewResult> OnGetSmsOneToManyOperationPartial()
        {

            await SetModel();


            return new PartialViewResult
            {
                ViewName = "_SmsOneToManyOperationTable",
                ViewData = new ViewDataDictionary<SmsOneToManyOperationModel>(ViewData, this)
            };
        }


        private async Task SetModel()
        {
            this.Logs = await logService.GetPaginatedSmsBulkOneToManyOperationAsync(this.BeginDate, this.EndDate, this.BulkName, PageNumber, PageSize);
        }

        public async Task<PartialViewResult> OnGetBatchs(string BulkName)
        {


            List<SmsBatchResult> list = await this.logService.GetSmsBulkResultOneToManyList(BulkName);


            return new PartialViewResult
            {
                ViewName = "_BatchTable",
                ViewData = new ViewDataDictionary<List<SmsBatchResult>>(ViewData, list)
            };
        }

      
    }

}
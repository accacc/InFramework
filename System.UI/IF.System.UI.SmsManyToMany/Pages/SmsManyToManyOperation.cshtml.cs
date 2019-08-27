using IF.Core.Data;
using IF.Core.Log;
using IF.Core.Sms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.System.UI.SmsManyToMany.Pages
{

    //[Authorize]

    public class SmsManyToManyOperationModel : PageModel
    {

        private readonly ISmsLogService logService;

        //public IEnumerable<ApplicationErrorLog> Logs { get; set; }

        public PagedListResponse<SmsBulkManyToManyOperation> Logs { get; set; }

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



        public SmsManyToManyOperationModel(ISmsLogService logservice)
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



        public async Task<PartialViewResult> OnGetSmsManyToManyOperationPartial()
        {

            await SetModel();


            return new PartialViewResult
            {
                ViewName = "_SmsManyToManyOperationTable",
                ViewData = new ViewDataDictionary<SmsManyToManyOperationModel>(ViewData, this)
            };
        }


        private async Task SetModel()
        {
            this.Logs = await logService.GetPaginatedSmsBulkManyToManyOperationAsync(this.BeginDate, this.EndDate, this.BulkName, PageNumber, PageSize);
        }

        public async Task<PartialViewResult> OnGetBatchs(string BulkName)
        {


            List<SmsBatchResult> list = await this.logService.GetSmsBulkResultManyToManyList(BulkName);


            return new PartialViewResult
            {
                ViewName = "_BatchTable",
                ViewData = new ViewDataDictionary<List<SmsBatchResult>>(ViewData, list)
            };
        }
    }

}
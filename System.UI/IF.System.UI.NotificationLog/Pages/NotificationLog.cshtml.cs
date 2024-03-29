﻿using IF.Core.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.System.UI.NotificationLog.Pages
{

    [Authorize]
    public class NotificationLogModel : PageModel
    {

        private readonly INotificationLogService notificationLogService;

        public IEnumerable<INotificationLog> Logs { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Logger { get; set; }
        public string UserId { get; set; }

        public NotificationLogModel(INotificationLogService notificationLogService)
        {
            this.notificationLogService = notificationLogService;            
            this.Take = 50;
            this.Skip = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-15);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnPost(DateTime BeginDate, DateTime EndDate, string logger, string UserId, int skip, int take)
        {
            if (skip >= 1 && take >= 50)
            {
                this.Take = take;
                this.Skip = skip;
                this.BeginDate = BeginDate;
                this.EndDate = EndDate;
                this.UserId = UserId;
                this.Logger = logger;
                var pages = await notificationLogService.GetPaginatedAsync(BeginDate, EndDate, UserId, logger, skip, take);

                this.Logs = pages.Data;
            }
        }

      
    }
}
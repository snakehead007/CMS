using CMS.Data.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Hubs
{
    public static class HubExtensions
    {
        public const string CourseChangedEvent = "CourseChanged";
        public static Task NotifyCourseChanged(this IHubContext<CourseHub> hubContext,Course course)
        {
            return hubContext.Clients.All.SendAsync(CourseChangedEvent, course);
        }
    }
}

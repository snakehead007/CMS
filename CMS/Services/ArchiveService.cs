using CMS.Data.Repositories;
using CMS.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class ArchiveService : BackgroundService, IHostedService
    {
        // 5 min * 60 sec * 1000ms
        public const int IntervalInMs = 1000 * 60 * 5;

        private readonly ILogger<ArchiveService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ArchiveService(ILogger<ArchiveService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Archive Service has started");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    _logger.LogInformation("Archive Process has started");

                    ICourseRepository courseRepository = scope.ServiceProvider.GetRequiredService<ICourseRepository>();
                    ArchiveRepository archiveRepository = scope.ServiceProvider.GetRequiredService<ArchiveRepository>();

                    //enddate courses bekijken
                    var list = await courseRepository.GetListAsync();

                    foreach (var course in list)
                    {
                        if (course.EndDate < DateTime.UtcNow) {
                            _logger.LogInformation("Archive Process is archiving Course with id: " + course.CourseId);

                            await archiveRepository.ArchiveCourseAsync(course.CourseId);
                        }
                    }
                    _logger.LogInformation("Archive Process has ended");

                }

                await Task.Delay(IntervalInMs, stoppingToken);
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Webadmin.Models;

namespace Webadmin.Services
{
    public class DatabaseCleanerService : IHostedService, IDisposable
    {
        private Timer timer;
        private IServiceScopeFactory scopeFactory;

        public DatabaseCleanerService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { //temp value 
            timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        public async void DoWork(object state)
        { 
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<COMP2003_FContext>();
                
                await dbContext.Database.ExecuteSqlRawAsync("EXEC delete_old_bookings");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        } 
        
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private cleanTableDbContext cleanTableDbContext;

        public DatabaseCleanerService(cleanTableDbContext cleanTableDb)
        {
            cleanTableDbContext = cleanTableDb;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { //temp value 
            timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        public async void DoWork(object state)
        { 
            await cleanTableDbContext.Database.ExecuteSqlRawAsync("EXEC deleteTimer");
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

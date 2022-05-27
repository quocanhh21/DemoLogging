using DemoLogging.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Linq;

namespace DemoLogging.Models
{
    public class EmployeeService
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("Start method Initialize in EmployeeService class");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Employees.Any())
                {
                    return;   // DB has been seeded
                }
                context.Employees.AddRange(
                    new Employee
                    {
                        Name = "Le Quoc Anh",
                        Address = "123 su van hanh",
                        PhoneNumber = "09337748547",
                        Email = "leanh@gmail.com"
                    },

                     new Employee
                     {
                         Name = "Le Quoc A",
                         Address = "123 cmt8",
                         PhoneNumber = "09444557384",
                         Email = "leanh123123@gmail.com"
                     }
                );
                context.SaveChanges();
            }
            timer.Stop();

            TimeSpan ts = timer.Elapsed;
            //format time
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            logger.Debug($"Finish method Initialize in EmployeeService class. Run time: {elapsedTime} ");
        }
    }
}

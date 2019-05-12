using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Models
{
    
    public class TasksDbSeeder
    {
        public static void Initialize(TasksDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any tasks.
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            context.Tasks.AddRange(
                new Task
                {
                    Title = "Booking Commision",
                    Description = "Verify booking commision",
                    DateAdded = DateTime.Now,
                    Deadline = new DateTime(2019, 5, 15, 12, 0, 0),
                    TaskImportance = (TaskImportance)1,
                    TaskState = (TaskState)1
                },
                new Task
                {
                    Title = "Booking Review",
                    Description = "Do the Booking review situation",
                    DateAdded = DateTime.Now,
                    Deadline = new DateTime(2019, 5, 20, 15, 30, 0),
                    TaskImportance = (TaskImportance)2,
                    TaskState = (TaskState)1
                }
            );
            context.SaveChanges(); // commit transaction
        }


    }
}

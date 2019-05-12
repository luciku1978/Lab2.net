using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laborator2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laborator2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksDbContext context;
        public TasksController(TasksDbContext context)
        {
            this.context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<Models.Task> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            IQueryable<Models.Task> result = context.Tasks.Include(t => t.Comments);

            if (from == null && to == null)
                return result;

            if (from != null)
                result = result.Where(t => t.Deadline >= from);

            if (to != null)
                result = result.Where(t => t.Deadline <= to);

            return result;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = context.Tasks.Include(t => t.Comments).FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }

        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] Models.Task task)
        {
            task.DateClosed = null;
            task.DateAdded = DateTime.Now;
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                task.DateClosed = null;
                task.DateAdded = DateTime.Now;
                context.Tasks.Add(task);
                context.SaveChanges();
                return Ok(task);
            }
            task.Id = id;
            if (task.TaskState == TaskState.Closed && existing.TaskState != TaskState.Closed)
                task.DateClosed = DateTime.Now;
            else if (existing.TaskState == TaskState.Closed && task.TaskState != TaskState.Closed)
                task.DateClosed = null;

            context.Tasks.Update(task);
            context.SaveChanges();
            return Ok(task);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = context.Tasks.Include(t => t.Comments).FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            context.Tasks.Remove(existing);
            context.SaveChanges();
            return Ok();
        }

    }
}
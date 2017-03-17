using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Kanban.Models;

namespace Kanban.Controllers
{
    public class TaskSetsApiController : ApiController
    {
        private ModelDb db = new ModelDb();

        // GET: api/TaskSetsWebApi
        public async Task<IEnumerable<TaskSet>> GetTaskSet()
        {
            return (await db.TaskSet.ToListAsync()).Select(x=>new TaskSet{ Card = x.Card, Id= x.Id, Name= x.Name });
            //return db.TaskSet;
        }

        // GET: api/TaskSetsWebApi/5
        [ResponseType(typeof(TaskSet))]
        public async Task<IHttpActionResult> GetTaskSet(int id)
        {
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            if (taskSet == null)
            {
                return NotFound();
            }

            return Ok(taskSet);
        }

        // PUT: api/TaskSetsWebApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTaskSet(int id, TaskSet taskSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskSet.Id)
            {
                return BadRequest();
            }

            db.Entry(taskSet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskSetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TaskSetsWebApi
        [ResponseType(typeof(TaskSet))]
        public async Task<IHttpActionResult> PostTaskSet(TaskSet taskSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskSet.Add(taskSet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = taskSet.Id }, taskSet);
        }

        // DELETE: api/TaskSetsWebApi/5
        [ResponseType(typeof(TaskSet))]
        public async Task<IHttpActionResult> DeleteTaskSet(int id)
        {
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            if (taskSet == null)
            {
                return NotFound();
            }

            db.TaskSet.Remove(taskSet);
            await db.SaveChangesAsync();

            return Ok(taskSet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskSetExists(int id)
        {
            return db.TaskSet.Count(e => e.Id == id) > 0;
        }
    }
}
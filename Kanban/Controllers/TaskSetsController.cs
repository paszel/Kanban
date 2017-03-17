using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kanban.Models;

namespace Kanban.Controllers
{
    public class TaskSetsController : Controller
    {
        private ModelDb db = new ModelDb();

        public async Task<JsonResult> GetAllTasks()
        {
            return Json(await db.TaskSet.ToListAsync(), JsonRequestBehavior.AllowGet);
        }
        
        // GET: TaskSets
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskSet.ToListAsync());
        }

        // GET: TaskSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            if (taskSet == null)
            {
                return HttpNotFound();
            }
            return View(taskSet);
        }

        // GET: TaskSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] TaskSet taskSet)
        {
            if (ModelState.IsValid)
            {
                db.TaskSet.Add(taskSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taskSet);
        }

        // GET: TaskSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            if (taskSet == null)
            {
                return HttpNotFound();
            }
            return View(taskSet);
        }

        // POST: TaskSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] TaskSet taskSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskSet);
        }

        // GET: TaskSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            if (taskSet == null)
            {
                return HttpNotFound();
            }
            return View(taskSet);
        }

        // POST: TaskSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskSet taskSet = await db.TaskSet.FindAsync(id);
            db.TaskSet.Remove(taskSet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

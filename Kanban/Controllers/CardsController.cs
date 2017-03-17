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
    public class CardsController : Controller
    {
        private ModelDb db = new ModelDb();

        // GET: Cards
        public async Task<ActionResult> Index()
        {
            var card = db.Card.Include(c => c.TaskSet);
            return View(await card.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Card.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            ViewBag.TaskSetId = new SelectList(db.TaskSet, "Id", "Name");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Priority,BeginDate,EndDate,TaskSetId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Card.Add(card);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TaskSetId = new SelectList(db.TaskSet, "Id", "Name", card.TaskSetId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Card.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskSetId = new SelectList(db.TaskSet, "Id", "Name", card.TaskSetId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Priority,BeginDate,EndDate,TaskSetId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TaskSetId = new SelectList(db.TaskSet, "Id", "Name", card.TaskSetId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Card.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Card card = await db.Card.FindAsync(id);
            db.Card.Remove(card);
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

﻿using System.Linq;
using System.Web.Mvc;
using FriendsMess.Models;

namespace FriendsMess.Controllers
{
    public class ExtraController : Controller
    {
        //
        // GET: /Extra/
        private ApplicationDbContext _context;

        public ExtraController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var extras = _context.OtherExpenses.ToList();
            return View(extras);
        }

        public ActionResult New()
        {
            ViewBag.status = "New Expense";
            var expense = new OtherExpense();
            return View("OtherExpense",expense);
        }
        [HttpPost]
        public ActionResult Save(OtherExpense expense)
        {
            if (!ModelState.IsValid)
                return View("OtherExpense", expense);
            if (expense.Id == 0)
                _context.OtherExpenses.Add(expense);
            else
            {
                var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == expense.Id);
                if (expenseInDb == null)
                    return HttpNotFound();
                expenseInDb.Name = expense.Name;
                expenseInDb.Amount = expense.Amount;
                expenseInDb.Description = expense.Description;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == id);
            if (expenseInDb == null)
                return HttpNotFound();
            ViewBag.status = "Edit Expense";
            return View("OtherExpense", expenseInDb);
        }

        public ActionResult Delete(int id)
        {
            var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == id);
            if (expenseInDb == null)
                return HttpNotFound();
            _context.OtherExpenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
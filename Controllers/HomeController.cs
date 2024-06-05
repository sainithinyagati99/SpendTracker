using Microsoft.AspNetCore.Mvc;
using SpendTracker.Models;
using System.Diagnostics;

namespace SpendTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendTrackerDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allexpenses = _context.Expenses.ToList();
            var totalexpenses = allexpenses.Sum(x => x.Expense);
            ViewBag.Expenses = totalexpenses;
            return View(allexpenses);
        }
        public IActionResult CreateEditExpenses(int? id)
        {
            if (id != null) {
                var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
                return View(expenseInDb);
            }
            return View();
        }
        public IActionResult DeleteExpense(int? id) {
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expenses model)
        {
            if(model.Id==0) {
                _context.Expenses.Add(model);
            }
            else
            {
                _context.Expenses.Update(model);
            }
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

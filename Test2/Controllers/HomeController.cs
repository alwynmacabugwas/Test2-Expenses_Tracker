using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test2.Models;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Test2DbContext _context;
        private readonly IWebHostEnvironment env;

        public HomeController(ILogger<HomeController> logger, Test2DbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(x => x.Price);
            ViewBag.Expenses = totalExpenses;
            return View(allExpenses);
        }

        public IActionResult CreateExpense()
        {
            return View();
        }

        [Authorize]
        public IActionResult EditExpense(int? id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return RedirectToAction("Expenses");
            }

            var expenseDto = new ExpenseDto()
            {
                Id = expense.Id,
                Description = expense.Description,
                Price = expense.Price,
                Email = expense.Email,
                Category = expense.Category
            };

            ViewData["ImageName"] = expense.ImageName;


            return View(expenseDto);
        }
        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }
        [HttpPost]
        public IActionResult CreateEditExpenseForm(ExpenseDto model)
        {
            if (model.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Please select an image");
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(model.ImageFile!.FileName);

            string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                model.ImageFile.CopyTo(stream);
            }

            Expense expense = new Expense()
            {
                Id = model.Id,
                Description = model.Description,
                Price = model.Price,
                ImageName = newFileName,
                Email = model.Email,
                Category = model.Category
            };         

            if (expense.Id == 0)
            {
                _context.Expenses.Add(expense);
            }
            else
            {
                _context.Expenses.Update(expense);
            }
            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }
        [HttpPost]
        public IActionResult EditExpenseForm(int id, ExpenseDto expenseDto)
        {
            var expense = _context.Expenses.Find(id);

            if (expense == null)
            {
                return RedirectToAction("Expenses");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ImageName"] = expense.ImageName;

                return View(expenseDto);
            }

            string newFileName = expense.ImageName;
            if (expenseDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(expenseDto.ImageFile.FileName);

                string imageFullPath = env.WebRootPath + "\\images\\" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    expenseDto.ImageFile.CopyTo(stream);
                }

                string oldImagePath = env.WebRootPath + "\\images\\" + expense.ImageName;
                System.IO.File.Delete(oldImagePath);
            }

            expense.ImageName = newFileName;
            expense.Email = expenseDto.Email;
            expense.Price = expenseDto.Price;
            expense.Category = expenseDto.Category;
            expense.Description = expenseDto.Description;

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

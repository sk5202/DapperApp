using Dapper;
using DapperApp.Domain.Entities;
using DapperApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DapperApp.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration _configuration;

            public HomeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            IList<User> model = new List<User>();

            var aa = _configuration.GetConnectionString("SqlConnection");

            using (IDbConnection db = new SqlConnection(aa))
            {
                string selectQuery = @"SELECT * FROM [dbo].[User]";

                model = db.Query<User>(selectQuery).ToList();
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormCollection form)
        {
            var name = form["Name"];
            if(!string.IsNullOrEmpty(name))
            {
                var aa = _configuration.GetConnectionString("SqlConnection");

                using (IDbConnection db = new SqlConnection(aa))
                {
                    string insertQuery = @"INSERT INTO [dbo].[User]([Name]) VALUES (@Name)";

                    await  db.ExecuteAsync(insertQuery, new
                    {
                        name
                    });
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
<<<<<<< HEAD
﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bumbo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace bumbo.Controllers;

public class NormsController : Controller
{
    private readonly ILogger<NormsController> _logger;
    private readonly IConfiguration _configuration;

    public NormsController(ILogger<NormsController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public void Insert(Norm norm)
    {
        string connectionString = _configuration.GetConnectionString("jules");

        using (SqlConnection conn = new SqlConnection(connectionString)) 
        { 
            conn.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Norm (week, year, branchId, activity, normInSeconds) VALUES (@Week, @Year, @BranchId, @Activity, @NormInSeconds)", conn);
            command.Parameters.AddWithValue("@Week", norm.week);
            command.Parameters.AddWithValue("@Year", norm.year);
            command.Parameters.AddWithValue("@BranchId", norm.branchId);
            command.Parameters.AddWithValue("@Activity", norm.activity);
            command.Parameters.AddWithValue("@NormInSeconds", norm.normInSeconds);
            command.ExecuteNonQuery();
        }

    }
}

=======
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class NormsController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public NormsController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }
    }
}
>>>>>>> 8e5722d6ee676189c31dc400999d4f2fe6df2b54

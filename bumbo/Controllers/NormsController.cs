using System.Diagnostics;
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


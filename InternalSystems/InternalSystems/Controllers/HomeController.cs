using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InternalSystems.Models;
using Microsoft.Data.SqlClient;
using InternalSystems.Service;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace InternalSystems.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        ServicePointManager.ServerCertificateValidationCallback =
            new RemoteCertificateValidationCallback(ValidateServerCertificate);

        var sqlDataService = new SqlDataService(_configuration);
        var connection = new SqlConnection();
        connection.ConnectionString = sqlDataService.GetConnectionString();

        //open
        connection.Open();

        //commmand
        var command = connection.CreateCommand();
        command.CommandText = @"select * from test_table";

        //reader
        var reader = command.ExecuteReader();

        //loop & write
        while (reader.Read() == true)
        {
            Console.WriteLine(reader["id"] + " " + reader["name"]);
        }

        //dispose reader
        reader.Dispose();
        //dispose command
        command.Dispose();
        //close connection
        connection.Close();

        return View();
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

    public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
        {
            return true;
        }

        if (sender is System.Net.WebRequest)
        {
            System.Net.WebRequest Request = (System.Net.WebRequest)sender;

            switch (Request.RequestUri.Host)
            {
                case "localhost":
                    return true;
            }
        }

        return false;
    }
}


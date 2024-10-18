using DataLayer.Interfaces;
using DataLayer.Repositories;

namespace DataLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.Run();
        }
    }
}

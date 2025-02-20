using VAT_AT_Calc.API.Contracts.Services;
using VAT_AT_Calc.API.Middlewares;
using VAT_AT_Calc.API.Models.Options;
using VAT_AT_Calc.API.Services;

namespace VAT_AT_Calc.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOptions();

            // IOptions - DI
            builder.Services.Configure<ValidValueAddedTaxRatesOptions>(builder.Configuration.GetSection(ValidValueAddedTaxRatesOptions.ValidValueAddedTaxRates));

            // Services - DI
            builder.Services.AddTransient<ICalculatorService, CalculatorService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowed(origin => true)
                               .AllowCredentials()
                               .Build();
                    });
            });


            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors("default");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

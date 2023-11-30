using ManagingListUsers.Data;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ManagingListUsers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<DbInitializer>();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                   new Microsoft.OpenApi.Models.OpenApiInfo
                   {
                       Title = "Swagger",
                       Description = "Swagger Document",
                       Version = "v1"
                   });
                var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                var filepath = Path.Combine(AppContext.BaseDirectory, filename);
                options.IncludeXmlComments(filepath, true);
            });

            var app = builder.Build();

            SeedData(app);
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "ManagingListUsers");
                });
            }
            app.MapControllers();

            app.Run();
        }
        static void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<DbInitializer>();
                service.Initialize();
            }
        }
    }
}
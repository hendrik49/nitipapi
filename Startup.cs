using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using nitipApi.DataAccess;
using nitipApi.Repositroy;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using MySql.Data.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace nitipApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public bool IsServerConnected(string connection)
        {
            using (var l_oConnection = new SqlConnection(connection))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=localhost,1433;Database=NitipDB; User ID=sa;Password=Hendrik49;MultipleActiveResultSets=true";
            var mysqlconnection = @"server=localhost; port=3306;Database=NitipDB;User ID=root;Password=password;sslmode=none;";

            //if (IsServerConnected(connection))
                //services.AddDbContext<NitipContext>(options => options.UseSqlServer(connection));
            //else if (IsServerConnected(mysqlconnection))
                services.AddDbContext<NitipContext>(options => options.UseMySQL(mysqlconnection));
            //else
                //services.AddDbContext<NitipContext>(opt => opt.UseInMemoryDatabase());


            services.AddMvc();
            services.AddScoped<INitipRepository, NitipRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}

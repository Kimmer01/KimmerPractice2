
using EFCorePractice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationWebAPI.Filters;

namespace WebApplicationWebAPI
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

            builder.Services.AddMemoryCache();//IMemoryCache, 建议使用 GetOrCreateAsync，可以避免缓存穿透问题(查询结果为 null，导致每次都访问数据库)
            //缓存雪崩(缓存项集中过期，导致对数据库的访问，每隔一段时间就剧增。解决方案：再基础过期时间之上，再加一个随机过期时间)

            //Scoped
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                string connStr = builder.Configuration.GetSection("ConnectionString").Value;
                opt.UseSqlServer(connStr);
            });

            //TODO: 什么时候会触发 Resource Filter, Result Filter, Authorization Filter
            builder.Services.Configure<MvcOptions>(opt =>
            {
                //按照注册顺序，倒序触发
                opt.Filters.Add<MyExceptionFilter>();
                opt.Filters.Add<LogExceptionFilter>();

                opt.Filters.Add<MyActionFilter>();
                opt.Filters.Add<RateLimitFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseResponseCaching();
            app.MapControllers();

            string conn = app.Configuration.GetSection("ConnectionStr").Value;
            Console.WriteLine(conn);

            app.Run();
        }
    }
}
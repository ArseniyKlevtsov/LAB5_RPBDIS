using RailwayTrafficSolution.Data;
using RailwayTrafficSolution.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayTrafficSolution.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        // Метод Invoke вызывается при обработке запроса
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, RailwayTrafficContext dbContext)
        {
            // Проверяем, установлен ли ключ "starting" в сессии
            if (!(context.Session.Keys.Contains("starting")))
            {
                DbInitializer dbInitializer = new DbInitializer();
                // Инициализируем базу данных через DbInitializer
                dbInitializer.InitializeDatabase(dbContext);

                // Устанавливаем ключ "starting" в "Yes", чтобы избежать повторной инициализации
                context.Session.SetString("starting", "Yes");
            }

            // Вызываем следующий элемент в конвейере запросов
            return _next.Invoke(context);
        }
    }
    // Класс-расширение для добавления DbInitializerMiddleware в конвейер запросов
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}

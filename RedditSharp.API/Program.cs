using RedditSharp.API.BusinessLogicLayer;
using RedditSharp.API.DataStore;
using RedditSharp.API.Helper;
using RedditSharp.API.MiddleWare;
using RedditSharp.API.RedditHelper;
using RedditSharp.API.Repository;
using RedditSharp.API.ViewModel;

namespace RedditSharp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IWebAgent>(
                new BotWebAgent(
                    builder.Configuration["TestUserName"],
                    builder.Configuration["TestUserPassword"],
                    builder.Configuration["RedditClientID"],
                    builder.Configuration["RedditClientSecret"],
                    builder.Configuration["RedditRedirectURI"]));

            builder.Services.AddSingleton<Reddit>();

            builder.Services.AddSingleton<IFileWriter, FileWriter>();
            builder.Services.AddSingleton<IDataStore<string, PostModel>, InMemoryDataStore<string, PostModel>>();

            builder.Services.AddScoped<IRepository<PostModel>, Repository<PostModel>>();
            builder.Services.AddScoped<IRedditPostService, RedditPostService>();
            builder.Services.AddScoped<IRedditSharpClient, RedditSharpClient>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Global Exception Handler
            app.UseGlobalExceptionHandler();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

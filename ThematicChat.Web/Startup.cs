using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThematicChat.Core.ChatBoard;
using ThematicChat.Core.Chatting.Interfaces;
using ThematicChat.Core.Chatting.UseCases.JoinChat;
using ThematicChat.Core.Chatting.UseCases.LeaveChat;
using ThematicChat.Core.Chatting.UseCases.PublishChat;
using ThematicChat.Core.Chatting.UseCases.SendMessage;
using ThematicChat.Core.Chatting.UseCases.UnpublishChat;
using ThematicChat.Infrastructure.Repositories;
using ThematicChat.Web.Hubs;
using ThematicChat.Web.Services;

namespace ThematicChat.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(PublishChatHandler), typeof(UnpublishChatHandler), typeof(JoinChatHandler),
                typeof(LeaveChatHandler), typeof(SendMessageHandler));

            services.AddSingleton<IChatBoardRepository, ChatBoardRepository>();
            services.AddSingleton<IChatRepository, ChatRepository>();

            services.AddTransient<IUserNotifier, SignalRUserNotifier>();
            services.AddTransient<IUserInfoProvider, SignalRUserInfoProvider>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}

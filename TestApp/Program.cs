using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Core;
using TestApp.Services;
using TestApp.StaticObjects;
using TestApp.ViewModels;

namespace TestApp
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Palette.AddColor(AppColor.Black, new SKColor(0, 0, 0));
            Palette.AddColor(AppColor.Red, new SKColor(255, 0, 0));
            Palette.AddColor(AppColor.Gray, new SKColor(196, 196, 196));

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                })
                .Build();

            var app = host.Services.GetService<App>();
            app.Resources["Locator"] = new LocatorFactory(new IoCContainerFactory()).GetLocator();
            app.StartMainWindow(new MainWindow());
            app?.Run();
        }
    }
}

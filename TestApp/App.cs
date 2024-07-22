using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    using System.Windows;
    using TestApp.Core;

    public class App : Application
    {
        public App()
        {

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public void StartMainWindow(MainWindow mainWindow) 
        {
            mainWindow.Show();
        }
    }
}

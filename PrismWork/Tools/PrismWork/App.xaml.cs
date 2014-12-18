using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace PrismWork
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            App.RunMode();
        }

        static void RunMode()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                WorkBootstrapper bootstrapper = new WorkBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception exception)
            {
                Application.Current.Dispatcher.InvokeShutdown();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {

            }
            Application.Current.Dispatcher.InvokeShutdown();
        }
    }
}

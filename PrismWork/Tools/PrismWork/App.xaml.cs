using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using CommonHelper.Helpers;
using WorkCommon.Manager;

namespace PrismWork
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static MemoryMappedFileHelper<EditMemoryFile> EditMemory = new MemoryMappedFileHelper<EditMemoryFile>("PrismWork");

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (this.MainWindow != null)
            {
                this.MainWindow.SourceInitialized += MainWindow_SourceInitialized;
            }

            App.RunMode();
        }

        void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            var window = sender as Window;
            if (window != null && App.EditMemory != null)
            {
                IntPtr hwnd = new WindowInteropHelper(window).Handle;
                EditMemoryFile editMomoryFile = EditMemory.LoadMemoryFile();
            }
            //Editor.ControlLib.OpenEdiotr.EdiotrHelper.ShowWindow(newEditFile.Handle);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (EditMemory != null)
            {
                EditMemory.DisposeMemoryMappedFile();
            }

            base.OnExit(e);
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
                if (exception != null)
                {
                    GlobalLog.Logger.Info("Process Over... ", exception);
                }
                Application.Current.Dispatcher.InvokeShutdown();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                GlobalLog.Logger.Error("Process Over... ", exception);
            }
            Application.Current.Dispatcher.InvokeShutdown();
        }
    }
}

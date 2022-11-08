using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF_LoginForm.View;

namespace WPF_LoginForm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, sv) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    mainView.Show();
                    loginView.Close();
                }
            };

            //var mainView = new MainView();
            //mainView.Show();

        }

        private string fileLog = @"C:/temp/WPF_LoginForm.log";
        public App()
        {
            this.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
            try
            {
                //App.Settings = new Settings();
                //App.Settings.Load(); // this creates default settings.json file if does not exist
            }
            catch (Exception exception)
            {
                this.SaveException(exception);
                App.Current.Shutdown();
            }
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.SaveException(e.Exception);
            MessageBox.Show($"Aplikacja zostanie zamknieta. Wystapil nie oczekiwany blad!" +
                $"Sprawdz plik {fileLog}", "UWAGA!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SaveException(Exception exception)
        {
            string text = $"{exception.Message}{Environment.NewLine}{exception.StackTrace}";
            File.WriteAllText(fileLog, text);
        }
    }
}

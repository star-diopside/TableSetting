using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Unity;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using TableSetting.Wpf.Services;
using TableSetting.Wpf.Views;
using Unity;
using Unity.Microsoft.Logging;

namespace TableSetting.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    "logs",
                                    Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(path, "debug.log"), restrictedToMinimumLevel: LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
                .WriteTo.File(Path.Combine(path, "error.log"), restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().AddExtension(new LoggingExtension(new LoggerFactory().AddSerilog()));
            containerRegistry.RegisterDialog<EditConnectionString>();
            containerRegistry.RegisterSingleton<IOpenFileService, OpenFileService>();
            containerRegistry.RegisterSingleton<ISaveFileService, SaveFileService>();
            containerRegistry.RegisterSingleton<IMessageBoxService, MessageBoxService>();
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            Log.Error(e.Exception, e.Exception.Message);
            e.Handled = true;
        }
    }
}

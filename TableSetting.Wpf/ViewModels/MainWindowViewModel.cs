using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Npgsql;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TableSetting.Wpf.Models;
using TableSetting.Wpf.Properties;
using TableSetting.Wpf.Services;
using TableSetting.Wpf.Views;

namespace TableSetting.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly ILogger<MainWindowViewModel> _logger;
        private readonly IDialogService _dialogService;
        private readonly IOpenFileService _openFileService;
        private readonly ISaveFileService _saveFileService;
        private readonly IMessageBoxService _messageBoxService;

        public List<string> DbProviders { get; }
        public ReactiveProperty<string?> SelectedDbProvider { get; } = new();
        public ObservableCollection<ConnectionSetting> ConnectionSettings { get; } = new();
        public ReactiveProperty<ConnectionSetting?> SelectedConnectionSetting { get; } = new();
        public ReactiveProperty<DataTable> DbSchema { get; } = new();
        public ReactiveProperty<DataTable> DbTables { get; } = new();

        public ICommand ClosedWindowCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveLogFileCommand { get; }
        public ICommand AddConnectionStringItemCommand { get; }
        public ICommand EditConnectionStringItemCommand { get; }
        public ICommand RemoveConnectionStringItemCommand { get; }
        public ICommand ResetConnectionSettingsCommand { get; }
        public ICommand CheckConnectCommand { get; }

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger,
                                   IDialogService dialogService,
                                   IOpenFileService openFileService,
                                   ISaveFileService saveFileService,
                                   IMessageBoxService messageBoxService)
        {
            _logger = logger;
            _dialogService = dialogService;
            _openFileService = openFileService;
            _saveFileService = saveFileService;
            _messageBoxService = messageBoxService;

            DbProviderFactories.RegisterFactory("Odbc", OdbcFactory.Instance);
            DbProviderFactories.RegisterFactory("OleDb", OleDbFactory.Instance);
            DbProviderFactories.RegisterFactory("SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactories.RegisterFactory("MySqlClient", MySqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("SQLite", SQLiteFactory.Instance);

            DbProviders = DbProviderFactories.GetFactoryClasses()
                                             .AsEnumerable()
                                             .Select(row => row.Field<string>("InvariantName") ?? string.Empty)
                                             .OrderBy(name => name)
                                             .ToList();

            SelectedDbProvider.Value = Settings.Default.ApplicationSettings.DbProviderName;
            ConnectionSettings.AddRange(Settings.Default.ApplicationSettings.ConnectionSettings);

            ClosedWindowCommand = new DelegateCommand(ClosedWindow);
            OpenFileCommand = new AsyncReactiveCommand().WithSubscribe(OpenFileAsync).AddTo(_disposable);
            SaveFileCommand = new AsyncReactiveCommand().WithSubscribe(SaveFileAsync).AddTo(_disposable);
            SaveLogFileCommand = new DelegateCommand(SaveLogFile);
            AddConnectionStringItemCommand = new DelegateCommand(AddConnectionStringItem);
            EditConnectionStringItemCommand = SelectedConnectionSetting.Select(s => s is not null)
                                                                       .ToReactiveCommand()
                                                                       .WithSubscribe(EditConnectionStringItem)
                                                                       .AddTo(_disposable);
            RemoveConnectionStringItemCommand = SelectedConnectionSetting.Select(s => s is not null)
                                                                         .ToReactiveCommand()
                                                                         .WithSubscribe(RemoveConnectionStringItem)
                                                                         .AddTo(_disposable);
            ResetConnectionSettingsCommand = new DelegateCommand(ResetConnectionSettings);
            CheckConnectCommand = SelectedDbProvider.Select(s => s is not null)
                                                    .ToAsyncReactiveCommand()
                                                    .WithSubscribe(CheckConnectAsync)
                                                    .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void ClosedWindow()
        {
            Settings.Default.ApplicationSettings = new()
            {
                DbProviderName = SelectedDbProvider.Value ?? string.Empty,
                ConnectionSettings = ConnectionSettings.ToList()
            };
            Settings.Default.Save();
        }

        private async Task OpenFileAsync()
        {
            string? file = _openFileService.SelectOpenFile(new FileFilter[]
            {
                new() { Description = "設定ファイル", Extensions = new[] { "*.json" } },
                new() { Description = "すべてのファイル", Extensions = new[] { "*.*" } }
            });

            if (file is not null)
            {
                var bytes = await File.ReadAllBytesAsync(file);
                var settings = JsonSerializer.Deserialize<ApplicationSettings>(bytes) ?? throw new InvalidDataException();

                SelectedDbProvider.Value = settings.DbProviderName;
                ConnectionSettings.Clear();
                ConnectionSettings.AddRange(settings.ConnectionSettings);
            }
        }

        private async Task SaveFileAsync()
        {
            string? file = _saveFileService.SelectSaveFile(new FileFilter[]
            {
                new() { Description = "設定ファイル", Extensions = new[] { "*.json" } },
                new() { Description = "すべてのファイル", Extensions = new[] { "*.*" } }
            });

            if (file is not null)
            {
                var settings = new ApplicationSettings
                {
                    DbProviderName = SelectedDbProvider.Value ?? string.Empty,
                    ConnectionSettings = ConnectionSettings.ToList()
                };

                await File.WriteAllBytesAsync(file, JsonSerializer.SerializeToUtf8Bytes(settings, new()
                {
                    WriteIndented = true
                }));
            }
        }

        private void SaveLogFile()
        {
            _saveFileService.SelectSaveFile();
        }

        private void AddConnectionStringItem()
        {
            var parameters = new DialogParameters
            {
                { "Title", "項目の追加" }
            };

            _dialogService.ShowDialog(nameof(EditConnectionString), parameters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var setting = result.Parameters.GetValue<ConnectionSetting>(nameof(ConnectionSetting));
                    ConnectionSettings.Add(setting);
                }
            });
        }

        private void EditConnectionStringItem()
        {
            if (SelectedConnectionSetting.Value is null)
            {
                throw new InvalidOperationException();
            }

            var parameters = new DialogParameters
            {
                { "Title", "項目の編集" },
                { nameof(ConnectionSetting), SelectedConnectionSetting.Value }
            };

            _dialogService.ShowDialog(nameof(EditConnectionString), parameters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var setting = result.Parameters.GetValue<ConnectionSetting>(nameof(ConnectionSetting));
                    SelectedConnectionSetting.Value.Key = setting.Key;
                    SelectedConnectionSetting.Value.Value = setting.Value;
                    SelectedConnectionSetting.Value.Enable = setting.Enable;
                }
            });
        }

        private void RemoveConnectionStringItem()
        {
            if (SelectedConnectionSetting.Value is null)
            {
                throw new InvalidOperationException();
            }

            if (_messageBoxService.ShowMessage("選択された項目を削除してもよろしいですか？",
                                               "確認",
                                               MessageBoxButton.OKCancel,
                                               MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                ConnectionSettings.Remove(SelectedConnectionSetting.Value);
            }
        }

        private void ResetConnectionSettings()
        {
            if (_messageBoxService.ShowMessage("設定をリセットします。よろしいですか？",
                                               "確認",
                                               MessageBoxButton.OKCancel,
                                               MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Settings.Default.Reset();
                SelectedDbProvider.Value = Settings.Default.ApplicationSettings.DbProviderName;
                ConnectionSettings.Clear();
                ConnectionSettings.AddRange(Settings.Default.ApplicationSettings.ConnectionSettings);
            }
        }

        private async Task CheckConnectAsync()
        {
            if (SelectedDbProvider.Value is null)
            {
                throw new InvalidOperationException();
            }

            var factory = DbProviderFactories.GetFactory(SelectedDbProvider.Value);
            var connection = factory.CreateConnection() ?? throw new NotImplementedException();
            var builder = factory.CreateConnectionStringBuilder() ?? throw new NotImplementedException();

            foreach (var setting in ConnectionSettings.Where(s => s.Enable))
            {
                builder[setting.Key] = setting.Value;
            }

            connection.ConnectionString = builder.ConnectionString;

            _logger.LogDebug("Connection: {@Connection}", connection);

            DbSchema.Value = await connection.GetSchemaAsync();
            DbTables.Value = await connection.GetSchemaAsync("Tables");
        }
    }
}

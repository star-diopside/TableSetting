using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using TableSetting.Wpf.Models;

namespace TableSetting.Wpf.ViewModels
{
    public class EditConnectionStringViewModel : BindableBase, IDialogAware
    {
        private readonly CompositeDisposable _disposable = new();

        public string Title { get; private set; } = string.Empty;
        public ReactiveProperty<string> Key { get; } = new();
        public ReactiveProperty<string> Value { get; } = new();
        public ReactiveProperty<bool> Enable { get; } = new();

        public event Action<IDialogResult>? RequestClose;

        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

        public EditConnectionStringViewModel()
        {
            OKCommand = Key.Select(s => !string.IsNullOrEmpty(s))
                           .ToReactiveCommand()
                           .WithSubscribe(OK)
                           .AddTo(_disposable);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() => _disposable.Dispose();

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.TryGetValue(nameof(Title), out string title))
            {
                Title = title;
            }

            if (parameters.TryGetValue(nameof(ConnectionSetting), out ConnectionSetting setting))
            {
                Key.Value = setting.Key;
                Value.Value = setting.Value;
                Enable.Value = setting.Enable;
            }
        }

        private void OK() => RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters
        {
            {
                nameof(ConnectionSetting),
                new ConnectionSetting
                {
                    Key = Key.Value,
                    Value = Value.Value,
                    Enable = Enable.Value
                }
            }
        }));

        private void Cancel() => RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
    }
}

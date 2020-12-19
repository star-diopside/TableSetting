using System.Windows;

namespace TableSetting.Wpf.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}

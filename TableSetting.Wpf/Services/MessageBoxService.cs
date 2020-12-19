using System.Windows;

namespace TableSetting.Wpf.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(text, caption, button, icon);
        }
    }
}

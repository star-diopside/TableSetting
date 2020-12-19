using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace TableSetting.Wpf.Views
{
    public class WindowCloseAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            Window.GetWindow(AssociatedObject).Close();
        }
    }
}

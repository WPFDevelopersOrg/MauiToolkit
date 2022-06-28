using System.Windows.Input;

namespace Maui.Toolkit.Controls;

public class SettingsControl : View
{
    public ICommand? Command { get; set; }
    public object? CommandParameter { get; set; }
}

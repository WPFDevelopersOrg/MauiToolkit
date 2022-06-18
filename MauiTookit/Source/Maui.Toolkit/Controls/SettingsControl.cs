using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.Toolkit.Controls;
public class SettingsControl : View
{
    public ICommand? Command { get; set; }
    public object? CommandParameter { get; set; }
}

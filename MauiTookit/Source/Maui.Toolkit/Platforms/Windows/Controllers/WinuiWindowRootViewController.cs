using Maui.Toolkit.Controls;
using Maui.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Toolkit.Platforms.Windows.Controllers;
internal class WinuiWindowRootViewController : INavigationViewBuilder
{
    //public 

    string? INavigationViewBuilder.Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    string? INavigationViewBuilder.AppIcon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Color? INavigationViewBuilder.ViewBackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Brush? INavigationViewBuilder.ViewBackground { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Color? INavigationViewBuilder.ViewContentBackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Brush? INavigationViewBuilder.ViewContentBackground { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool INavigationViewBuilder.IsToggleButtonVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Button INavigationViewBuilder.BackButton { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    SettingsControl? INavigationViewBuilder.Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    SearchBar? INavigationViewBuilder.SearchBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    double INavigationViewBuilder.ViewTitleBarHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    double INavigationViewBuilder.ViewTileBarFontSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    object? INavigationViewBuilder.SelectedItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    event EventHandler INavigationViewBuilder.SelectionChanged
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    event EventHandler INavigationViewBuilder.ItemInvoked
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }
}

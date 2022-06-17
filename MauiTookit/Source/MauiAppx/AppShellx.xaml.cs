using Maui.Toolkit.Helpers;
using MauiAppx.Views;

namespace MauiAppx;

public partial class AppShellx : Shell
{
    public AppShellx()
    {
        InitializeComponent();

        //Routing.RegisterRoute("HomeRouter", typeof(HomePage));

#if WINDOWS

        if (Resources.TryGetValue("FlyoutHeaderKey", out var value))
            FlyoutHeader = value;

#elif MACCATALYST
        
        if (Resources.TryGetValue("ShellItemTemplateKey", out var value))
        {
            if (value is DataTemplate dataTemplate)
                ItemTemplate = dataTemplate;
        }

#endif

    }
}
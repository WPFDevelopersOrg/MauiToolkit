using Maui.Toolkit.Helpers;
using Maui.Toolkit.Services;
using Maui.Toolkit.Shared;
using System;

namespace MauiAppx.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        _WindowsService = ServiceProviderHelper.GetService<IWindowsService>();

        _StatusBarService = ServiceProviderHelper.GetService<IStatusBarService>();
    }

    readonly IWindowsService? _WindowsService;
    readonly IStatusBarService? _StatusBarService;

    IDisposable? _Dispasable = default;

    private void ShowDialogClick(object sender, EventArgs e)
    {
        Window window = new Window(new HomePage());
        Application.Current?.OpenWindow(window);
    }

    private void ChangeTitleBarClick(object sender, EventArgs e)
    {
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.ExtendsContentIntoTitleBar);
        _WindowsService?.SetTitleBar(WindowTitleBarKind.PlatformDefault);
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.PlatformDefault);
    }

    private void ChangeTitleBarClick1(object sender, EventArgs e)
    {
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.ExtendsContentIntoTitleBar);
        _WindowsService?.SetTitleBar(WindowTitleBarKind.Default);
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.PlatformDefault);
    }

    private void ChangeTitleBarClick2(object sender, EventArgs e)
    {
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.ExtendsContentIntoTitleBar);
        _WindowsService?.SetTitleBar(WindowTitleBarKind.CustomTitleBarAndExtension);
        //_WindowsService?.SetTitleBar(WindowTitleBarKind.PlatformDefault);
    }

    private void ChangeWindowSizeClick(object sender, EventArgs e)
    {
        _WindowsService?.ResizeWindow(new Size(1920, 1080));
    }

    private void MaxWindowClick(object sender, EventArgs e)
    {
        _WindowsService?.SetWindowMaximize();
    }

    private void MinWindowClick(object sender, EventArgs e)
    {
        _WindowsService?.SetWindowMinimize();
    }

    private void FullScreenClick(object sender, EventArgs e)
    {
        _WindowsService?.SwitchWindow(true);
    }

    private void DefaultScreenClick(object sender, EventArgs e)
    {
        _WindowsService?.SwitchWindow(false);
    }

    private void BackdropClick(object sender, EventArgs e)
    {
        _WindowsService?.SetBackdrop(BackdropsKind.Acrylic);
    }

    private void TrayBlinkClick(object sender, EventArgs e)
{
        _Dispasable = _StatusBarService?.SchedulePeriodic(TimeSpan.FromMilliseconds(300), default);
    }

    private void TrayStopBlinkClick(object sender, EventArgs e)
    {
        _Dispasable?.Dispose();
    }

    private void TrayShowClick(object sender, EventArgs e)
    {

    }

    private void TrayCloseClick(object sender, EventArgs e)
    {

    }

    private void TrayDescriptionClick(object sender, EventArgs e)
    {
        _StatusBarService?.SetDescription("10");
    }


}
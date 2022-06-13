﻿namespace MauiAppx;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

#if ANDROID || IOS
        MainPage = new AppShell();
#else
        MainPage = new AppShellx();
#endif

    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return base.CreateWindow(activationState);
    }
}

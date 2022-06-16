using Maui.Toolkit.Shared;

namespace Maui.Toolkit.Services;
public interface IWindowsService
{
    /// <summary>
    /// can control titlebar
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    bool SetTitleBar(WindowTitleBarKind kind);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    bool SetBackdrop(BackdropsKind kind);

    /// <summary>
    /// Max window
    /// </summary>
    /// <returns></returns>
    bool SetWindowMaximize();

    /// <summary>
    /// Min window
    /// </summary>
    /// <returns></returns>
    bool SetWindowMinimize();

    /// <summary>
    /// restore window
    /// </summary>
    /// <returns></returns>
    bool RestoreWindow();

    /// <summary>
    /// resize window
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    bool ResizeWindow(Size size);

    /// <summary>
    /// fullscreen or not
    /// </summary>
    /// <param name="fullScreen"></param>
    /// <returns></returns>
    bool SwitchWindow(bool fullScreen);
}

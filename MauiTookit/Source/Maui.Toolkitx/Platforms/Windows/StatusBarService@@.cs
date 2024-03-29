﻿using Maui.Toolkitx.Platforms.Windows.Runtimes;
using Maui.Toolkitx.Platforms.Windows.Runtimes.Shell32;
using Maui.Toolkitx.Platforms.Windows.Runtimes.User32;
using Microsoft.UI.Xaml.Controls.Primitives;
using PInvoke;
using static PInvoke.User32;

namespace Maui.Toolkitx;

internal partial class StatusBarService
{
    unsafe bool RegisterClass()
    {
        _StatusBarWindowClassName = $"StatusBar_Silent_{Guid.NewGuid()}";
        var wndclass = new WNDCLASS
        {
            style = 0,
            lpfnWndProc = _WndProc,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = IntPtr.Zero,
            hIcon = IntPtr.Zero,
            hCursor = IntPtr.Zero,
            hbrBackground = IntPtr.Zero,
            lpszMenuName = null,
            lpszClassName = (char*)(void*)Marshal.StringToCoTaskMemUni(_StatusBarWindowClassName),
        };

        User32.RegisterClass(ref wndclass);
        _WmStatusBarCreated = RegisterWindowMessage("StatusBarCreated");
        _StatusBarWindowHandle = CreateWindowEx(0, _StatusBarWindowClassName, "", 0, 0, 0, 1, 1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

        return true;
    }

    bool LoadNotifyIconData(string? icon, string? title)
    {
        _NOTIFYICONDATA = NOTIFYICONDATA.GetDefaultNotifyData(_StatusBarWindowHandle);
        _NOTIFYICONDATA.uCallbackMessage = (uint)_WmStatusBarMouseMessage;
        if (string.IsNullOrWhiteSpace(icon))
        {
            var processPath = Environment.ProcessPath;
            if (!string.IsNullOrWhiteSpace(processPath))
            {
                var index = IntPtr.Zero;
                IntPtr hIcon = RuntimeInterop.ExtractAssociatedIcon(IntPtr.Zero, processPath, ref index);
                _NOTIFYICONDATA.hIcon = hIcon;
                _NOTIFYICONDATA.hBalloonIcon = hIcon;

                _hICon = hIcon;
            }
        }
        else
        {
            IntPtr hIcon = User32.LoadImage(IntPtr.Zero, icon, User32.ImageType.IMAGE_ICON, 32, 32, User32.LoadImageFlags.LR_LOADFROMFILE);
            _NOTIFYICONDATA.hIcon = hIcon;
            _NOTIFYICONDATA.hBalloonIcon = hIcon;

            _hICon = hIcon;
        }

        if (!string.IsNullOrWhiteSpace(title))
            _NOTIFYICONDATA.szTip = title;

        return true;
    }

    bool LoadPopupMenu()
    {
        m_hMenu = RuntimeInterop.CreatePopupMenu();
        if (m_hMenu == IntPtr.Zero)
            return false;
         
        AppendMenu(m_hMenu, MenuItemFlags.MF_STRING, new IntPtr(1), "123");
        AppendMenu(m_hMenu, MenuItemFlags.MF_SEPARATOR, new IntPtr(0), null);
        AppendMenu(m_hMenu, MenuItemFlags.MF_STRING, new IntPtr(2), "234");
        AppendMenu(m_hMenu, MenuItemFlags.MF_SEPARATOR, new IntPtr(0), null);
        AppendMenu(m_hMenu, MenuItemFlags.MF_STRING, new IntPtr(3), "345");
        AppendMenu(m_hMenu, MenuItemFlags.MF_SEPARATOR, new IntPtr(0), null);
        AppendMenu(m_hMenu, MenuItemFlags.MF_STRING, new IntPtr(4), "456");
        
        return true;
    }

    bool Show()
    {
        lock (this)
        {
            if (Volatile.Read(ref _IsShowIn))
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Modify, ref _NOTIFYICONDATA);
            else
            {
                Volatile.Write(ref _IsShowIn, true);
                return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Add, ref _NOTIFYICONDATA);
            }
        }
    }

    bool Hide()
    {
        lock (this)
        {
            Volatile.Write(ref _IsShowIn, false);
            _Disposable?.Dispose();
            return RuntimeInterop.Shell_NotifyIcon(NotifyCommand.NIM_Delete, ref _NOTIFYICONDATA);
        }
    }

    unsafe IntPtr WndProc_Callback(IntPtr hWnd, WindowMessage msg, void* wparam, void* lparam)
    {
        if ((int)msg == _WmStatusBarCreated)
        {
            //UpdateIcon(true);
        }
        else if((int)msg == _WmStatusBarMouseMessage)
        {
            switch ((WindowMessage)((long)lparam))
            {
                case WindowMessage.WM_LBUTTONDOWN:
                    break;
                case WindowMessage.WM_LBUTTONDBLCLK:
                    break;
                case WindowMessage.WM_RBUTTONDOWN:
                    {
                        if (m_hMenu == IntPtr.Zero)
                            break;

                        var vPoint = GetCursorPos();
                        RuntimeInterop.TrackPopupMenuEx(m_hMenu, TackMenuFlag.TPM_RIGHTBUTTON, vPoint.x, vPoint.y, _StatusBarWindowHandle, IntPtr.Zero);
                    }
                    break;
                default:
                    break;
            }
        }
        else if (msg == WindowMessage.WM_COMMAND)
        {
            switch ((int)wparam)
            {
                case 1:
                    break;

                default:
                    break;
            }
        }
        else
        {
          
        }

        return DefWindowProc(hWnd, msg, (IntPtr)wparam, (IntPtr)lparam);
    }
}

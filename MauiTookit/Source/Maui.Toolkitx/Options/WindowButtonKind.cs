namespace Maui.Toolkitx.Options;

[Flags]
public enum WindowButtonKind : byte
{
    Hide = 0x00,
    Show = 0x0F,

    EnableMaximizable = 0x01,
    EnableMinizable = 0x02,
    EnableResizable = 0x04,
    EnableClosable = 0x08,
}

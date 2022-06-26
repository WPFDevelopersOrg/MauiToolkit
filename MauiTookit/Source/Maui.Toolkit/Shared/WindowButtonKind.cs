namespace Maui.Toolkit.Shared;

[Flags]
public enum WindowConfigurationKind
{
    HideAllButton = 0x00,
    ShowAllButton = 0x01,
    DisableMaximizable = 0x02,
    DisableMinizable = 0x04,
    DisableResizable = 0x08,
    DisableClosable = 0x10,
}

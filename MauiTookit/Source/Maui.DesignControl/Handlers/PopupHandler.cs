using Maui.DesignControl.Shared;

namespace Maui.DesignControl.Handlers;
public partial class PopupHandler
{
    public static IPropertyMapper<IPopup, PopupHandler> Mapper = new PropertyMapper<IPopup, PopupHandler>(ViewMapper)
    {
        //[nameof(IPopup.MediaPlayer)] = MapMediaPlayer,
    };

    public PopupHandler() : base(Mapper)
    {

    }

    public PopupHandler(IPropertyMapper mapper) : base(mapper ?? Mapper)
    {

    }
    public PopupHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

}

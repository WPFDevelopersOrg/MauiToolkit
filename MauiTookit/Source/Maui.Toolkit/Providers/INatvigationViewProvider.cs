using Maui.Toolkit.Core;

namespace Maui.Toolkit.Providers;
public interface INatvigationViewProvider
{
    INavigationViewBuilder? GetRootShellViewBuilder();

    INavigationViewBuilder? CreateShellViewBuilder(in Window window);

    INavigationViewBuilder? CreateNavigationViewBuilder(in VisualElement visualElement);

}

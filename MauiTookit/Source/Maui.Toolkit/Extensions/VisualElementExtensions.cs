namespace Maui.Toolkit.Extensions;

public static class VisualElementExtensions
{
    public static VisualElement? GetParent(this VisualElement visualElement)
    {
        if (visualElement is null)
            return default;

        return visualElement.GetParent();
    }

    public static VisualElement? GetFirstParentWithAlignmentNotCenter(this VisualElement visualElement)
    {
        if (visualElement is null)
            return default;

        VisualElement? parentElement;
        for (; ; )
        {
            parentElement = GetParent(visualElement);
            if (parentElement is null)
                break;

            if (parentElement is not View viewElement)
                break;

            var horizontalOptions = viewElement.HorizontalOptions;
            if (horizontalOptions.Alignment is LayoutAlignment.Center)
                continue;

            break;
        }

        return parentElement;
    }


}

namespace Maui.Toolkitx.Config;

public class SearchBarConfigurations : BindableObject
{
    public static readonly BindableProperty TextProperty =
                           BindableProperty.Create(propertyName: nameof(Text),
                                                   returnType: typeof(string),
                                                   declaringType: typeof(SearchBarConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty PlaceholderTextProperty =
                           BindableProperty.Create(propertyName: nameof(PlaceholderText),
                                                   returnType: typeof(string),
                                                   declaringType: typeof(SearchBarConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty CommandProperty =
                           BindableProperty.Create(propertyName: nameof(Command),
                                                   returnType: typeof(ICommand),
                                                   declaringType: typeof(SearchBarConfigurations),
                                                   defaultValue: default);

    public static readonly BindableProperty CommandParameterProperty =
                           BindableProperty.Create(propertyName: nameof(CommandParameter),
                                                   returnType: typeof(object),
                                                   declaringType: typeof(SearchBarConfigurations),
                                                   defaultValue: default);

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty,value);
    }

    public string? PlaceholderText
    {
        get => (string?)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty,value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandParameterProperty,value);       
    }

    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

}

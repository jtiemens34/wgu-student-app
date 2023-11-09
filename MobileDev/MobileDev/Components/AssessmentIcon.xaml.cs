using System;
using System.Globalization;

namespace MobileDev.Components;

public partial class AssessmentIcon : ContentView
{
	public AssessmentIcon()
	{
		InitializeComponent();
	}
}
public class BorderColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromRgba("#558F45") : Color.FromRgba("#808080");
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
public class BorderBackgroundColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromRgba("#558F45") : Color.FromRgba("#FFFFFF");
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
public class LabelTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (AssessmentType)value == AssessmentType.Performance ? "P" : "O";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
public class LabelColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Color.FromRgba("#FFFFFF") : Color.FromRgba("#000000");
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
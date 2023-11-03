using System.Globalization;

namespace MobileDev.Components;

public partial class CourseView : ContentView
{
	public CourseView()
	{
		InitializeComponent();
	}
}
public class CourseStatusIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            CourseStatus.Completed => "completedcourseicon.png",
            _ => "uncompletedcourseicon.png",
        };
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
public class CourseStatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (CourseStatus)value == CourseStatus.Completed ? "#558F45" : "#808080";
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
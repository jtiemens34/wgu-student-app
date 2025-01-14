using System.Globalization;

namespace MobileDev.Components;

public partial class CourseView : ContentView
{
	public CourseView()
	{
		InitializeComponent();
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        await LoadAssessments();
    }
    public async void OnCourseClick(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditCourseModal(int.Parse(id.Text)));
        await LoadAssessments();
    }
    private async Task LoadAssessments()
    {
        int courseId = int.Parse(id.Text);
        BindableLayout.SetItemsSource(assessments, await App.DbHandler.GetAllAssessmentsFromCourseAsync(courseId));
    }
}
public class CourseStatusIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            CourseStatus.Completed => "completedcourseicon.png",
            CourseStatus.Dropped => "failedcourseicon.png",
            _ => "uncompletedcourseicon.png",
        };
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
public class CourseStatusRibbonConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            CourseStatus.Completed => "completedribbonicon.png",
            CourseStatus.Dropped => "failedribbonicon.png",
            _ => "uncompletedribbonicon.png",
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
        return value switch
        {
            CourseStatus.Completed => Color.FromRgba("#558F45"),
            CourseStatus.Dropped => Color.FromRgba("#FF0000"),
            _ => Color.FromRgba("#808080")
        };
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
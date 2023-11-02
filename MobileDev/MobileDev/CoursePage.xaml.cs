using MobileDev.Components;

namespace MobileDev;

public partial class CoursePage : ContentPage
{
	public CoursePage()
	{
		InitializeComponent();
	}

	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
        await App.DbHandler.AddNewCourseAsync("Software QA");
    }
	public async void OnLoad(object sender, EventArgs e)
	{
		List<Course> courses = await App.DbHandler.GetAllCoursesAsync();
		BindableLayout.SetItemsSource(courseList, courses);
	}
}
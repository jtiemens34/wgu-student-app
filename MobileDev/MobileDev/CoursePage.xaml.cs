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
		await Navigation.PushModalAsync(new AddCourseModal());
		await LoadCourses();
    }
	public async void OnLoad(object sender, EventArgs e)
	{
		await LoadCourses();
    }
	private async Task LoadCourses()
	{
        BindableLayout.SetItemsSource(courseList, await App.DbHandler.GetAllCoursesAsync());
    }
}
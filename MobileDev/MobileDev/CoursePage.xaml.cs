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
		await LoadAllCourses();
	}
	public async void OnGetButtonClicked(object sender, EventArgs e)
	{
		await LoadAllCourses();
	}

	private async Task LoadAllCourses()
	{
        List<Course> courses = await App.DbHandler.GetAllCoursesAsync();
        CourseView courseView = new();
        CourseLayout.Add(courseView);
    }
	private async Task LoadAllCourses(Term term)
	{
		List<Course> courses = await App.DbHandler.GetAllCoursesFromTermAsync(term);
		foreach (Course course in courses)
		{
			
		}
	}
}
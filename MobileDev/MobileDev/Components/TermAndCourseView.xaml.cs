namespace MobileDev.Components;

public partial class TermAndCourseView : ContentView
{
    private readonly Term Term;
	public TermAndCourseView(Term term)
	{
		InitializeComponent();
        Term = term;
        this.BindingContext = Term;
	}
    public void OnTermClick(object sender, EventArgs e)
    {
        courseList.IsVisible = !courseList.IsVisible;
        arrowIcon.Source = courseList.IsVisible ? "arrowretracticon.png" : "arrowexpandicon.png";
    }
    public async void OnLoad(object sender, EventArgs e)
    {
        await LoadCourses();
    }
    private async Task LoadCourses()
    {
        BindableLayout.SetItemsSource(courseList, await App.DbHandler.GetAllCoursesFromTermAsync(Term));
    }
}
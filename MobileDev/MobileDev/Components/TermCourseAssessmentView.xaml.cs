namespace MobileDev.Components;

public partial class TermCourseAssessmentView : ContentView
{
    private readonly Term Term;
	public TermCourseAssessmentView(Term term)
	{
		InitializeComponent();
        Term = term;
        this.BindingContext = Term;
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
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
    public async void OnTermClick(object sender, EventArgs e)
    {
        
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
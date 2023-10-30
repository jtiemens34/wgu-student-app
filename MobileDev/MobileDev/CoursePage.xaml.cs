namespace MobileDev;

public partial class CoursePage : ContentPage
{
	public CoursePage()
	{
		InitializeComponent();
		LoadAllTerms();
	}

	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
        await App.DbHandler.AddNewCourseAsync("Software QA");
    }

	public async void OnGetButtonClicked(object sender, EventArgs e)
	{
		await LoadAllTerms();
	}

	private async Task LoadAllTerms()
	{
		termList.ItemsSource = await App.DbHandler.GetAllTermsAsync();
		
	}
}
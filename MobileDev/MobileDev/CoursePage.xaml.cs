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
		await LoadTerms();
    }
	public async void OnLoad(object sender, EventArgs e)
	{
        await LoadTerms();
    }
	private async Task LoadTerms()
	{
		termList.Children.Clear();
		List<Term> terms = await App.DbHandler.GetAllTermsAsync();
		foreach (Term term in terms) termList.Add(new TermAndCourseView(term));
	}
}
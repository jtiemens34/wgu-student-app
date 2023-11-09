using MobileDev.Components;

namespace MobileDev;

public partial class AssessmentPage : ContentPage
{
	public AssessmentPage()
	{
		InitializeComponent();
	}
	public async void OnLoad(object sender, EventArgs e)
	{
		await LoadTerms();
	}
    private async Task LoadTerms()
    {
        termList.Children.Clear();
        List<Term> terms = await App.DbHandler.GetAllTermsAsync();
        foreach (Term term in terms) termList.Add(new TermCourseAssessmentView(term));
    }
}
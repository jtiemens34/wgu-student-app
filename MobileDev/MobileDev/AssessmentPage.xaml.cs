namespace MobileDev;

public partial class AssessmentPage : ContentPage
{
	public AssessmentPage()
	{
		InitializeComponent();
	}
	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new AddAssessmentModal());
	}
}
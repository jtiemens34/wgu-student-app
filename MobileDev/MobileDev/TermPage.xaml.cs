namespace MobileDev;

public partial class TermPage : ContentPage
{
	public TermPage()
	{
		InitializeComponent();
		LoadAllTerms();
    }
	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
		await App.DbHandler.AddNewTermAsync("Term 1");
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


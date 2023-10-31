using MobileDev.Components;

namespace MobileDev;

public partial class TermPage : ContentPage
{
	public TermPage()
	{
		InitializeComponent();
    }
	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
		await App.DbHandler.AddNewTermAsync("Term 1");
	}
	public async void OnLoad(object sender, EventArgs e)
	{
		await LoadAllTerms();
	}
	public async void OnGetButtonClicked(object sender, EventArgs e)
	{
		await LoadAllTerms();
	}
	private async Task LoadAllTerms()
	{
		List<Term> terms = await App.DbHandler.GetAllTermsAsync();
		TermView termView = new(terms, false);
		TermLayout.Add(termView);
	}
}


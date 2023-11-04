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
		await Navigation.PushModalAsync(new AddTermModal());
		await LoadTerms();
	}
	public async void OnLoad(object sender, EventArgs e)
	{
		await LoadTerms();
	}
	private async Task LoadTerms()
	{
		BindableLayout.SetItemsSource(termList, await App.DbHandler.GetAllTermsAsync());
	}
}


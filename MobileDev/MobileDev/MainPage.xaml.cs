namespace MobileDev;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }
	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
		await App.DbHandler.AddNewTerm("Term 1");
	}
	public async void OnGetButtonClicked(object sender, EventArgs e)
	{
		List<Term> terms = await App.DbHandler.GetAllTerms();
		termList.ItemsSource = terms;
	}
}


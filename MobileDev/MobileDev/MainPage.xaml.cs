using MobileDev.Components;

namespace MobileDev;

public partial class MainPage : ContentPage
{
	DatabaseHandler database;
	public MainPage()
	{
		InitializeComponent();
		//database = db;
    }
	private void AddComponent()
	{
		Term term = new();
		TermComponent component = new(term);
		TermLayout.Add(component);
	}
}


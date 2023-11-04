namespace MobileDev.Components;

public partial class TermView : ContentView
{
	public TermView()
	{
		InitializeComponent();
	}
    public async void OnTermClick(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditTermModal(int.Parse(id.Text)));
    }
}
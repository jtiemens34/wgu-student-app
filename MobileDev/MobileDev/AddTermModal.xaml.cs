namespace MobileDev;

public partial class AddTermModal : ContentPage
{
	public AddTermModal()
	{
		InitializeComponent();
	}
	public async void OnCancelClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure you want to leave?", "Your changes will not be saved!", "Yes", "No");
        if (acceptDialog) await Navigation.PopModalAsync();
	}
	public async void OnSaveClicked(object sender, EventArgs e)
	{
        if (termName.Text == null)
        {
            await this.DisplayAlert("Error", "Term Name can not be empty!", "OK");
            return;
        }
        Term addTerm= new()
        {
            Name = termName.Text,
            StartDate = startDate.Date,
            EndDate = endDate.Date
        };
        await App.DbHandler.SaveTermAsync(addTerm);
        await Navigation.PopModalAsync();
	}
}
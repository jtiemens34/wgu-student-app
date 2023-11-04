namespace MobileDev;

public partial class EditTermModal : ContentPage
{
    private int TermId;
    private Term Term;
	public EditTermModal(int termId)
	{
		InitializeComponent();
        TermId = termId;
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        Term = await App.DbHandler.GetTermAsync(TermId);
        termName.Text = Term.Name;
        startDate.Date = Term.StartDate;
        endDate.Date = Term.EndDate;
    }
    public async void OnCancelClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure you want to leave?", "Your changes will not be saved!", "Yes", "No");
        if (acceptDialog) await Navigation.PopModalAsync();
	}
	public async void OnSaveClicked(object sender, EventArgs e)
	{
        // TODO: Fix validation not working on Edit Term Modal
        if (termName.Text == null)
        {
            await this.DisplayAlert("Error", "Term Name can not be empty!", "OK");
            return;
        }
        Term.Name = termName.Text;
        Term.StartDate = startDate.Date;
        Term.EndDate = endDate.Date;
        await App.DbHandler.SaveTermAsync(Term);
        await Navigation.PopModalAsync();
	}
    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool acceptDialog = await this.DisplayAlert("Are you sure?", "This term will be permanently lost!", "Yes", "No");
        if (!acceptDialog) return;
        await App.DbHandler.DeleteTermAsync(Term);
        await Navigation.PopModalAsync();
    }
}
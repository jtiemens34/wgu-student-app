namespace MobileDev;

public partial class AddAssessmentModal : ContentPage
{
	public AddAssessmentModal()
	{
		InitializeComponent();
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        Binding binding = new()
        {
            Source = await App.DbHandler.GetAllCoursesAsync()
        };
        course.SetBinding(Picker.ItemsSourceProperty, binding);
        course.ItemDisplayBinding = new Binding("Name");
    }
    public async void OnCancelClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure you want to leave?", "Your changes will not be saved!", "Yes", "No");
        if (acceptDialog) await Navigation.PopModalAsync();
	}
	public async void OnSaveClicked(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(assessmentName.Text))
        {
            await this.DisplayAlert("Error", "Assessment Name can not be empty!", "OK");
            return;
        }
        Assessment addAssessment = new()
        {
            Name = assessmentName.Text,
            CourseId = course.SelectedIndex,
            Type = (AssessmentType)assessmentType.SelectedIndex,
            Date = scheduledDate.Date
        };
        await App.DbHandler.SaveAssessmentAsync(addAssessment);
        await Navigation.PopModalAsync();
	}
}
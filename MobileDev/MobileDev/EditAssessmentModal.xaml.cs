namespace MobileDev;

public partial class EditAssessmentModal : ContentPage
{
    private readonly int AssessmentId;
    private Assessment Assessment;
	public EditAssessmentModal(int assessmentId)
	{
		InitializeComponent();
        AssessmentId = assessmentId;
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        // Populate default values
        Assessment = await App.DbHandler.GetAssessmentAsync(AssessmentId);
        assessmentName.Text = Assessment.Name;
        assessmentType.SelectedIndex = (int)Assessment.Type;
        scheduledDate.Date = Assessment.Date;

        // Populate Course picker
        Binding binding = new()
        {
            Source = await App.DbHandler.GetAllCoursesAsync()
        };
        course.SetBinding(Picker.ItemsSourceProperty, binding);
        course.ItemDisplayBinding = new Binding("Name");

        course.SelectedIndex = Assessment.CourseId;
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
            await this.DisplayAlert("Error", "Course Name can not be empty!", "OK");
            return;
        }
        Assessment.Name = assessmentName.Text;
        Assessment.CourseId = course.SelectedIndex;
        Assessment.Date = scheduledDate.Date;
        Assessment.Type = (AssessmentType)assessmentType.SelectedIndex;
        await App.DbHandler.SaveAssessmentAsync(Assessment);
        await Navigation.PopModalAsync();
	}
    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool acceptDialog = await this.DisplayAlert("Are you sure?", "This assessment will be permanently lost!", "Yes", "No");
        if (!acceptDialog) return;
        await App.DbHandler.DeleteAssessmentAsync(Assessment);
        await Navigation.PopModalAsync();
    }
}
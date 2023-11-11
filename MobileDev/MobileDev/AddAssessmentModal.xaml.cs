namespace MobileDev;

public partial class AddAssessmentModal : ContentPage
{
    private readonly AssessmentType AssessmentType;
    private readonly int CourseId;
	public AddAssessmentModal(AssessmentType assessmentType, int courseId)
	{
		InitializeComponent();
        AssessmentType = assessmentType;
        CourseId = courseId;
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        Course course = await App.DbHandler.GetCourseAsync(CourseId);
        string courseName = course.Name;
        string assessmentType = this.AssessmentType == AssessmentType.Objective ? "n Objective" : " Performance";
        modalTitle.Text = "Adding a" + assessmentType + " assessment to " + courseName;
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
            CourseId = CourseId,
            Completed = completed.IsChecked,
            Type = AssessmentType,
            Date = scheduledDate.Date
        };
        await App.DbHandler.SaveAssessmentAsync(addAssessment);
        await Navigation.PopModalAsync();
	}
}
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
        scheduledDate.Date = Assessment.Date;
        completed.IsChecked = Assessment.Completed;
        notify.IsChecked = Assessment.NotificationEnabled;
        if (Assessment.NotificationEnabled)
        {
            timeDisplay.IsVisible = true;
            time.Time = Assessment.Date.TimeOfDay;
        }
        // Populate 
        Course course = await App.DbHandler.GetCourseAsync(Assessment.CourseId);
        string courseName = course.Name;
        string assessmentType = Assessment.Type == AssessmentType.Objective ? "Objective" : "Performance";
        modalTitle.Text = "Editing " + courseName + "'s " + assessmentType + " Assessment";
    }
    public async void OnCancelClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure you want to leave?", "Your changes will not be saved!", "Yes", "No");
        if (acceptDialog) await Navigation.PopModalAsync();
	}
    public async void OnNotifyChecked(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI && notify.IsChecked)
        {
            await this.DisplayAlert("Unsupported Platform", "Notifications not supported on Windows!", "OK");
            notify.IsChecked = false;
            return;
        }
        timeDisplay.IsVisible = notify.IsChecked;
    }
	public async void OnSaveClicked(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(assessmentName.Text))
        {
            await this.DisplayAlert("Error", "Course Name can not be empty!", "OK");
            return;
        }
        Assessment.Name = assessmentName.Text;

        DateTime scheduledTime = scheduledDate.Date;
        if (notify.IsChecked) scheduledTime.Add(time.Time);
        Assessment.Date = scheduledTime;

        Assessment.Completed = completed.IsChecked;
        Assessment.NotificationEnabled = notify.IsChecked;
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
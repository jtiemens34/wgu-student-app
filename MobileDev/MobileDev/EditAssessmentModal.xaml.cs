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
        startDate.Date = Assessment.StartDate;
        completed.IsChecked = Assessment.Completed;
        startNotify.IsChecked = Assessment.StartNotificationEnabled;
        endNotify.IsChecked = Assessment.EndNotificationEnabled;
        if (Assessment.StartNotificationEnabled)
        {
            startTimeDisplay.IsVisible = true;
            startTime.Time = Assessment.StartDate.TimeOfDay;
        }
        if (Assessment.EndNotificationEnabled)
        {
            endTimeDisplay.IsVisible = true;
            endTime.Time = Assessment.EndDate.TimeOfDay;
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
    public async void OnStartNotifyChecked(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI && startNotify.IsChecked)
        {
            await this.DisplayAlert("Unsupported Platform", "Notifications not supported on Windows!", "OK");
            startNotify.IsChecked = false;
            return;
        }
        startTimeDisplay.IsVisible = startNotify.IsChecked;
    }
    public async void OnEndNotifyChecked(object sender, EventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI && endNotify.IsChecked)
        {
            await this.DisplayAlert("Unsupported Platform", "Notifications not supported on Windows!", "OK");
            endNotify.IsChecked = false;
            return;
        }
        endTimeDisplay.IsVisible = endNotify.IsChecked;
    }
    public async void OnSaveClicked(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(assessmentName.Text))
        {
            await this.DisplayAlert("Error", "Course Name can not be empty!", "OK");
            return;
        }
        Assessment.Name = assessmentName.Text;

        DateTime startDateTime = startDate.Date;
        if (startNotify.IsChecked) startDateTime = startDateTime.Add(startTime.Time);
        Assessment.StartDate = startDateTime;

        DateTime endDateTime = endDate.Date;
        if (endNotify.IsChecked) endDateTime = endDateTime.Add(endTime.Time);
        Assessment.EndDate = endDateTime;

        Assessment.Completed = completed.IsChecked;
        Assessment.StartNotificationEnabled = startNotify.IsChecked;
        Assessment.EndNotificationEnabled = endNotify.IsChecked;
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
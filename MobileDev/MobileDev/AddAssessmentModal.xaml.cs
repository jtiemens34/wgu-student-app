using Plugin.LocalNotification;

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
            await this.DisplayAlert("Error", "Assessment Name can not be empty!", "OK");
            return;
        }
        DateTime date = scheduledDate.Date;
        if (notify.IsChecked) date = date.Add(time.Time);
        Assessment addAssessment = new()
        {
            Name = assessmentName.Text,
            CourseId = CourseId,
            Completed = completed.IsChecked,
            Type = AssessmentType,
            Date = date,
            NotificationEnabled = notify.IsChecked
        };
        await App.DbHandler.SaveAssessmentAsync(addAssessment);

        if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
            await addAssessment.AddNotification();

        await Navigation.PopModalAsync();
	}
}
namespace MobileDev;

public partial class AddCourseModal : ContentPage
{
	public AddCourseModal()
	{
		InitializeComponent();
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        Binding binding = new()
        {
            Source = await App.DbHandler.GetAllTermsAsync()
        };
        term.SetBinding(Picker.ItemsSourceProperty, binding);
        term.ItemDisplayBinding = new Binding("Name");
    }
    public async void OnShareClicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(notes.Text))
        {
            await this.DisplayAlert("Error", "Notes must not be empty!", "OK");
            return;
        }
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = notes.Text,
            Title = "Share Notes"
        });
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
    public async void OnCancelClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure you want to leave?", "Your changes will not be saved!", "Yes", "No");
        if (acceptDialog) await Navigation.PopModalAsync();
	}
	public async void OnSaveClicked(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(courseName.Text)) await this.DisplayAlert("Error", "Course Name can not be empty!", "OK");
        if (String.IsNullOrEmpty(instructorName.Text)) await this.DisplayAlert("Error", "Instructor Name can not be empty!", "OK");
        if (String.IsNullOrEmpty(instructorPhone.Text)) await this.DisplayAlert("Error", "Instructor Phone can not be empty!", "OK");
        if (String.IsNullOrEmpty(instructorEmail.Text)) await this.DisplayAlert("Error", "Instructor Email can not be empty!", "OK");
        if (courseName.Text == null || instructorName.Text == null || instructorPhone.Text == null || instructorEmail.Text == null) return;
        Term selectedTerm = (Term)term.SelectedItem;
        DateTime startDateTime = startDate.Date;
        if (startNotify.IsChecked) startDateTime = startDateTime.Add(startTime.Time);
        DateTime endDateTime = endDate.Date;
        if (endNotify.IsChecked) endDateTime = endDateTime.Add(endTime.Time);
        Course addCourse = new()
        {
            CourseStatus = (CourseStatus)courseStatus.SelectedIndex,
            Name = courseName.Text,
            TermId = selectedTerm.Id,
            Credits = (int)credits.Value,
            StartDate = startDateTime.Date,
            EndDate = endDateTime.Date,
            StartNotificationEnabled = startNotify.IsChecked,
            EndNotificationEnabled = endNotify.IsChecked,
            InstructorName = instructorName.Text,
            InstructorPhone = instructorPhone.Text,
            InstructorEmail = instructorEmail.Text,
            Notes = notes.Text
        };
        await App.DbHandler.SaveCourseAsync(addCourse);
        await Navigation.PopModalAsync();
	}
}
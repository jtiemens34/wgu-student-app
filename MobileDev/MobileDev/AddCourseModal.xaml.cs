namespace MobileDev;

public partial class AddCourseModal : ContentPage
{
	public AddCourseModal()
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
        if (courseName.Text == null) await this.DisplayAlert("Error", "Course Name can not be empty!", "OK");
        if (instructorName.Text == null) await this.DisplayAlert("Error", "Instructor Name can not be empty!", "OK");
        if (instructorPhone.Text == null) await this.DisplayAlert("Error", "Instructor Phone can not be empty!", "OK");
        if (instructorEmail.Text == null) await this.DisplayAlert("Error", "Instructor Email can not be empty!", "OK");
        if (courseName.Text == null || instructorName.Text == null || instructorPhone.Text == null || instructorEmail.Text == null) return;
        Course addCourse = new()
        {
            CourseStatus = (CourseStatus)courseStatus.SelectedIndex,
            Name = courseName.Text,
            Credits = (int)credits.Value,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            InstructorName = instructorName.Text,
            InstructorPhone = instructorPhone.Text,
            InstructorEmail = instructorEmail.Text
        };
        await App.DbHandler.SaveCourseAsync(addCourse);
        await Navigation.PopModalAsync();
	}
}
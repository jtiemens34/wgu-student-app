namespace MobileDev;

public partial class EditCourseModal : ContentPage
{
    private int CourseId;
    private Course Course;
	public EditCourseModal(int courseId)
	{
		InitializeComponent();
        CourseId = courseId;
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        Course = await App.DbHandler.GetCourseAsync(CourseId);
        courseName.Text = Course.Name;
        credits.Value = Course.Credits;
        courseStatus.SelectedIndex = (int)Course.CourseStatus;
        startDate.Date = Course.StartDate;
        endDate.Date = Course.EndDate;
        instructorName.Text = Course.InstructorName;
        instructorPhone.Text = Course.InstructorPhone;
        instructorEmail.Text = Course.InstructorEmail;
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
        Course.Name = courseName.Text;
        Course.Credits = (int)credits.Value;
        Course.CourseStatus = (CourseStatus)courseStatus.SelectedIndex;
        Course.StartDate = startDate.Date;
        Course.EndDate = endDate.Date;
        Course.InstructorName = instructorName.Text;
        Course.InstructorPhone = instructorPhone.Text;
        Course.InstructorEmail = instructorEmail.Text;
        await App.DbHandler.SaveCourseAsync(Course);
        await Navigation.PopModalAsync();
	}
    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool acceptDialog = await this.DisplayAlert("Are you sure?", "This course will be permanently lost!", "Yes", "No");
        if (!acceptDialog) return;
        await App.DbHandler.DeleteCourseAsync(Course);
        await Navigation.PopModalAsync();
    }
}
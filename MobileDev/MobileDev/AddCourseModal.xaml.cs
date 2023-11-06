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
        Course addCourse = new()
        {
            CourseStatus = (CourseStatus)courseStatus.SelectedIndex,
            Name = courseName.Text,
            TermId = term.SelectedIndex,
            Credits = (int)credits.Value,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            InstructorName = instructorName.Text,
            InstructorPhone = instructorPhone.Text,
            InstructorEmail = instructorEmail.Text,
            Notes = notes.Text
        };
        await App.DbHandler.SaveCourseAsync(addCourse);
        await Navigation.PopModalAsync();
	}
}
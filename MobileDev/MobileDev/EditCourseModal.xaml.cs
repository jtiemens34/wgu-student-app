namespace MobileDev;

public partial class EditCourseModal : ContentPage
{
    private readonly int CourseId;
    private Course Course;
	public EditCourseModal(int courseId)
	{
		InitializeComponent();
        CourseId = courseId;
	}
    public async void OnLoad(object sender, EventArgs e)
    {
        // Populate default values
        Course = await App.DbHandler.GetCourseAsync(CourseId);
        courseName.Text = Course.Name;
        credits.Value = Course.Credits;
        courseStatus.SelectedIndex = (int)Course.CourseStatus;
        startDate.Date = Course.StartDate;
        endDate.Date = Course.EndDate;
        instructorName.Text = Course.InstructorName;
        instructorPhone.Text = Course.InstructorPhone;
        instructorEmail.Text = Course.InstructorEmail;
        notes.Text = Course.Notes;

        // Populate Term picker
        List<Term> terms = await App.DbHandler.GetAllTermsAsync();
        Binding binding = new()
        {
            Source = terms
        };
        term.SetBinding(Picker.ItemsSourceProperty, binding);
        term.ItemDisplayBinding = new Binding("Name");

        term.SelectedIndex = terms.FindIndex(o => o.Id ==  Course.TermId);
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
        if (String.IsNullOrEmpty(courseName.Text ) || String.IsNullOrEmpty(instructorName.Text) 
            || String.IsNullOrEmpty(instructorPhone.Text) || String.IsNullOrEmpty(instructorEmail.Text)) return;
        Term selectedTerm = (Term)term.SelectedItem;
        Course.Name = courseName.Text;
        Course.TermId = selectedTerm.Id;
        Course.Credits = (int)credits.Value;
        Course.CourseStatus = (CourseStatus)courseStatus.SelectedIndex;
        Course.StartDate = startDate.Date;
        Course.EndDate = endDate.Date;
        Course.InstructorName = instructorName.Text;
        Course.InstructorPhone = instructorPhone.Text;
        Course.InstructorEmail = instructorEmail.Text;
        Course.Notes = notes.Text;
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
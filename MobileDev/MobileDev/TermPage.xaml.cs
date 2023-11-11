using MobileDev.Components;

namespace MobileDev;

public partial class TermPage : ContentPage
{
	public TermPage()
	{
		InitializeComponent();
    }
	public async void OnNewButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new AddTermModal());
		await LoadTerms();
	}
	public async void OnLoad(object sender, EventArgs e)
	{
		await LoadTerms();
	}
	private async Task LoadTerms()
	{
		BindableLayout.SetItemsSource(termList, await App.DbHandler.GetAllTermsAsync());
	}
	public async void OnEvaluationClicked(object sender, EventArgs e)
	{
        bool acceptDialog = await this.DisplayAlert("Are you sure?", "Creating evaluation data will clear all other data!", "Yes", "No");
		if (!acceptDialog) return;

		await App.DbHandler.DeleteAllAssessmentsAsync();
		await App.DbHandler.DeleteAllTermsAsync();
		await App.DbHandler.DeleteAllCoursesAsync();

		Term term = new()
		{
			Name = "Term 1",
			StartDate = DateTime.Now,
			EndDate = DateTime.Now.AddMonths(1)
		};
		await App.DbHandler.SaveTermAsync(term);

		Course course = new()
		{
			Name = "Mobile App Development",
			TermId = term.Id,
			StartDate = DateTime.Now,
			EndDate = DateTime.Now.AddMonths(1),
			Credits = 3,
			CourseStatus = CourseStatus.InProgress,
			InstructorName = "Anika Patel",
			InstructorPhone = "555-123-4567",
			InstructorEmail = "anika.patel@strimeuniversity.edu"
		};
		await App.DbHandler.SaveCourseAsync(course);

		Assessment performanceAssessment = new()
		{
			CourseId = course.Id,
			Name = "Mobile App Dev PA",
			Date = DateTime.Now.AddMonths(1),
			Type = AssessmentType.Performance,
			Completed = false,
			NotificationEnabled = false
		};
        await App.DbHandler.SaveAssessmentAsync(performanceAssessment);
        Assessment objectiveAssessment = new()
        {
            CourseId = course.Id,
            Name = "Mobile App Dev OA",
            Date = DateTime.Now.AddMonths(1),
            Type = AssessmentType.Objective,
            Completed = false,
            NotificationEnabled = false
        };
		await App.DbHandler.SaveAssessmentAsync(objectiveAssessment);

		await LoadTerms();
    }
}


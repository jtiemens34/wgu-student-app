using System.Globalization;

namespace MobileDev.Components;

public partial class CourseAndAssessmentView : ContentView
{
    public CourseAndAssessmentView()
    {
        InitializeComponent();
    }
    public async void OnLoad(object sender, EventArgs e)
    {
        await LoadAssessments();
    }
    private async Task LoadAssessments()
    {
        int courseIdParsed = int.Parse(courseId.Text);
        assessments.Children.Clear();
        Assessment performanceAssessment = await App.DbHandler.GetPerformanceAssessmentAsync(courseIdParsed),
                   objectiveAssessment = await App.DbHandler.GetObjectiveAssessmentAsync(courseIdParsed);
        performanceAssessment ??= new()
            {
                Name = "No Performance Assessment",
                Date = DateTime.Now,
                Type = AssessmentType.Performance,
                Completed = false
            };
        objectiveAssessment ??= new()
            {
                Name = "No Objective Assessment",
                Date = DateTime.Now,
                Type = AssessmentType.Objective,
                Completed = false
            };
        assessments.Children.Add(new AssessmentView(courseIdParsed, performanceAssessment));
        assessments.Children.Add(new AssessmentView(courseIdParsed, objectiveAssessment));
    }
}
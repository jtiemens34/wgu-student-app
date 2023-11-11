namespace MobileDev.Components;

public partial class AssessmentView : ContentView
{
    private readonly int CourseId;
	public AssessmentView(int courseId, Assessment assessment)
	{
		InitializeComponent();
        CourseId = courseId;
        BindingContext = assessment;
	}

    public async void OnAssessmentClicked(object sender, EventArgs e)
    {
        int assessmentIdParsed = int.Parse(assessmentId.Text);
        AssessmentType assessmentTypeParsed = assessmentType.Text == "Performance" ? AssessmentType.Performance : AssessmentType.Objective;
        if (assessmentIdParsed == 0) await Navigation.PushModalAsync(new AddAssessmentModal(assessmentTypeParsed, CourseId));
        else await Navigation.PushModalAsync(new EditAssessmentModal(assessmentIdParsed));
    }
}
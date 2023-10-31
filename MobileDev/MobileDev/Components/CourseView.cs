namespace MobileDev.Components;

public class CourseView : CollectionView
{
	public CourseView(List<Course> courses)
	{
		this.ItemTemplate = new(() =>
		{
			Grid layout = new();

			Image courseProgressIcon = new();
			AbsoluteLayout.SetLayoutBounds(courseProgressIcon, new Rect(10, 10, 30, 30));
			layout.Add(courseProgressIcon);

			Image ribbonIcon = new();
			AbsoluteLayout.SetLayoutBounds(ribbonIcon, new Rect(44, 0, 17, 25));
			Label ribbonLabel = new();
			ribbonLabel.SetBinding(Label.TextProperty, "Credits");
			AbsoluteLayout.SetLayoutBounds(ribbonLabel, new Rect(49, 4, 7, 12));
			layout.Add(ribbonIcon);
			layout.Add(ribbonLabel);

			Label courseNameLabel = new();
			courseNameLabel.SetBinding(Label.TextProperty, "Name");
			AbsoluteLayout.SetLayoutBounds(courseNameLabel, new Rect(69, 9, 234, 26));
			layout.Add(courseNameLabel);

			ImageButton editButton = new() { Source = "editicon.svg" };
			AbsoluteLayout.SetLayoutBounds(editButton, new Rect(69, 31, 15, 15));
			layout.Add(editButton);

            ImageButton deleteButton = new() { Source = "deleteicon.svg" };
            AbsoluteLayout.SetLayoutBounds(deleteButton, new Rect(90, 31, 15, 15));
			layout.Add(deleteButton);

			Label startDateLabel = new();
			startDateLabel.SetBinding(Label.TextProperty, "StartDate");
			AbsoluteLayout.SetLayoutBounds(startDateLabel, new Rect(170, 35, 58, 13));
			layout.Add(startDateLabel);

            Label endDateLabel = new();
            endDateLabel.SetBinding(Label.TextProperty, "EndDate");
            AbsoluteLayout.SetLayoutBounds(endDateLabel, new Rect(228, 35, 58, 13));
			layout.Add(endDateLabel);

			ImageButton datePickButton = new() { Source = "datepickicon.svg" };
			AbsoluteLayout.SetLayoutBounds(datePickButton, new Rect(282, 26, 20, 20));
			layout.Add(datePickButton);

			Image assessmentIcon = new();
			AbsoluteLayout.SetLayoutBounds(assessmentIcon, new Rect(310, 15, 20, 20));
			layout.Add(assessmentIcon);

            return layout;
		});
		this.ItemsSource = courses;
	}
}
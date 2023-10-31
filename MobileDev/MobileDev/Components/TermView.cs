using Microsoft.Maui.Controls.Internals;

namespace MobileDev.Components;

public class TermView : CollectionView
{
	public TermView(List<Term> terms, bool displayCourses)
	{
		this.ItemTemplate = new(() =>
		{
            Grid gridView = new()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(15) },
                    new ColumnDefinition { Width = new GridLength(15) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(30) }
                },
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            Label nameLabel = new() { VerticalOptions = LayoutOptions.Center };
            nameLabel.SetBinding(Label.TextProperty, "Name");
            gridView.Add(nameLabel, 0, 0);
            gridView.SetRowSpan(nameLabel, 2);

            ImageButton editButton = new()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Source = "editicon.svg"
            };
            gridView.Add(editButton, 1, 0);
            gridView.SetRowSpan(editButton, 2);

            ImageButton deleteButton = new()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Source = "deleteicon.svg"
            };
            gridView.Add(deleteButton, 2, 0);
            gridView.SetRowSpan(deleteButton, 2);

            Label startDateLabel = new() { VerticalOptions = LayoutOptions.Start };
            startDateLabel.SetBinding(Label.TextProperty, "StartDate");
            gridView.Add(startDateLabel, 4, 0);


            Label endDateLabel = new() { VerticalOptions = LayoutOptions.Start };
            startDateLabel.SetBinding(Label.TextProperty, "EndDate");
            gridView.Add(endDateLabel, 5, 0);

            ImageButton datePickButton = new()
            {
                VerticalOptions = LayoutOptions.Center,
                Source = "datepickicon.svg"
            };
            gridView.Add(datePickButton, 5, 0);
            gridView.SetRowSpan(datePickButton, 2);

            return gridView;
        });
        this.ItemsSource = terms;
    }
}
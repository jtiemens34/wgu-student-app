namespace MobileDev.Components;

public partial class TermComponent : ContentView
{
	private Term Term;
	public TermComponent(Term term)
	{
		InitializeComponent();
		Term = term;
	}
}
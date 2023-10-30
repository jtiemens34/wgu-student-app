namespace MobileDev;

public partial class App : Application
{
	public static DatabaseHandler DbHandler { get; private set; }
	public App(DatabaseHandler handler)
	{
		InitializeComponent();

		MainPage = new AppShell();

		DbHandler = handler;
	}
}

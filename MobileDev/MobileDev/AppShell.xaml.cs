using Plugin.LocalNotification;

namespace MobileDev;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}
	public async void OnLoad(object sender, EventArgs e)
	{
        if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
        {
            foreach (Assessment assessment in await App.DbHandler.GetAllNotifiedAssessmentsAsync())
                await assessment.AddNotification();
            foreach (Course course in await App.DbHandler.GetAllNotifiedCoursesAsync())
                await course.AddNotification();
        }
    }
}

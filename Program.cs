using Team3_ProjectB;

public class Program
{
    public static void Main()
    {
        NavigationService.Clear(); // Ensure the navigation stack is empty at start
        NavigationService.Navigate(Menu.Start); // Start the app with the main menu
    }
}

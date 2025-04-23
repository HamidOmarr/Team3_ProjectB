
public class ReservationSystem
{
    public static void ProcessPayment()
    {
        Console.WriteLine("Press 1 to pay with Apple Pay");
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("Payment successful!");
        }
        else
        {
            Console.WriteLine("Payment failed. Try again.");
            ProcessPayment();
        }
    }
}
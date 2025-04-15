public class SeatSelection
{
    public static int AmountSeats { get; set; }


    public static int AmountSeatsInput()
    {
        Console.Clear();
      Console.WriteLine("Enter the amount of seats you want to reserve: ");
      string input = Console.ReadLine();
      AmountSeats = Convert.ToInt32(input);
        SeatSelectionMap();

        return AmountSeats;
    }




    public static void SeatSelectionMap()
    {
        int rows = 5;
        int seatsPerRow = 10;
        int aisle = seatsPerRow / 2;

        int selectedRow = 0;
        int selectedSeat = 0;

        var selectedSeats = new HashSet<(int row, int seat)>();

        ConsoleKey key;

        do
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("┌────────────────────────────── SCREEN ──────────────────────────────┐");
            Console.WriteLine("└────────────────────────────────────────────────────────────────────┘\n");
            Console.ResetColor();

            Console.Write("         ");
            for (int seat = 0; seat < seatsPerRow; seat++)
            {
                if (seat == aisle)
                    Console.Write("    ");
                Console.Write($"{seat + 1,5}");
            }
            Console.WriteLine("\n");

            for (int row = 0; row < rows; row++)
            {
                char rowLetter = (char)('A' + row);
                Console.Write($"   Row {rowLetter}   ");

                for (int seat = 0; seat < seatsPerRow; seat++)
                {
                    if (seat == aisle)
                        Console.Write("    "); // gangpad

                    bool isCursor = row == selectedRow && seat == selectedSeat;
                    bool isChosen = selectedSeats.Contains((row, seat));

                    if (isCursor)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;

                        if (isChosen)
                            Console.Write(" [X] ");
                        else
                            Console.Write(" [^] ");

                        Console.ResetColor();
                    }
                    else if (isChosen)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" [X] ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" [ ] ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nGeselecteerd: {selectedSeats.Count}/{AmountSeats}");
            Console.WriteLine("Gebruik ↑ ↓ ← → om te navigeren, [Spatie] om te selecteren, Enter om te bevestigen");

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedRow > 0) selectedRow--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedRow < rows - 1) selectedRow++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selectedSeat > 0) selectedSeat--;
                    break;
                case ConsoleKey.RightArrow:
                    if (selectedSeat < seatsPerRow - 1) selectedSeat++;
                    break;
                case ConsoleKey.Spacebar:
                    var seatPos = (selectedRow, selectedSeat);
                    if (selectedSeats.Contains(seatPos))
                    {
                        selectedSeats.Remove(seatPos);
                    }
                    else if (selectedSeats.Count < AmountSeats)
                    {
                        selectedSeats.Add(seatPos);
                    }
                    break;
            }

        } while (key != ConsoleKey.Enter || selectedSeats.Count != AmountSeats);

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Je hebt de volgende stoelen geselecteerd:\n");

        foreach (var (row, seat) in selectedSeats)
        {
            char rowLetter = (char)('A' + row);
            Console.WriteLine($"Rij {rowLetter}, Stoel {seat + 1}");
        }

        Console.ResetColor();
    }






}
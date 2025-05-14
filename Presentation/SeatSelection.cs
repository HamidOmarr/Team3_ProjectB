public class SeatSelection
{
    public static int AmountSeats { get; set; }

    public static int AmountSeatsInput(int auditoriumId, string movieName, string sessionTime, long reservationId)
    {
        Console.Clear();
        Console.WriteLine("Enter the amount of seats you want to reserve: ");
        string input = Console.ReadLine();
        AmountSeats = Convert.ToInt32(input);
        SeatSelectionMap(auditoriumId, movieName, sessionTime, reservationId);
        return AmountSeats;
    }

    public static void SeatSelectionMap(int auditoriumId, string movieName, string sessionTime, long reservationId)
    {
        SeatsLogic seatsLogic = new SeatsLogic();
        var seats = seatsLogic.GetSeatsByAuditorium(auditoriumId);

        // Create a dictionary for seat lookup
        var seatLookup = new Dictionary<(string, int), SeatsModel>();
        foreach (var seat in seats)
        {
            var seatKey = (seat.RowNumber.Trim().ToUpper(), seat.SeatNumber);
            if (!seatLookup.ContainsKey(seatKey))
            {
                seatLookup.Add(seatKey, seat);
            }
        }

        // Get distinct row list
        var rowList = new List<string>();
        foreach (var seat in seats)
        {
            string row = seat.RowNumber.Trim().ToUpper();
            if (!rowList.Contains(row))
            {
                rowList.Add(row);
            }
        }

        // Sort rows alphabetically
        rowList.Sort();

        // Find the maximum seat number
        int maxSeatNumber = 0;
        foreach (var seat in seats)
        {
            if (seat.SeatNumber > maxSeatNumber)
            {
                maxSeatNumber = seat.SeatNumber;
            }
        }

        int selectedRowIndex = 0;
        int selectedSeatNumber = 1;

        var selectedSeats = new HashSet<(string row, int seat)>();
        int amountSeats = AmountSeats;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  _________              __      _________      .__                 __  .__               
 /   _____/ ____ _____ _/  |_   /   _____/ ____ |  |   ____   _____/  |_|__| ____   ____  
 \_____  \_/ __ \\__  \\   __\  \_____  \_/ __ \|  | _/ __ \_/ ___\   __\  |/  _ \ /    \ 
 /        \  ___/ / __ \|  |    /        \  ___/|  |_\  ___/\  \___|  | |  (  <_> )   |  \
/_______  /\___  >____  /__|   /_______  /\___  >____/\___  >\___  >__| |__|\____/|___|  /
        \/     \/     \/               \/     \/          \/     \/                    
");
            Console.ResetColor();
            Console.Write("\n         "); // Indent to align with "Row A"
            for (int seatNum = 1; seatNum <= maxSeatNumber; seatNum++)
            {
                Console.Write($"{seatNum,5}");
            }
            Console.WriteLine("\n");

            foreach (var row in rowList)
            {
                Console.Write($"   Rij {row}   ");
                for (int seatNum = 1; seatNum <= maxSeatNumber; seatNum++)
                {
                    var pos = (row, seatNum);
                    bool exists = seatLookup.ContainsKey(pos);
                    bool isCursor = row == rowList[selectedRowIndex] && seatNum == selectedSeatNumber;
                    bool isSelected = selectedSeats.Contains(pos);

                    if (exists)
                    {
                        var seat = seatLookup[pos];
                        bool isVip = seat.SeatTypeId == 2;

                        if (isCursor)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(isSelected ? " [X] " : " [^] ");
                            Console.ResetColor();
                        }
                        else if (isSelected)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" [X] ");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (isVip)
                                Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" [ ] ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write("     ");
                    }
                }

                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("┌────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("└───────────────────────────────SCREEN───────────────────────────────┘\n");
            Console.ResetColor();
            var currentRow = rowList[selectedRowIndex];
            var hoveredSeat = seatLookup.ContainsKey((currentRow, selectedSeatNumber)) ? seatLookup[(currentRow, selectedSeatNumber)] : null;
            string priceInfo = hoveredSeat != null ? $"Prijs: {hoveredSeat.Price:F2} Euro" : "Onbekende stoel";
            Console.Write("Legenda: VIP Seat ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ ]  ");
            Console.ResetColor();

            Console.Write("Standard Seat ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[ ]");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine($"\nGeselecteerd: {selectedSeats.Count}/{amountSeats} — {priceInfo}");
            Console.WriteLine("Gebruik ↑ ↓ ← → om te navigeren, [Spatie] om te selecteren, Enter om te bevestigen");

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedRowIndex > 0) selectedRowIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedRowIndex < rowList.Count - 1) selectedRowIndex++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selectedSeatNumber > 1) selectedSeatNumber--;
                    break;
                case ConsoleKey.RightArrow:
                    if (selectedSeatNumber < maxSeatNumber) selectedSeatNumber++;
                    break;
                case ConsoleKey.Spacebar:
                    var pos = (rowList[selectedRowIndex], selectedSeatNumber);
                    if (seatLookup.ContainsKey(pos))
                    {
                        if (selectedSeats.Contains(pos))
                            selectedSeats.Remove(pos);
                        else if (selectedSeats.Count < amountSeats)
                            selectedSeats.Add(pos);
                    }
                    break;
            }

        } while (key != ConsoleKey.Enter || selectedSeats.Count != amountSeats);

        Console.Clear();
        Console.WriteLine("You have selected the following seats:\n");

        foreach (var selectedSeat in selectedSeats)
        {
            var seat = seatLookup[selectedSeat];
            Console.WriteLine($"Row {selectedSeat.row}, Seat {selectedSeat.seat} — {seat.Price:F2} Euro");
        }

        // Call the Foodmenu process
        Foodmenu.StartFoodMenu(reservationId);

        // Call the Checkout process
        Checkout.StartCheckout(movieName, sessionTime, new List<(string, int)>(selectedSeats));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Team3_ProjectB
{
    public class SeatSelection
    {
        public static int AmountSeats { get; set; }

        public static int AmountSeatsInput(int auditoriumId, string movieName, string sessionTime, long reservationId, int sessionId)
        {
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();
            var seatsLogic = new SeatsLogic();
            var availableSeats = seatsLogic.GetAvailableSeats(auditoriumId, sessionId);
            int maxSeats = availableSeats.Count;

            if (maxSeats == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No seats are available for this session.");
                Console.ResetColor();
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey(true);
                NavigationService.GoBack();
                return 0;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Enter the amount of seats you want to reserve (1-{maxSeats}, press Backspace to go back): ");
            Console.ResetColor();

            string input = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (int.TryParse(input, out int amount) && amount > 0 && amount <= maxSeats)
                    {
                        AmountSeats = amount;
                        NavigationService.Navigate(() => SeatSelectionMap(auditoriumId, movieName, sessionTime, reservationId, sessionId));
                        return AmountSeats;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nInvalid input. Please enter a number between 1 and {maxSeats}.");
                        Console.ResetColor();
                        input = "";
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        NavigationService.GoBack();
                        return 0;
                    }
                }
                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
            }
        }

        public static void SeatSelectionMap(int auditoriumId, string movieName, string sessionTime, long reservationId, int sessionId)
        {
            SeatsLogic seatsLogic = new SeatsLogic();
            var unavailableSeatIds = SeatsLogic.GetReservedSeatIds((int)sessionId);

            var seats = seatsLogic.GetSeatsByAuditorium(auditoriumId);

            var seatLookup = new Dictionary<(string, int), SeatsModel>();
            var reservedPositions = new HashSet<(string, int)>();

            foreach (var seat in seats)
            {
                var pos = (seat.RowNumber.Trim().ToUpper(), seat.SeatNumber);
                seatLookup[pos] = seat;
                if (unavailableSeatIds.Contains((int)seat.Id))
                {
                    reservedPositions.Add(pos);
                }
            }

            var rowList = seats.Select(s => s.RowNumber.Trim().ToUpper()).Distinct().OrderBy(r => r).ToList();
            int maxSeatNumber = seats.Max(s => s.SeatNumber);

            int selectedRowIndex = 0;
            int selectedSeatNumber = 1;

            var selectedSeats = new HashSet<(string row, int seat)>();
            int amountSeats = AmountSeats;
            ConsoleKey key;

            bool IsNavigable(string row, int seatNum) =>
                seatLookup.ContainsKey((row, seatNum)) && !reservedPositions.Contains((row, seatNum));

            while (true)
            {
                Console.Clear();
                LoginStatusHelper.ShowLoginStatus();

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
                Console.Write("\n         ");
                for (int seatNum = 1; seatNum <= maxSeatNumber; seatNum++)
                {
                    Console.Write($"{seatNum,5}");
                }
                Console.WriteLine("\n");

                bool compactAuditorium = auditoriumId == 2 || auditoriumId == 3;
                string seatDisplay = compactAuditorium ? "[ ]" : " [ ] ";
                string seatDisplaySelected = compactAuditorium ? "[X]" : " [X] ";
                string seatDisplayCursor = compactAuditorium ? "[^]" : " [^] ";

                foreach (var row in rowList)
                {
                    Console.Write($"   Rij {row}   ");
                    for (int seatNum = 1; seatNum <= maxSeatNumber; seatNum++)
                    {
                        var pos = (row, seatNum);
                        bool exists = seatLookup.ContainsKey(pos);
                        bool isCursor = row == rowList[selectedRowIndex] && seatNum == selectedSeatNumber;
                        bool isSelected = selectedSeats.Contains(pos);
                        bool isUnavailable = reservedPositions.Contains(pos);

                        if (exists)
                        {
                            var seat = seatLookup[pos];
                            bool isVip = seat.SeatTypeId == 2;
                            bool isComfort = seat.SeatTypeId == 3;

                            if (isCursor)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(isSelected ? seatDisplaySelected : seatDisplayCursor);
                                Console.ResetColor();
                            }
                            else if (isUnavailable)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(seatDisplaySelected);
                                Console.ResetColor();
                            }
                            else if (isSelected)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(seatDisplaySelected);
                                Console.ResetColor();
                            }
                            else if (isComfort)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(seatDisplay);
                                Console.ResetColor();
                            }
                            else
                            {
                                if (isVip)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(seatDisplay);
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Write(compactAuditorium ? "   " : "     ");
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

                Console.Write("Comfort Seat ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[ ]  ");
                Console.ResetColor();

                Console.Write("Standard Seat ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[ ]  ");
                Console.ResetColor();

                Console.Write("Taken Seat ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[X]");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"\nGeselecteerd: {selectedSeats.Count}/{amountSeats} — {priceInfo}");
                Console.WriteLine("\n*Selecteer alle stoelen om door te gaan.*");
                Console.WriteLine("\nGebruik ↑ ↓ ← → om te navigeren, [Spatie] om te selecteren, Enter om te bevestigen");
                Console.WriteLine("Press Backspace to go back.");

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        int originalUp = selectedRowIndex;
                        do selectedRowIndex--; while (selectedRowIndex >= 0 && !IsNavigable(rowList[selectedRowIndex], selectedSeatNumber));
                        if (selectedRowIndex < 0) selectedRowIndex = originalUp;
                        break;

                    case ConsoleKey.DownArrow:
                        int originalDown = selectedRowIndex;
                        do selectedRowIndex++; while (selectedRowIndex < rowList.Count && !IsNavigable(rowList[selectedRowIndex], selectedSeatNumber));
                        if (selectedRowIndex >= rowList.Count) selectedRowIndex = originalDown;
                        break;

                    case ConsoleKey.LeftArrow:
                        int originalLeft = selectedSeatNumber;
                        do selectedSeatNumber--; while (selectedSeatNumber > 0 && !IsNavigable(rowList[selectedRowIndex], selectedSeatNumber));
                        if (selectedSeatNumber <= 0) selectedSeatNumber = originalLeft;
                        break;

                    case ConsoleKey.RightArrow:
                        int originalRight = selectedSeatNumber;
                        do selectedSeatNumber++; while (selectedSeatNumber <= maxSeatNumber && !IsNavigable(rowList[selectedRowIndex], selectedSeatNumber));
                        if (selectedSeatNumber > maxSeatNumber) selectedSeatNumber = originalRight;
                        break;

                    case ConsoleKey.Spacebar:
                        var pos = (rowList[selectedRowIndex], selectedSeatNumber);
                        if (seatLookup.ContainsKey(pos) && !reservedPositions.Contains(pos))
                        {
                            if (selectedSeats.Contains(pos))
                                selectedSeats.Remove(pos);
                            else if (selectedSeats.Count < amountSeats)
                                selectedSeats.Add(pos);
                        }
                        break;

                    case ConsoleKey.Backspace:
                        NavigationService.GoBack();
                        return;

                    case ConsoleKey.Enter:
                        if (selectedSeats.Count != amountSeats)
                        {
                            Console.Clear();
                            LoginStatusHelper.ShowLoginStatus();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n*Je moet alle stoelen selecteren om door te gaan.*");
                            Console.ResetColor();
                            Console.WriteLine("\nDruk op een toets om terug te keren...");
                            Console.ReadKey(true);

                            continue;
                        }
                        else
                        {
                            goto ConfirmSelection;
                        }
                }
            }

ConfirmSelection:

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

            Console.WriteLine("You have selected the following seats:\n");

            foreach (var selectedSeat in selectedSeats)
            {
                var seat = seatLookup[selectedSeat];
                Console.WriteLine($"Row {selectedSeat.row}, Seat {selectedSeat.seat} — {seat.Price:F2} Euro");
            }

            NavigationService.Navigate(() =>
                Checkout.StartCheckout(movieName, sessionTime, new List<(string, int)>(selectedSeats), auditoriumId)
            );
        }
    }
}

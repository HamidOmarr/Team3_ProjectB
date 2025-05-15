namespace Team3_ProjectB
{
    public class Foodmenu
    {
        public static void StartFoodMenu(long reservationId)
        {

            ConsumablesLogic consumablesLogic = new ConsumablesLogic();
            var consumables = consumablesLogic.GetAllConsumables();

            var selectedItems = new Dictionary<long, int>();
            int selectedIndex = 0;
            ConsoleKey key;


            do
            {
                Console.Clear();
                Console.WriteLine("Use ↑ ↓ to navigate, Space to add/remove, Enter to confirm:\n");
                Console.WriteLine("───────────────────────────────────────");

                for (int i = 0; i < consumables.Count; i++)
                {
                    var c = consumables[i];
                    int quantity = selectedItems.ContainsKey(c.Id) ? selectedItems[c.Id] : 0;

                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"[>] {c.Name} - {c.Price} EUR (Selected: {quantity}x)");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"[ ] {c.Name} - {c.Price} EUR (Selected: {quantity}x)");
                    }
                }


                Console.WriteLine("───────────────────────────────────────");
                Console.WriteLine("↑ ↓ = Navigate  |  Space = Add/Remove  |  Enter = Confirm  |  Esc = Cancel");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key == ConsoleKey.DownArrow && selectedIndex < consumables.Count - 1)
                    selectedIndex++;
                else if (key == ConsoleKey.Spacebar)
                {
                    var selectedItem = consumables[selectedIndex];
                    if (selectedItems.ContainsKey(selectedItem.Id))
                    {
                        selectedItems[selectedItem.Id]++;
                    }
                    else
                    {
                        selectedItems[selectedItem.Id] = 1;
                    }
                }
                else if (key == ConsoleKey.Backspace)
                {
                    var selectedItem = consumables[selectedIndex];
                    if (selectedItems.ContainsKey(selectedItem.Id))
                    {
                        selectedItems[selectedItem.Id]--;
                        if (selectedItems[selectedItem.Id] <= 0)
                            selectedItems.Remove(selectedItem.Id);
                    }
                }

            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                Console.WriteLine("No items selected. Returning to the main menu...");
                Console.ReadKey();
                return;
            }

            SaveSelectedItems(reservationId, selectedItems, consumables);

            Console.Clear();
            Console.WriteLine("Your selected items:\n");
            decimal totalPrice = 0;
            foreach (var (id, quantity) in selectedItems)
            {
                var consumable = consumables.First(c => c.Id == id);
                Console.WriteLine($"{consumable.Name} - {quantity}x - {consumable.Price * quantity} EUR");
                totalPrice += (decimal)(consumable.Price * quantity);
            }
            Console.WriteLine($"\nTotal Price: {totalPrice} EUR");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void SaveSelectedItems(long reservationId, Dictionary<long, int> selectedItems, List<ConsumableModel> consumables)
        {
            ReservationConsumablesLogic reservationConsumablesLogic = new ReservationConsumablesLogic();

            foreach (var (consumableId, quantity) in selectedItems)
            {
                var consumable = consumables.First(c => c.Id == consumableId);
                reservationConsumablesLogic.SaveReservationConsumable(new ReservationConsumableModel
                {
                    ReservationId = reservationId,
                    ConsumableId = consumableId,
                    Quantity = quantity,
                    ActualPrice = (decimal)(consumable.Price * quantity)
                });
            }
        }
    }
}
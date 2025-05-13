public class Foodmenu
{
    public static void StartFoodMenu(long reservationId)
    {
        ConsumablesLogic consumablesLogic = new ConsumablesLogic();
        var consumables = consumablesLogic.GetAllConsumables();

        var selectedItems = new Dictionary<long, int>(); // Key: Consumable ID, Value: Quantity
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Food & Drinks Menu:");
            Console.WriteLine("───────────────────────────────────────");
            foreach (var consumable in consumables)
            {
                int quantity = selectedItems.ContainsKey(consumable.Id) ? selectedItems[consumable.Id] : 0;
                Console.WriteLine($"{consumable.Id}. {consumable.Name} - {consumable.Price:C} (Selected: {quantity}x)");
            }
            Console.WriteLine("───────────────────────────────────────");
            Console.WriteLine("Use the item number to select/deselect items.");
            Console.WriteLine("Press Enter to confirm your selection or Esc to cancel.");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Enter)
            {
                break; // Confirm selection
            }
            else if (key == ConsoleKey.Escape)
            {
                Console.WriteLine("No items selected. Returning to the main menu...");
                Console.ReadKey();
                return; // Exit without saving
            }
            else if (key >= ConsoleKey.D1 && key <= ConsoleKey.D9)
            {
                int itemId = key - ConsoleKey.D0;
                var selectedItem = consumables.FirstOrDefault(c => c.Id == itemId);

                if (selectedItem != null)
                {
                    Console.WriteLine($"Do you want to (A)dd or (R)emove {selectedItem.Name}? (A/R)");
                    var action = Console.ReadKey(true).Key;

                    if (action == ConsoleKey.A)
                    {
                        if (selectedItems.ContainsKey(itemId))
                            selectedItems[itemId]++;
                        else
                            selectedItems[itemId] = 1;
                    }
                    else if (action == ConsoleKey.R && selectedItems.ContainsKey(itemId))
                    {
                        selectedItems[itemId]--;
                        if (selectedItems[itemId] == 0)
                            selectedItems.Remove(itemId);
                    }
                }
            }
        }

        // Save selected items to the database
        SaveSelectedItems(reservationId, selectedItems, consumables);

        Console.Clear();
        Console.WriteLine("Your selected items:");
        decimal totalPrice = 0;
        foreach (var (id, quantity) in selectedItems)
        {
            var consumable = consumables.First(c => c.Id == id);
            Console.WriteLine($"{consumable.Name} - {quantity}x - {consumable.Price * quantity:C}");
            totalPrice += (decimal)(consumable.Price * quantity);
        }
        Console.WriteLine($"Total Price: {totalPrice:C}");
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

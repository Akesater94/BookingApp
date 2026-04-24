using EFCore;
using EFCore.Migrations;
using EFCore.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BookingApp;

internal class App
{
    private readonly ICustomerService _customerService;
    private readonly ILiftService _liftService;
    private readonly IBookingService _bookingService;
    public List<Customer> Customers { get; set; } = null!;
    public int Week { get; set; }
    public bool ItemIsNull { get; set; }
    public DateOnly startDate { get; set; }
    public string? PopularItemList { get; set; }
    public List<DateOnly> WeekDays { get; set; } = new List<DateOnly>();
    public bool DeleteMode { get; set; }
    public int Step { get; set; }
    public App()
    {
        var context = new BookingDbContext();

        var customerRepo = new CustomerRepository(context);
        _customerService = new CustomerService(customerRepo);

        var liftRepo = new LiftRepository(context);
        _liftService = new LiftService(liftRepo);

        var bookingRepo = new BookingRepository(context);
        _bookingService = new BookingService(bookingRepo);
    }

    public async Task Run()
    {
        Step = 1;
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        startDate = today;

        while (startDate.DayOfWeek != DayOfWeek.Monday)
        {
            startDate = startDate.AddDays(1);
        }
        Week = ISOWeek.GetWeekOfYear(startDate.ToDateTime(TimeOnly.MinValue));

        WeekDays.Add(startDate);
        WeekDays.Add(startDate.AddDays(1));
        WeekDays.Add(startDate.AddDays(2));
        WeekDays.Add(startDate.AddDays(3));
        WeekDays.Add(startDate.AddDays(4));

        Customer? customer = null;
        Lift? lift = null;
        DateOnly? date = null;
        Booking? booking = null;

        while (true)
        {
            switch (Step)
            {
                case 1:
                    MainMenu();
                    break;
                case 2:
                    Customer? newCustomer = (CreateOrSelectCustomer());
                    if (newCustomer == null)
                    {
                        Step = 1;
                    }
                    else
                    {
                        customer = await _customerService.AddCustomerAsync(newCustomer);
                    }

                    break;
                case 3:
                    Lift? newLift = SelectLift(await _liftService.GetLiftsAsync());
                    if (newLift != null)
                    {
                        lift = newLift;
                    }
                    break;
                case 4:
                    DateOnly? selectedDate = SelectDate(lift);
                    if (selectedDate != null)
                    {
                        date = selectedDate;
                    }
                    break;
                case 5:
                    if (customer != null && lift != null && date != null)
                    {
                        var bookingInfo = BookingConfirmation(customer, lift, date.Value);
                        await _bookingService.AddBookingAsync(bookingInfo.Item1, bookingInfo.Item2, bookingInfo.Item3);
                        Step = 1;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Någonting någonstans blev fel, försök igen.");
                        Console.ReadKey();
                        customer = null;
                        lift = null;
                        date = null;
                        booking = null;
                        Step = 1;
                    }
                    break;
                case 6:
                    AdminMenu();
                    break;
                case 7:
                    customer = AdminCustomerList(await _customerService.GetCustomersAsync());
                    break;
                case 8:
                    booking = AdminCustomerBookings(await _bookingService.GetLiftsAsync(customer.Id));
                    if (DeleteMode)
                    {
                        await _bookingService.DeleteBookingAsync(booking);
                        DeleteMode = false;
                    }
                    break;
                case 9:
                    {
                        var liftDetails = AdminAddLift();
                        object[] items = { liftDetails.Item1, liftDetails.Item2, liftDetails.Item3, liftDetails.Item4, liftDetails.Item5 };
                        lift = new Lift()
                        {
                            Name = liftDetails.Item1,
                            Price = liftDetails.Item2,
                            Height = liftDetails.Item3,
                            Engine = liftDetails.Item4,
                            PlaformCapacityKg = liftDetails.Item5,
                        };

                        foreach (var item in items)
                        {
                            if (item == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Någon av fältena blev inte korrekt inmatade, försök igen.");
                                Console.ReadKey();
                                ItemIsNull = true;
                                break;
                            }
                        }
                        if (ItemIsNull == false)
                        {
                            await _liftService.AddLiftAsync(lift);
                            Console.Clear();
                            Console.WriteLine($"{lift.Name} har lagts till!");
                            Console.ReadKey();
                        }

                    }
                    Step = 6;
                    break;
                case 10:
                    if (PopularItemList.ToLower() == "customer")
                    {
                        List<Customer> customers = await _customerService.GetCustomersAsync();
                        AdminMostPopular(customers, "Våra mest trogna kunder", c => c.Name, c => c.Bookings.Count);
                    }
                    if (PopularItemList.ToLower() == "lift")
                    {

                        List<Lift> lifts = await _liftService.GetLiftsAsync();
                        AdminMostPopular(lifts, "Våra mest bokade liftar", l => l.Name, l => l.Bookings.Count);
                    }
                    break;
                case 11:
                    //AdminMostLoyalCustomers();
                    break;
            }

        }
    }
    private void AdminMostPopular<T>(List<T> items, string windowTitle, Func<T, string> itemName, Func<T, int> bookingCount)
    {
        Console.Clear();
        List<string> itemContent = new List<string>();
        var sortedItems = items.OrderByDescending(bookingCount).ToList();

        for (int i = 0; i < sortedItems.Count; i++)
        {
            itemContent.Add($"{i + 1}. {itemName(sortedItems[i]).PadRight(25)} antal bokningar: {bookingCount(sortedItems[i])}");
        }
        Window liftWindow = new Window(windowTitle, 0, 0, itemContent);
        liftWindow.Draw();
        if (PopularItemList == "customer")
        {
            Console.WriteLine("Tryck L för att se mest hyrda liftar");
        }
        if (PopularItemList == "lift")
        {
            Console.WriteLine("Tryck K för att se mest populära kunder");
        }
        Console.WriteLine("Tryck C för att gå tillbaka till menyn");
        var key = KeyInput().Key;
        switch (key)
        {
            case ConsoleKey.C:
                Step = 6;
                break;
            case ConsoleKey.K:
                PopularItemList = "customer";
                break;
            case ConsoleKey.L:
                PopularItemList = "lift";
                break;
        }
    }

    private (string, int, int, string, int) AdminAddLift()
    {

        Console.Clear();
        Window NameWindow = new Window("Namn", 0, 0, new List<string> { "Vänligen fyll i namnet på liften" });
        NameWindow.Draw();
        string? liftName = Console.ReadLine();

        Console.Clear();
        Window priceWindow = new Window("Pris", 0, 0, new List<string> { "Vänligen fyll i timpriset" });
        priceWindow.Draw();
        int.TryParse(Console.ReadLine(), out int liftPrice);

        Console.Clear();
        Window heightWindow = new Window("Höjd", 0, 0, new List<string> { "Vänligen fyll i markhöjden" });
        heightWindow.Draw();
        int.TryParse(Console.ReadLine(), out int liftHeight);

        Console.Clear();
        string? liftEngine;
        Window engineWindow = new Window("Motor", 0, 0, new List<string> { "Vänligen välj en motor", "1. Elektrisk", "2. Diesel" });
        engineWindow.Draw();
        var key = Console.ReadKey().Key;
        switch (key)
        {
            case ConsoleKey.D1:
                liftEngine = "Elektrisk";
                break;
            case ConsoleKey.D2:
                liftEngine = "Diesel";
                break;
            default:
                liftEngine = null;
                break;
        }

        Console.Clear();
        Window maxLoadWindow = new Window("Motor", 0, 0, new List<string> { "Vänligen fyll i maxvikten för liftens plattform" });
        maxLoadWindow.Draw();
        int.TryParse(Console.ReadLine(), out int liftMaxWeight);

        return (liftName, liftPrice, liftHeight, liftEngine, liftMaxWeight);
    }

    private Customer? AdminCustomerList(List<Customer> customers)
    {
        Console.Clear();
        List<string> adminCustomerContent = new List<string>();
        for (int i = 0; i < customers.Count; i++)
        {
            if (customers[i] != null)
            {
                adminCustomerContent.Add((i + 1).ToString() + ". " + customers[i].Name);
            }
        }

        Window adminCustomerWindow = new Window("Kunder", 0, 0, adminCustomerContent);
        adminCustomerWindow.Draw();
        Console.WriteLine("Tryck K för att skriva in ett kundnummer");
        Console.WriteLine("Tryck C för att komma tillbaka");
        ConsoleKey key = Console.ReadKey().Key;
        switch (key)
        {
            case ConsoleKey.K:
                Console.Clear();
                adminCustomerWindow.Draw();
                Console.Write("Skriv in ett kundnummer och tryck enter: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= customers.Count)
                {
                    index--;
                    Step++;
                    return customers[index];
                }
                break;
            case ConsoleKey.C:
                Step--;
                break;
        }
        return null;

    }

    private Booking? AdminCustomerBookings(List<Booking> bookings)
    {
        Console.Clear();
        List<string> customerBookings = new List<string>();

        if (bookings.IsNullOrEmpty())
        {
            Console.Write("Inga bokningar fanns att hitta.");
            Step = 7;
            Console.ReadKey();
            return null;
        }

        string customerName = bookings.FirstOrDefault().Customer.Name;

        for (int i = 0; i < bookings.Count; i++)
        {
            string liftName = bookings[i].Lift.Name;
            customerBookings.Add($"{(i + 1).ToString().PadRight(4)}  {liftName.PadRight(12)} Bokad till: {bookings[i].BookedDate.Value}");
        }

        Window bookingWindow = new Window(customerName + "s bokningar", 0, 0, customerBookings);
        bookingWindow.Draw();
        Console.WriteLine("Tryck D för att välja en bokning att ta bort");
        Console.WriteLine("Tryck C för att gå tillbaka");
        ConsoleKey key = Console.ReadKey().Key;
        switch (key)
        {
            case ConsoleKey.D:
                Console.Clear();
                bookingWindow.Draw();
                DeleteMode = true;
                Console.Write("Skriv numret för en bokning att ta bort och tryck enter: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    index--;
                    if (index >= 0 && index <= bookings.Count)
                    {
                        return bookings[index];
                    }
                }
                break;
            case ConsoleKey.C:
                Step = 7;
                break;
        }
        return null;
    }

    private void AdminMenu()
    {
        Console.Clear();
        List<string> adminContent = new List<string>()
        {
            "1. Visa alla kunder.",
            "2. Visa våra mest populära liftar.",
            "3. Lägg till lift.",
        };

        Window adminWindow = new Window("Admin", 0, 0, adminContent);
        adminWindow.Draw();
        ReturnMessage();

        ConsoleKey input = KeyInput().Key;
        if (input == ConsoleKey.D1)
        {
            Step++;
        }
        if (input == ConsoleKey.D2)
        {
            PopularItemList = "lift";
            Step = 10;
        }
        if (input == ConsoleKey.D3)
        {
            Step = 9;
        }
    }

    private (int, int, DateOnly) BookingConfirmation(Customer customer, Lift lift, DateOnly date)
    {
        Console.Clear();
        List<string> confirmationContent = new List<string>()
        {
            $"{customer.Name} har bokat {lift.Name}",
            $"Till dagen: {date.ToString()}"
        };

        Window confirmationWindow = new Window("", 0, 0, confirmationContent);
        confirmationWindow.Draw();
        Console.Write("Tryck på valfri knapp för att gå till huvudmenyn: ");
        Console.ReadKey();
        Step = 1;
        return (customer.Id, lift.Id, date);
    }

    private DateOnly? SelectDate(Lift lift)
    {
        Console.Clear();

        List<string> calenderContent = new List<string>()
        {
            "Vecka: " + Week
        };
        var x = lift;

        List<DateOnly?> dates = lift.Bookings
                .Where(b => b.LiftId == lift.Id)
                .Select(b => b.BookedDate)
                .ToList();

        for (int i = 0; i < WeekDays.Count; i++)
        {
            if (dates.Contains(WeekDays[i]))
            {
                calenderContent.Add((WeekDays[i].DayOfWeek.ToString().PadRight(15) + "[X]".PadRight(5) + WeekDays[i].ToString()));
            }
            else
            {
                calenderContent.Add(WeekDays[i].DayOfWeek.ToString().PadRight(15) + (i + 1).ToString().PadRight(5) + WeekDays[i].ToString());
            }
        }

        Window calenderWindow = new Window("", 0, 0, calenderContent);
        calenderWindow.Draw();
        ReturnMessage();
        Console.WriteLine("Tryck högerpil för att gå fram en vecka.");
        Console.WriteLine("Tryck vänsterpil för att backa en vecka.");
        Console.Write("Tryck motsvarande siffra för att boka den dagen: ");

        var Input = KeyInput();

        if (Input.Key == ConsoleKey.RightArrow)
        {
            IncreaseWeek();
            return null;
        }
        if (Input.Key == ConsoleKey.LeftArrow)
        {
            DecreaseWeek();
            return null;
        }

        if (int.TryParse(Input.KeyChar.ToString(), out int index))
        {
            if (index <= WeekDays.Count && index > 0 && !dates.Contains(WeekDays[index - 1]))
            {
                Step++;
                return WeekDays[index - 1];
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ogiltigt val!");
                Console.ReadKey();
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    private void IncreaseWeek()
    {
        for (int i = 0; i < WeekDays.Count; i++)
        {
            WeekDays[i] = WeekDays[i].AddDays(7);
        }
        Week = ISOWeek.GetWeekOfYear(WeekDays[1].ToDateTime(TimeOnly.MinValue));
    }
    private void DecreaseWeek()
    {
        if (WeekDays[0].AddDays(-7) < DateOnly.FromDateTime(DateTime.Now))
        {
            return;
        }
        else
        {
            for (int i = 0; i < WeekDays.Count; i++)
            {
                WeekDays[i] = WeekDays[i].AddDays(-7);
                Week = ISOWeek.GetWeekOfYear(WeekDays[i].ToDateTime(TimeOnly.MinValue));
            }
        }
    }

    private void MainMenu()
    {
        Console.Clear();
        List<string> menuContent = new List<string>()
        {
            "1. Boka en lift",
            "2. Admin",
        };

        Window menuWindow = new Window("Huvudmeny", 0, 0, menuContent);
        menuWindow.Draw();

        var input = KeyInput();
        if (input.Key == ConsoleKey.D1)
            Step++;
        if (input.Key == ConsoleKey.D2)
            Step = 6;
    }

    private Customer? CreateOrSelectCustomer()
    {
        Console.Clear();
        List<string> customerInfo = new List<string>()
            {
                "Fyll i ditt namn: "
            };
        Window customerWindow = new Window("", 0, 0, customerInfo);
        customerWindow.WidestLineWidth = 50;
        customerWindow.Draw();
        Console.SetCursorPosition(20, 1);
        string? customerName = Console.ReadLine();
        if (customerName == null || customerName == string.Empty)
        {
            Console.Clear();
            Console.WriteLine("Du måste fylla i ett namn...");
            Console.WriteLine("Tryck på valfri knapp för att gå tillbaka.");
            Console.ReadKey();
            Console.Clear();
            Step = 1;
            return null;
        }
        Customer newCustomer = new Customer()
        {
            Name = customerName!
        };

        Step++;
        return newCustomer;

    }

    private Lift? SelectLift(List<Lift> lifts)
    {
        Console.Clear();
        List<string> liftContent = new List<string>()
        {
            "#".PadRight(5)+"Namn".PadRight(20)+"höjd(m)".PadRight(14)+"kr/h".PadRight(7)+"motor".PadRight(15)
        };
        for (int i = 0; i < lifts.Count; i++)
        {
            liftContent.Add($"{(i + 1).ToString().PadRight(5)}{lifts[i].Name.PadRight(20)}{lifts[i].Height.ToString().PadRight(14)}{lifts[i].Price.ToString().PadRight(7)}{lifts[i].Engine.PadRight(15)}");
        }

        Window liftWindow = new Window("Liftar", 0, 0, liftContent);
        liftWindow.Draw();

        ReturnMessage();
        Console.Write("Välj en siffra för att välja lift: ");
        var input = KeyInput();
        if (int.TryParse(input.KeyChar.ToString(), out int index))
        {
            if (index <= lifts.Count && index >= 0)
            {
                Step++;
                return lifts[index - 1];
            }
            else
            {
                Console.Clear();
                Console.Write($"Var vänlig och välj en siffra mellan 1 - {lifts.Count}");
                Console.ReadKey();
                return null;
            }
        }
        return null;
    }
    private ConsoleKeyInfo KeyInput()
    {
        var keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.C:
                if (Step == 3 || Step == 6)
                {
                    Step = 1;
                }
                else
                {
                    Step -= 1;
                }
                break;
            case ConsoleKey.M:
                Step = 1;
                break;
        }

        return keyInfo;
    }

    private void ReturnMessage()
    {
        List<string> returnContent = new List<string>()
        {
            "Tryck C för att gå tillbaka",
            "Tryck M för att komma till huvudmenyn, OBS dina ändringar kommer inte att sparas"
        };

        Window returnWindow = new Window("", 0, 20, returnContent);
        returnWindow.Draw();
    }

    private void TestTool()
    {
        Console.Clear();
        Console.WriteLine("Du är nu på steg: " + Step);
        Console.ReadKey();
        Step = 1;
    }
}

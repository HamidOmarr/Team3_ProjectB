using Xunit;
using System.Collections.Generic;
using Team3_ProjectB;


public class AccountsLogicTests
{
    [Fact]
    public void SetCurrentAccount_SetsCorrectAccount()
    {
        var acc = new AccountModel(1, "Test User", "tjeerd@test.com", "password", "normal");

        AccountsLogic.SetCurrentAccount(acc);
        Assert.Equal("tjeerd@test.com", AccountsLogic.CurrentAccount?.Email);
    }

    [Fact]
    public void CheckLogin_Valid_ReturnsAccount()
    {
        var logic = new AccountsLogic();
        var acc = new AccountModel(0, "User", "jakhals@giraffe.com", "pass", "normal");
        AccountsAccess.Write(acc);

        var result = logic.CheckLogin("jakhals@giraffe.com", "pass");

        Assert.NotNull(result);
        Assert.Equal("jakhals@giraffe.com", result.Email);
    }

    [Fact]
    public void CheckLogin_Invalid_ReturnsNull()
    {
        var logic = new AccountsLogic();
        var result = logic.CheckLogin("wrong@address.com", "incorrect");

        Assert.Null(result);
    }

    [Fact]
    public void WriteAccount_ReturnsValidId()
    {
        var logic = new AccountsLogic();
        var acc = new AccountModel(0, "Test", "valid@domain.com", "1234", "normal");

        var id = logic.WriteAccount(acc);

        Assert.True(id > 0);
    }
}


public class ConsumablesLogicTests
{
    [Fact]
    public void GetAllConsumables_ReturnsList()
    {
        var logic = new ConsumablesLogic();
        var result = logic.GetAllConsumables();
        Assert.NotNull(result);
    }
}

public class MoviesLogicTests
{
    [Fact]
    public void GetAllMovies_ReturnsList()
    {
        var logic = new MoviesLogic();
        var result = logic.GetAllMovies();
        Assert.NotNull(result);
    }
}

public class PricesLogicTests
{
    [Fact]
    public void GetPrice_Standard_ReturnsValue()
    {
        var logic = new PricesLogic();
        var result = logic.GetPrice(1, null);
        Assert.True(result >= 0);
    }
}

public class ReservationConsumablesLogicTests
{
    [Fact]
    public void SaveAndFetchConsumables_Works()
    {
        var logic = new ReservationConsumablesLogic();
        var model = new ReservationConsumableModel
        {
            ReservationId = 999,
            ConsumableId = 1,
            Quantity = 2,
            ActualPrice = 10
        };
        logic.SaveReservationConsumable(model);
        var result = logic.GetConsumablesForCheckout(999);
        Assert.Contains(result, i => i.Quantity == 2);
    }
}

public class ReservationsLogicTests
{
    [Fact]
    public void CreateReservation_ReturnsId()
    {
        var logic = new ReservationsLogic();
        var res = new ReservationModel { UserId = 1, Status = "pending", TotalPrice = 0 };
        var id = logic.CreateReservation(res);
        Assert.True(id > 0);
    }
}

public class SeatsLogicTests
{
    [Fact]
    public void GetSeatsByAuditorium_ReturnsSeats()
    {
        var logic = new SeatsLogic();
        var result = logic.GetSeatsByAuditorium(1);
        Assert.NotNull(result);
    }

    [Fact]
    public void GetSeatByRowAndNumber_ReturnsSeat()
    {
        var logic = new SeatsLogic();
        var result = logic.GetSeatByRowAndNumber("A", 1, 1);
        Assert.NotNull(result);
    }

    [Fact]
    public void GetReservedSeatIds_ReturnsSet()
    {
        var result = SeatsLogic.GetReservedSeatIds(1);
        Assert.NotNull(result);
    }
}

public class TicketsLogicTests
{
    [Fact]
    public void CreateAndGetTicket_Works()
    {
        var logic = new TicketsLogic();
        var ticket = new TicketModel
        {
            ReservationId = 1,
            MovieSessionId = 1,
            SeatId = 1,
            ActualPrice = 9
        };
        logic.CreateTicket(ticket);
        var result = logic.GetTicketById(ticket.Id);
        Assert.NotNull(result);
    }
}

public class ShowMoviesTests
{
    [Fact]
    public void Constructor_LoadsMovies()
    {
        var sm = new ShowMovies();
        Assert.NotNull(sm.Movies);
    }
}

public class ShowMoviesManagerTests
{
    [Fact]
    public void Constructor_LoadsSessions()
    {
        var manager = new ShowMoviesManager();
        Assert.NotNull(manager.MovieSessions);
    }
}

public class SeatSelectionTests
{
    [Fact]
    public void SetAmountSeats_AssignsCorrectValue()
    {
        SeatSelection.AmountSeats = 5;
        Assert.Equal(5, SeatSelection.AmountSeats);
    }
}

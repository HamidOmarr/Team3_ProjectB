namespace Team3_ProjectB
{
    public class TicketsLogic
{
    public void CreateTicket(TicketModel ticket)
    {
        TicketsAccess.Create(ticket);
    }

    public TicketModel GetTicketById(long id)
    {
        return TicketsAccess.GetById(id);
    }
}
}
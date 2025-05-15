namespace Team3_ProjectB
{
    public class ConsumablesLogic
    {
        private readonly ConsumablesAccess _consumablesAccess;

        public ConsumablesLogic()
        {
            _consumablesAccess = new ConsumablesAccess();
        }

        public List<ConsumableModel> GetAllConsumables()
        {
            return _consumablesAccess.GetAllConsumables();
        }
    }
}
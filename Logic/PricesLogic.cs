﻿namespace Team3_ProjectB
{
    public class PricesLogic
    {
        public decimal GetPrice(int seatTypeId, int? promotionTypeId)
        {
            return PriceAccess.GetPrice(seatTypeId, promotionTypeId);
        }
    }
}
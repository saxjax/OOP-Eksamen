using System;
namespace FKlubStregsystem
{
    public class SeasonalProduct: Product
    {
        private DateTime _seasonStartDate;
        private DateTime _seasonEndDate;

        #region Public Attributes

        public DateTime SeasonStartDate
        {
            get => _seasonStartDate;
            set => _seasonStartDate = value;
        }

        public DateTime SeasonEndDate
        {
            get => _seasonEndDate;
            set => _seasonEndDate = value;
        }

        #endregion

        #region Constructor
        void Initialize(DateTime seasonStartDate, DateTime seasonEndDate)
        {
            _seasonStartDate = seasonStartDate;
            _seasonEndDate = seasonEndDate;
        }

        public SeasonalProduct(int id, string name, decimal price, bool active = true, bool canBeBoughtOnCredit = false,
            DateTime seasonStartDate = new DateTime(), DateTime seasonEndDate = new DateTime())
            : base(id, name, price, active, canBeBoughtOnCredit)
        {
            Initialize(seasonStartDate, seasonEndDate);
        }

        public SeasonalProduct(int id, string name, decimal price, string seasonStartDate = "", string seasonEndDate = "",
            bool active = true, bool canBeBoughtOnCredit = false)
            : base(id, name, price, active, canBeBoughtOnCredit)
        {
            DateTime startDate = seasonStartDate == "" ? new DateTime() : Convert.ToDateTime(seasonStartDate);
            DateTime endDate = seasonEndDate == "" ? new DateTime() : Convert.ToDateTime(seasonEndDate);

            Initialize(seasonStartDate: startDate, seasonEndDate: endDate);
        }
        #endregion


    }
}

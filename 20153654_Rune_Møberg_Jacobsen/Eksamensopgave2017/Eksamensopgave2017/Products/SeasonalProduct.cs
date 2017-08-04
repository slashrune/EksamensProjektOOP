using System;

namespace Eksamensopgave2017
{
    public class SeasonalProduct : Product
    {

        #region Private Fields

        /// <summary>
        /// Validated date of the start of the season
        /// </summary>
        private DateTime _seasonStartDate;
        /// <summary>
        /// Validated date of the start of the season
        /// </summary>
        private DateTime _seasonEndDate;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="active"></param>
        /// <param name="seasonStartDate"></param>
        /// <param name="seasonEndDate"></param>
        public SeasonalProduct(int productID, string name, decimal price, bool active,
                               string seasonStartDate, string seasonEndDate)
            : base(productID, name, price, active)
        {
            SeasonStartDate = DateTime.Parse(seasonStartDate);
            SeasonEndDate = DateTime.Parse(seasonEndDate);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The date of the start of the season of the product
        /// </summary>
        public DateTime SeasonStartDate
        {
            get { return _seasonStartDate; }
            set
            {
                if (value > DateTime.Now)
                    _seasonStartDate = value;
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// The date of the end of the season
        /// </summary>
        public DateTime SeasonEndDate
        {
            get { return _seasonEndDate; }
            set
            {
                if (value > DateTime.Now)
                    _seasonEndDate = value;
                throw new ArgumentOutOfRangeException();

            }
        }

        #endregion
    }
}

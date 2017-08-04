using System;

namespace Eksamensopgave2017
{
    public class Product
    {
        #region Private Members

        /// <summary>
        /// The name of the product, that is not null
        /// </summary>
        private string _name;
        /// <summary>
        /// The price of the product
        /// </summary>
        private decimal _price;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="active"></param>
        /// <param name="canBeBoughtOnCredit"></param>
        public Product(int productID, string name, decimal price, bool active)
        {
            Name = name;
            Price = price;
            Active = active;
            ProductID = productID;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Accessor for the Product ID, is ReadOnly
        /// </summary>
        public int ProductID { get; }

        /// <summary>
        /// Property validating name not to be null
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
                else
                    throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                _price = value;
            }
        }

        /// <summary>
        /// Is true if a product is active, and can be sold
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Is true if the product is possible to buy if the user have no money on their account
        /// </summary>
        public bool CanBeBoughtOnCredit { get; set; }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Overridden ToString() method for a product
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ProductID} - {Name} - {Price} dkr";
        }

        #endregion
    }
}

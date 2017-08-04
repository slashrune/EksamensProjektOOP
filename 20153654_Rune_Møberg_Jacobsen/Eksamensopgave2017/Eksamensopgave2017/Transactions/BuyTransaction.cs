using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class BuyTransaction : Transaction
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transactionUser"></param>
        /// <param name="productToBy"></param>
        public BuyTransaction(int transactionId, User transactionUser, Product productToBuy)
            : base(transactionId, transactionUser, productToBuy.Price)
        {
            TransactionUser = transactionUser;
            ProductToBuy = productToBuy;
            Amount = productToBuy.Price;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Product to be bought
        /// </summary>
        public Product ProductToBuy { get; set; }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Executes the transaction and writes to logfile
        /// </summary>
        public override void Execute()
        {
            if (TransactionUser.Balance < Amount && !ProductToBuy.CanBeBoughtOnCredit)
                throw new InsufficientCreditsException(TransactionUser, ProductToBuy);
            else
                TransactionUser.Balance -= Amount;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}

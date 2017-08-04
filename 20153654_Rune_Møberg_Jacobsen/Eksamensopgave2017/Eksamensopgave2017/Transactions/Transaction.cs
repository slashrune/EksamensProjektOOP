using System;

namespace Eksamensopgave2017
{
    public abstract class Transaction
    {
        #region Private Members

        /// <summary>
        /// The user of the transaction
        /// </summary>
        private User _transactionUser;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transactionUser"></param>
        public Transaction(int transactionID, User transactionUser, decimal amount)
        {
            TransactionUser = transactionUser;
            Amount = amount;
            DateOfTransaction = DateTime.Now;
            TransactionID = transactionID;
        }

        #endregion

        #region Public properties 

        /// <summary>
        /// Accessor for the transaction ID
        /// </summary>
        public int TransactionID { get; }

        /// <summary>
        /// Amount to be inserted or withdrawn
        /// </summary>
        public decimal Amount { get; protected set; }

        /// <summary>
        /// Property for the user making an transaction, validating that the user is not null
        /// </summary>
        public User TransactionUser
        {
            get { return _transactionUser; }
            set
            {
                if (value != null)
                    _transactionUser = value;
                else
                    throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Holds the date for the transaction
        /// </summary>
        public DateTime DateOfTransaction { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// An abstract class demanding an implementation of an Execute method
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Overriden ToString()
        /// </summary>
        /// <returns> A string containing ID, User, Amount and time information </returns>
        public override string ToString()
        {
            return $"ID: {TransactionID} User: {TransactionUser} Amount: {Amount} Date/Time: {DateOfTransaction.ToShortDateString()} - {DateOfTransaction.ToShortTimeString()}";
        }

        #endregion
    }
}

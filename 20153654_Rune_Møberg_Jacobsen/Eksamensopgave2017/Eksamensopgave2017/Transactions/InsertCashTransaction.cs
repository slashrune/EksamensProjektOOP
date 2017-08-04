namespace Eksamensopgave2017
{
    public class InsertCashTransaction : Transaction
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="transactionUser"></param>
        public InsertCashTransaction(int transactionId, User transactionUser, decimal amount)
            : base(transactionId, transactionUser, amount)
        {
            TransactionUser = transactionUser;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method that executes the insert cash transaction
        /// </summary>
        public override void Execute()
        {
            TransactionUser.Balance += Amount;
        }

        /// <summary>
        /// Overriden To string method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Cash insertion: {base.ToString()}";
        }

        #endregion
    }
}

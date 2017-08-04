using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave2017
{
    /// <summary>
    /// Delegate for the command typed in by the user
    /// </summary>
    /// <param name="command"></param>
    public delegate void StregsystemEvent(string command);
    
    class Stregsystem : IStregsystem
    {
        #region Private Members

        /// <summary>
        /// List of users in the system
        /// </summary>
        private List<User> _listOfUsers = new List<User>();
        /// <summary>
        /// List of products in the system
        /// </summary>
        private List<Product> _listOfProducts = new List<Product>();
        /// <summary>
        /// List of all transactions conducted in the System
        /// </summary>
        private List<Transaction> ListOfTransactions = new List<Transaction>();
        /// <summary>
        /// Hold an instance of a product file
        /// </summary>
        private FileHandler _productFile;
        /// <summary>
        /// Hold an instance of a user file
        /// </summary>
        private FileHandler _userFile;
        /// <summary>
        /// Log file for transactions
        /// </summary>
        private FileHandler _logFile;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Stregsystem()
        {
            _userFile = new FileHandler(@"../../../users.csv");
            _productFile = new FileHandler(@"../../../products.csv");
            _logFile = new FileHandler(@"../../../transactionlog.txt");
            GetProductsFromFile();
            GetUsersFromFile();
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Enumerable containing all products that are active
        /// </summary>
        public IEnumerable<Product> ActiveProducts => _listOfProducts.Where(p => p.Active);

        #endregion

        #region Events

        /// <summary>
        /// The event to be raised if the user has a low balance
        /// </summary>
        public event UserBalanceNotification UserBalanceWarning;

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes a user and an amount and returns the transaction
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            Transaction transaction = new InsertCashTransaction(_logFile.LinesInFile + 1, user, amount);

            ExecuteTransaction(transaction);

            return transaction as InsertCashTransaction;
        }

        /// <summary>
        /// Takes a user and a product and returns a BuyTransaction
        /// </summary>
        /// <param name="user"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public BuyTransaction BuyProduct(User user, Product product)
        {
            Transaction transaction = new BuyTransaction(_logFile.LinesInFile + 1, user, product);

            ExecuteTransaction(transaction);

            if (user.Balance < 50)
                UserBalanceWarning(user, user.Balance);

            return transaction as BuyTransaction;
        }

        /// <summary>
        /// Returns a product when receiving a product ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product GetProductByID(int productID)
        {
            Product product = ActiveProducts.FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
                return product;

            throw new ProductNotFoundException();
        }

        /// <summary>
        /// Returns a enumerable of transactions of a user and a specified number of transaction
        /// </summary>
        /// <param name="user"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return ListOfTransactions
                .Where(t => t.TransactionUser.Equals(user))
                .OrderByDescending(t => t.DateOfTransaction)
                .Take(count);
        }

        /// <summary>
        /// By giving a usre name the methods returns a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(string username)
        {
            User user = _listOfUsers.Find(u => u.UserName == username);

            if (user != null)
                return user;  

            throw new UserNotFoundException();
        }

        /// <summary>
        /// Returns a user that lives up to the given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public User GetUser(Func<User, bool> predicate)
        {
            return _listOfUsers.FirstOrDefault(predicate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes a given transaction
        /// </summary>
        /// <param name="transaction"></param>
        private void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
            }
            catch (InsufficientCreditsException)
            {
                throw;
            }

            ListOfTransactions.Add(transaction);
            _logFile.WriteStringToFile($"{transaction}"); 
        }

        /// <summary>
        /// Gets the products from a file and fill them into a list
        /// Uses static class <see cref="StringCleaner"/> methods to clean the information for noise characters
        /// </summary>
        private void GetProductsFromFile()
        {
            IEnumerable<string[]> _cleanProductInformation = _productFile.GetCellsInLinesOfFile(';')
                .Select(s => StringCleaner.RemoveCharacterFromStringArray(s, '"'))
                .Select(s => StringCleaner.RemoveHtmlTagsFromStringArray(s)).Skip(1);

            _listOfProducts = MakeProductsFromInformation(_cleanProductInformation);
        }

        /// <summary>
        /// Takes an array of product information and returns a list of product objects
        /// </summary>
        /// <param name="productArray"></param>
        /// <returns></returns>
        public List<Product> MakeProductsFromInformation(IEnumerable<string[]> productArray)
        {
            return productArray.Select(x =>
                new Product(
                    int.Parse(x[0]),
                    x[1],
                    decimal.Parse(x[2]) / 100,
                    x[3].Equals("1")
                    )
                ).ToList();
        }

        /// <summary>
        /// Gets the products from a file an fill them into a list.
        /// Uses static class <see cref="StringCleaner"/> methods to clean for noise characters
        /// </summary>
        private void GetUsersFromFile() 
        {
            IEnumerable<string[]> CleanUserInformation = _userFile.GetCellsInLinesOfFile(';')
                .Select(s => StringCleaner.RemoveCharacterFromStringArray(s, '"')).Skip(1);

            _listOfUsers = RegisterUsers(CleanUserInformation);
        }

        /// <summary>
        /// Takes an array of product information and returns a list of user objects
        /// </summary>
        /// <param name="userArray"></param>
        /// <returns></returns>
        public static List<User> RegisterUsers(IEnumerable<string[]> userArray)
        {
            return userArray.Select(x => new User(x[1], x[2], x[3], x[4], decimal.Parse(x[5]))).ToList();
        }

        #endregion
    }
}
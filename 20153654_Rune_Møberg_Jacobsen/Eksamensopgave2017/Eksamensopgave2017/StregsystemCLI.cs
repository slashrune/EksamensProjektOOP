using System;
using System.Linq;

namespace Eksamensopgave2017
{
    class StregsystemCLI : IStregsystemUI
    {
        #region Private Members

        /// <summary>
        /// Is true if the program is running, turns false in <see cref="Close"/> 
        /// </summary>
        private bool _running;
        /// <summary>
        /// Reference to the stregsystem
        /// </summary>
        private IStregsystem _stregsystem;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="stregsystem"></param>
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            // Event subscription
            _stregsystem.UserBalanceWarning += DisplayUserBalanceWarning;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that is raised when command is entered
        /// </summary>
        public event StregsystemEvent CommandEntered;

        #endregion

        #region Public Methods

        /// <summary>
        /// The start method keeps the system runnung and prints the menu and awaits commands
        /// </summary>
        public void Start()
        {
            _running = true;
            do
            {
                DisplayMenu();
                OnCommandEntered(Console.ReadLine());
            } while (_running);
        }

        /// <summary>
        /// Method that when called sets the running field to false and breaks the loop in <see cref="Start()"/>
        /// </summary>
        public void Close() => _running = false;

        /// <summary>
        /// Displays the message if the admin commmand was not found
        /// </summary>
        /// <param name="adminCommand"></param>
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"---\nThe command {adminCommand} was not found\n---");
        }

        /// <summary>
        /// Display a general error in the terminal
        /// </summary>
        /// <param name="errorString"></param>
        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"---\nError occured: {errorString}\n---");
        }

        /// <summary>
        /// Display a message describing the buyer and the product that did not succeed because of insufficient cash 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="product"></param>
        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"---\nThe user: {user.UserName} does not have enough cash to buy: {product.Name}\n---");
        }

        /// <summary>
        /// Displays a message of a product that could not be found
        /// </summary>
        /// <param name="product"></param>
        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"---\n{product} was not found\n---");
        }

        /// <summary>
        /// Displays a message if two many arguments was given in a command
        /// </summary>
        /// <param name="command"></param>
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"---\nTo many arguments was written at purchase: {command}\n---");
        }

        /// <summary>
        /// Displays a succesfull transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"---\n{transaction.TransactionUser.UserName} bought {transaction.ProductToBuy.Name} for {transaction.Amount} dkr\n---");
        }

        /// <summary>
        /// Displays a succesfull transaction and the amount of products
        /// </summary>
        /// <param name="count"></param>
        /// <param name="transaction"></param>
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"---\n{transaction} Amount: {count}\n---");
        }

        /// <summary>
        /// Display information about a user
        /// </summary>
        /// <param name="user"></param>
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"---\nUser: {user} Balance: {user.Balance}\n---");
            _stregsystem.GetTransactions(user, 10).ToList().ForEach(Console.WriteLine);
            if (user.Balance < 50)
                DisplayUserBalanceWarning(user, user.Balance);
        }

        /// <summary>
        /// Display a message showing if a user is not found and the given username
        /// </summary>
        /// <param name="userName"></param>
        public void DisplayUserNotFound(string userName)
        {
            Console.WriteLine($"---\nThe username: {userName} does not exist\n---");
        }

        /// <summary>
        /// Prints out all active products
        /// </summary>
        public void DisplayMenu()
        {
            _stregsystem.ActiveProducts.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("---\nType in your user name and a product id to make a purchase ex. UserName 11");
            Console.WriteLine("Make a multi purchase by writing username, number of products and the product id ex. UserName 2 11\n---");
        }

        /// <summary>
        /// Displays  a message of the user balance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="balance"></param>
        public void DisplayUserBalanceWarning(User user, decimal balance)
        {
            Console.WriteLine($"{user.UserName} have only {balance} left on the account\n---");
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Method to invoke the command entered event (raise). Is checked for subscribers before invoking
        /// </summary>
        /// <param name="command"></param>
        protected virtual void OnCommandEntered(string command)
        {
            CommandEntered?.Invoke(command);
        }

        #endregion
    }
}
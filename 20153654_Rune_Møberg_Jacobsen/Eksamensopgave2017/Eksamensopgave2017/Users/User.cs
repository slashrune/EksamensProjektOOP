using System;
using System.Linq;

namespace Eksamensopgave2017
{
    /// <summary>
    /// The delegate for the User balance event
    /// </summary>
    /// <param name="user">The user</param>
    /// <param name="balance">The balance of the user</param>
    public delegate void UserBalanceNotification(User user, decimal balance);

    public class User : IComparable<User>
    {
        #region Private members

        /// <summary>
        /// The static user id number
        /// </summary>
        private static int _userId= 1;
        /// <summary>
        /// Holds the private user ID
        /// </summary>
        private int _userID;
        /// <summary>
        /// The validated first name of the user
        /// </summary>
        private string _firstName;
        /// <summary>
        /// The validated last name of the user
        /// </summary>
        private string _lastName;
        /// <summary>
        /// The validated user name of the user
        /// </summary>
        private string _userName;
        /// <summary>
        /// The validated email of the user
        /// </summary>
        private string _email;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="balance"></param>
        public User(string firstName, string lastName, string userName, string email, decimal balance)
        {
            FirstName = firstName;
            Lastname = lastName;
            UserName = userName;
            Email = email;
            Balance = balance;
            _userID = _userId++;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The property to get the user id
        /// </summary>
        public int UserID => _userID;

        /// <summary>
        /// Property validating first name to not be null
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstName = value;
                else
                    throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Property validating first name to not be null
        /// </summary>
        public string Lastname
        {
            get { return _lastName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastName = value;
                else
                    throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Property validating the user name
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value.All(c => char.IsLower(c) || char.IsDigit(c) || c.Equals('_')))
                    _userName = value;
                else
                    throw new UserNameNotValidException();
            }
        }

        /// <summary>
        /// Holds the email of the user validates by <see cref="IsEmailValid(string)"/> 
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (IsEmailValid(value))
                    _email = value;
                else
                    throw new EmailNotValidException();
            }
        }

        /// <summary>
        /// Property handling access to the money of the user
        /// </summary>
        public decimal Balance { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overriden to string method
        /// </summary>
        /// <returns>Firstname Lastname (Email)</returns>
        public override string ToString()
        {
            return $"{FirstName} {Lastname} ({Email})";
        }

        /// <summary>
        /// Overriden Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            User u = (User)obj;

            return UserID == u.UserID;
        }

        /// <summary>
        /// Overriden GetHashCode method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => UserID.GetHashCode();

        /// <summary>
        /// Compares users to eachother by user id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(User other) => UserID.CompareTo(other.UserID);

        #endregion

        #region Private Methods

        /// <summary>
        /// Divides email into local-part and domain, and calls <see cref="IsLocalPartValid(string)"/> and <see cref="IsDomainValid(string)"/> for validation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsEmailValid(string email)
        {
            string[] emailSplit = email.Split('@');

            if (emailSplit.Length != 2)
                return false;

            return IsLocalPartValid(emailSplit[0]) && IsDomainValid(emailSplit[1]);
        }

        /// <summary>
        /// Validates the local part of the email and returns false if it is not lower case, 
        /// upper case and a digit and the special characters of '.', '_' and '-'.
        /// </summary>
        /// <param name="localPart"></param>
        /// <returns></returns>
        private bool IsLocalPartValid(string localPart)
        {
            return localPart.All(c => char.IsLetterOrDigit(c) || IsValidCharacter(c, '.', '_', '-'));
        }

        /// <summary>
        /// Validates the local part of the email and returns false if it is not lower case,
        /// upper case and a digit and the special characters of '.', '_' and '-'.
        /// The first and final character is ensured not to be either '.' or '-'
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private bool IsDomainValid(string domain)
        {
            return domain.All(c => char.IsLetterOrDigit(c) || IsValidCharacter(c, '.', '-'))
                   && !(domain.First().Equals('.') || domain.First().Equals('-')) 
                   && !(domain.Last().Equals('.') || domain.Last().Equals('-'))
                   && domain.Contains('.');
        }

        /// <summary>
        /// Method that valuates a given character upon different parameters of characters
        /// </summary>
        /// <param name="character"></param>
        /// <param name="charArray"></param>
        /// <returns></returns>
        private bool IsValidCharacter(char character, params char[] charArray) => charArray.Any(c => c == character);

        #endregion
    }
}

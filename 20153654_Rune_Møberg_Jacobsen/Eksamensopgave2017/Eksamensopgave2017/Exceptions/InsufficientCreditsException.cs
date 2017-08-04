using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class InsufficientCreditsException  : Exception
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InsufficientCreditsException() : base() { }

        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="s"></param>
        public InsufficientCreditsException(string s) : base(s) { }

        /// <summary>
        /// Contructor with message and inner exception
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ex"></param>
        public InsufficientCreditsException(string s, Exception ex) : base(s, ex) { }

        /// <summary>
        /// Constructor taking a user and a product
        /// </summary>
        /// <param name="user"></param>
        /// <param name="product"></param>
        public InsufficientCreditsException(User user, Product product) : base()
        {
            TransactionUser = user;
            ProductToBuy = product;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The user responsible for the exception
        /// </summary>
        public User TransactionUser { get; set; }
        
        /// <summary>
        /// The product the user wants to buy
        /// </summary>
        public Product ProductToBuy { get; set; }

        #endregion        
    }
}
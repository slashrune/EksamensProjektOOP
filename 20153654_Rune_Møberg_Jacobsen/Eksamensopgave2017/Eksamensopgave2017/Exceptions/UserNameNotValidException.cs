using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class UserNameNotValidException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserNameNotValidException() : base() { }

        /// <summary>
        /// Constructor taking a message
        /// </summary>
        /// <param name="s"></param>
        public UserNameNotValidException(string s) : base(s) { }

        /// <summary>
        /// Constructor taking a message and an inner exception
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ex"></param>
        public UserNameNotValidException(string s, Exception ex) : base(s, ex) { }

        #endregion
    }
}

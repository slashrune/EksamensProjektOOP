using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class UserNotFoundException  : Exception
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserNotFoundException() : base() { }

        /// <summary>
        /// Constructor taking a message
        /// </summary>
        /// <param name="s"></param>
        public UserNotFoundException(string s) : base(s) { }

        /// <summary>
        /// Constructor taking a message and an inner exception
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ex"></param>
        public UserNotFoundException(string s, Exception ex) : base(s, ex) { }

        #endregion
    }
}
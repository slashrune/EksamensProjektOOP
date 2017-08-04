using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class EmailNotValidException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmailNotValidException() : base() { }

        /// <summary>
        /// Constructor taking a message
        /// </summary>
        /// <param name="s"></param>
        public EmailNotValidException(string s) : base(s) { }

        /// <summary>
        /// Constructor taking a message and an inner exception
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ex"></param>
        public EmailNotValidException(string s, Exception ex) : base(s, ex) { }

        #endregion
    }
}

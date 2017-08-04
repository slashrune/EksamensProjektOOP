using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eksamensopgave2017
{
    public class FileHandler
    {
        #region Private Members

        /// <summary>
        /// Path for the file
        /// </summary>
        private string _filePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="filePath"></param>
        public FileHandler(string filePath)
        {
            _filePath = filePath;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of lines in a file
        /// </summary>
        public int LinesInFile => File.ReadAllLines(_filePath).Length;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns all lines of a file with each cell of the csv in an string[] Enumerable
        /// </summary>
        /// <param name="splitter"></param>
        /// <returns></returns>
        public IEnumerable<string[]> GetCellsInLinesOfFile(char splitter)
        {
            return ReadLinesFromFiles().Select(c => c.Split(splitter));
        }

        /// <summary>
        /// Append a string to the end of a text file
        /// </summary>
        /// <param name="s"></param>
        public void WriteStringToFile(string s)

        {
            File.AppendAllText(_filePath, s + Environment.NewLine);
        }

        /// <summary>
        /// Returns an Enumerable string array where each array have an instance of a given string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        public IEnumerable<string[]> GetLinesContainingString(string s, char splitter)
        {
            return GetCellsInLinesOfFile(splitter).Where(l => ContainStringInArray(s, l));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reads all line from a file into an string Enumerable
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> ReadLinesFromFiles()
        {
            return File.ReadAllLines(_filePath);
        }

        private bool ContainStringInArray(string str, string[] sArray)
        {

            return sArray.Any(s => s.Contains(str));
        }
        
        #endregion
    }
}

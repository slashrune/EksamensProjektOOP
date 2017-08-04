using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave2017
{
    public class StregsystemController
    {
        #region Private Members

        /// <summary>
        /// Reference to the user interface
        /// </summary>
        private IStregsystemUI _ui;
        /// <summary>
        /// Reference to the stregsystem
        /// </summary>
        private IStregsystem _s;
        /// <summary>
        /// Dictionary for the admin commands. It is populated in <see cref="PopulateAdminCommands"/>
        /// </summary>
        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();

        #endregion 

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="stregsystem"></param>
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _s = stregsystem;
            ui.CommandEntered += ParseCommand;
            PopulateAdminCommands();
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Command that runs when the event is raised from stregsystemCLI
        /// It splits the command and calls either user commands or admin commands.
        /// Exceptions are handled at the buttom
        /// </summary>
        /// <param name="command"></param>
        private void ParseCommand(string command)
        {
            string[] commandArray = command.Split(' ');

            if (string.IsNullOrEmpty(command))
                return;
            else if (commandArray.Length > 3)
            {
                _ui.DisplayTooManyArgumentsError(command);
                return;
            }

            try
            {
                if (command[0].Equals(':'))
                    ParseAdminCommand(commandArray);
                else
                    ParseUserCommand(commandArray);
            }
            catch (InsufficientCreditsException e)
            {
                _ui.DisplayInsufficientCash(e.TransactionUser, e.ProductToBuy);
            }
            catch (UserNotFoundException)
            {
                _ui.DisplayUserNotFound(commandArray.Length == 1 ? commandArray[0] : commandArray[1]);
            }
            catch (ProductNotFoundException)
            {
                _ui.DisplayProductNotFound(commandArray.Length == 2 ? commandArray[1] : commandArray[2]);
            }
            catch (KeyNotFoundException)
            {
                _ui.DisplayAdminCommandNotFoundMessage(commandArray[0]);
            }
            catch (FormatException)
            {
                _ui.DisplayGeneralError($"{command} must consist of either a productid or amount of buys");
            }
            catch (OverflowException)
            {
                _ui.DisplayGeneralError($"The numbers in: {command} must not be that high!");
            }

        }

        /// <summary>
        /// Parses a string array of admin commands into dictionary <see cref="_adminCommands"/> 
        /// </summary>
        /// <param name="commandArray"></param>
        private void ParseAdminCommand(string[] commandArray)
        {
            _adminCommands[commandArray[0]](commandArray.Skip(1).ToArray());
        }
        
        /// <summary>
        /// Handles user commands
        /// </summary>
        /// <param name="commandArray"></param>
        private void ParseUserCommand(string[] commandArray)
        {
            switch (commandArray.Length)
            {
                case 1:
                    _ui.DisplayUserInfo(_s.GetUserByUsername(commandArray[0]));
                    break;
                case 2:
                    _ui.DisplayUserBuysProduct(
                        _s.BuyProduct(_s
                            .GetUserByUsername(commandArray[0]), _s
                                .GetProductByID(int.Parse(commandArray[1]))));
                    break;
                case 3:
                    _ui.DisplayUserBuysProduct(int.Parse(commandArray[1]), _s.BuyProduct(
                            _s.GetUserByUsername(commandArray[0]),
                                _s.GetProductByID(int.Parse(commandArray[2]))));

                    int numberOfProducts = int.Parse(commandArray[1]) - 1;

                    for (int i = 1; i < numberOfProducts; i++)
                    {
                        _s.BuyProduct(
                            _s.GetUserByUsername(commandArray[0]),
                                _s.GetProductByID(int.Parse(commandArray[2])));
                    }
                    break;
            }
        }

        /// <summary>
        /// Method to populate the dictionary with admin commands
        /// </summary>
        private void PopulateAdminCommands()
        {
            _adminCommands.Add(":q", c => _ui.Close());
            _adminCommands.Add(":quit", c => _ui.Close());
            _adminCommands.Add(":activate", c => _s.GetProductByID(int.Parse(c[0])).Active = true);
            _adminCommands.Add(":deactivate", c => _s.GetProductByID(int.Parse(c[0])).Active = false);
            _adminCommands.Add(":crediton", c => _s.GetProductByID(int.Parse(c[0])).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", c => _s.GetProductByID(int.Parse(c[0])).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", c => _s.AddCreditsToAccount(_s.GetUserByUsername(c[0]), int.Parse(c[1])));
        }
    }

    #endregion
}                                                                                   
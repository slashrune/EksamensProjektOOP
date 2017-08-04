//20153654_Rune_Møberg_Jacobsen

namespace Eksamensopgave2017
{
    class Program
    {
        static void Main(string[] args)
        {
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);

            ui.Start();
        }
    }
}

using ConsoleTableExt;
using System.Collections.Generic;

namespace cSharpAcademy_Flashcards
{
    internal class Helpers
    {
        internal static void DisplayList<T>(List<T> list) where T : class
        {
            ConsoleTableBuilder
                .From(list)
                .ExportAndWriteLine();

            Console.WriteLine("Type anything to continue");
            Console.ReadLine();
        }
    }
}

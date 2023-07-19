using cSharpAcademy_Flashcards.Controllers;


namespace cSharpAcademy_Flashcards
{
    internal class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();

            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("Hello, what would you like to do?");
                Console.WriteLine("Type 0 to close app");
                Console.WriteLine("Type 1 to Add new stack");
                Console.WriteLine("Type 2 to Delete the stack");
                Console.WriteLine("Type 3 to Add new flashcard to the stack");
                Console.WriteLine("Type 4 to Delete flashcard to the stack");
                Console.WriteLine("Type 5 to see all flashcard from stack");
                Console.WriteLine("Type 6 to start study session");
                Console.WriteLine("Type 7 to display session history");

                string input = Validation.Integer(Console.ReadLine());

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Goodbye");
                        closeApp = true;
                        break;
                    case "1":
                        Stack.Add();
                        break;
                    case "2":
                        Stack.Delete();
                        break;
                    case "3":
                        Flashcard.Add();
                        break;
                    case "4":
                        Flashcard.Delete();
                        break;
                    case "5":
                        Stack.ShowStackItems();
                        break;
                    case "6":
                        StudySession.Start();
                        break;
                    case "7":
                        StudySession.DisplayScore();
                        break;
                    default:
                        Console.WriteLine("There is no such an option, please give correct number");
                        break;
                }
            }
        }
    }
}

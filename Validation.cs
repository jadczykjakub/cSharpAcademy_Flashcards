namespace cSharpAcademy_Flashcards
{
    internal class Validation
    {
        internal static string Integer(string userInput)
        {
            while (string.IsNullOrEmpty(userInput) || !Int32.TryParse(userInput, out _))
            {
                Console.WriteLine("Your answer needs to be an integer. Try again");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
    }
}

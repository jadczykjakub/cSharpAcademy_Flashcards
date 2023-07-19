using cSharpAcademy_Flashcards.DTOs;
using System.Configuration;
using System.Data.SqlClient;

namespace cSharpAcademy_Flashcards.Controllers
{
    internal class Flashcard
    {
        internal static void Add()
        {
            StackDto stack = Stack.SelectExisitngStack();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
    
                connection.Open();

                Console.WriteLine("Give name of stack you want to choose");
                string stackQuery = $"SELECT StackId FROM stack WHERE StackName = '{stack.StackName}';";
                SqlCommand stackCommand = new SqlCommand(stackQuery, connection);
                object stackIdObj = stackCommand.ExecuteScalar();
                Console.WriteLine("Write Flashcard question:");
                string flashcardQuestion = Console.ReadLine();
                       
                Console.WriteLine("Write Flashcard answer");
                string flashcardAnswer = Console.ReadLine();
                string flashcardQuery = $"INSERT INTO Flashcard (FlashcardQuestion, FlashcardAnswer, StackId) VALUES ('{flashcardQuestion}', '{flashcardAnswer}', {stackIdObj});";
                        
                SqlCommand flashcardCommand = new SqlCommand(flashcardQuery, connection);
                int rowsAffected = flashcardCommand.ExecuteNonQuery();
                        
                Console.WriteLine("Flashcard added successfully");

                connection.Close();
            }
        }

        internal static void Delete()
        {
            StackDto stack = Stack.SelectExisitngStack();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                connection.Open();

                Console.WriteLine("Give name of stack you want to choose");
                string stackQuery = $"SELECT StackId FROM stack WHERE StackName = '{stack.StackName}';";
                SqlCommand stackCommand = new SqlCommand(stackQuery, connection);
                object stackIdObj = stackCommand.ExecuteScalar();

                Console.WriteLine("Give me id of the flashcard you want to delete");

                string flashcardId = Validation.Integer(Console.ReadLine());

                string flashcardQuery = $"delete from flashcard where FlashcardId = '{flashcardId}'";

                SqlCommand flashcardCommand = new SqlCommand(flashcardQuery, connection);
                int rowsAffected = flashcardCommand.ExecuteNonQuery();

                Console.WriteLine("Flashcard deleted successfully");

                connection.Close();
            }
        }
    }
}

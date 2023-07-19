using cSharpAcademy_Flashcards.DTOs;
using System.Configuration;
using System.Data.SqlClient;

namespace cSharpAcademy_Flashcards.Controllers
{
    internal class Stack
    {
        internal static void Add()
        {
            Console.WriteLine("Give name of new stack");
            string stackName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                try
                {
                    connection.Open();
                    string query = $"insert into stack (stackName) values ('{stackName}');";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Stack inserted successfully");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while executing SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                connection.Close();
            }
        }

        internal static void Delete()
        {
            Console.WriteLine("Give name of the stack you want to delete");
            string stackName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                try
                {
                    connection.Open();
                    string query = $"delete from stack where stackName = '{stackName}';";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Stack deleted successfully");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occurred while executing SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                connection.Close();
            }
        }

        internal static void ShowStackItems()
        {
            StackDto stack = Stack.SelectExisitngStack();
            List<FlashcardDto> flascardList = GetFlashcardsByStackName(stack.StackName);

            Helpers.DisplayList(flascardList);
        }

        internal static List<FlashcardDto> GetFlashcardsByStackName(string existingStack)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                connection.Open();

                List<FlashcardDto> flascardList = new List<FlashcardDto>();

                string stackQuery = $"SELECT StackId FROM stack WHERE StackName = '{existingStack}';";
                SqlCommand stackCommand = new SqlCommand(stackQuery, connection);
                object stackIdObj = stackCommand.ExecuteScalar();

                string query = $"SELECT s.StackId, s.StackName, f.FlashcardId, f.FlashcardQuestion, f.FlashcardAnswer FROM Stack s JOIN Flashcard f ON s.StackId = f.StackId WHERE stackName = '{existingStack}';";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    flascardList.Add(new FlashcardDto { FlashcardAnswer = (string)reader["FlashcardAnswer"], FlashcardQuestion = (string)reader["FlashcardQuestion"] });
                }

                connection.Close();

                return flascardList;
            }
        }

        internal static StackDto SelectExisitngStack()
        {
            string stackName = "";
            StackDto stackInfo = new StackDto();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                bool validName = false;

                connection.Open();

                while (!validName)
                {
                    Console.WriteLine("Give name of stack you want to choose");
                    string stackNameInput = Console.ReadLine();
                    string stackQuery = $"SELECT StackId FROM stack WHERE StackName = '{stackNameInput}';";
                    SqlCommand stackCommand = new SqlCommand(stackQuery, connection);
                    object stackIdObj = stackCommand.ExecuteScalar();

                    if (stackIdObj != null && stackIdObj != DBNull.Value)
                    {
                        Console.WriteLine($"The name '{stackNameInput}' exists in the table.");
                        stackInfo.StackName = stackNameInput;
                        stackInfo.StackId = Convert.ToInt32(stackIdObj);
                        validName = true;
                    }
                    else
                    {
                        Console.WriteLine($"The name '{stackNameInput}' does not exist in the table. Please try again.");
                        continue;
                    }
                }
                connection.Close();
            }
            return stackInfo;
        }
    }
}

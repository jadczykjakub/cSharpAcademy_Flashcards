using cSharpAcademy_Flashcards.DTOs;
using System.Configuration;
using System.Data.SqlClient;

namespace cSharpAcademy_Flashcards.Controllers
{
    internal class StudySession
    {
        internal static void Start()
        {
            StackDto stack = Stack.SelectExisitngStack();
            DateTime date = DateTime.Now;
            List<FlashcardDto> flashcardsList = Stack.GetFlashcardsByStackName(stack.StackName);
            int score = 0;

            for (int i = 0; i < flashcardsList.Count; i++)
            {
                FlashcardDto flashcard = flashcardsList[i];
                Console.WriteLine($"Flashcard question: {flashcard.FlashcardQuestion}");
                Console.WriteLine("Please asnwer");
                string userAnswer = Console.ReadLine();

                if(flashcard.FlashcardAnswer == userAnswer)
                {
                    score++;
                }
            }

            Console.WriteLine($"Your result is: {score} ");

            SetScoreAndDate(score, date, stack.StackId);

            Console.ReadLine();
        }

        internal static void SetScoreAndDate(int score, DateTime date, int stackId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                connection.Open();

                string stackQuery = $"INSERT INTO StudySession (StackId, SessionScore, SessionDate) VALUES ('{stackId}', '{score}', '{date}');";
                SqlCommand sqlCommand = new SqlCommand (stackQuery, connection);
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static void DisplayScore()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("dbConnectionString")))
            {
                connection.Open();

                List<StudySessionDto> SessionScore = new List<StudySessionDto>();

                string query = $"SELECT * FROM StudySession;";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SessionScore.Add(new StudySessionDto { SessionDate = (DateTime)reader["SessionDate"], SessionScore = (int)reader["SessionScore"], StackId = (int)reader["StackId"] });
                }

                connection.Close();

                Helpers.DisplayList(SessionScore);
            }
        }
    }
}
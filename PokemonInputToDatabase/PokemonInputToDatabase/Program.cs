using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using PokemonClassLibrary;

namespace PokemonInputToDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string name, description = "";

            int hp, speed, level = 0;

            bool exitMenu = false;

            PokemonTypesEnum type = 0;

            Pokemon pokemon;

            //loop through the pokemon input until user wants to exit
            while (!exitMenu)
            {
                Console.WriteLine("Enter the Pokemon's name: ");
                name = Console.ReadLine();

                Console.WriteLine("\nEnter the Pokemon's description: ");
                description = Console.ReadLine();

                type = (PokemonTypesEnum) GetPokemonTypeInput();
                
                hp = GetNumberInput("\nEnter the Pokemon's hp: ");

                speed = GetNumberInput("\nEnter the Pokemon's speed: ");

                level = GetNumberInput("\nEnter the Pokemon's level: ");

                pokemon = new Pokemon(name, description, hp, speed, level, type);

                Console.WriteLine("\nPOKEMON DATA: "
                    + "\n---------------------");

                Console.WriteLine(pokemon.ToString());

                Console.WriteLine("\n---------------------");

                Console.WriteLine("\nCommitting the pokemon to the database...");

                CommitPokemonToDatabase(pokemon);

                Console.ReadLine();

                Console.Clear();

                exitMenu = ShouldExitMenu();
            }
            
        }

        //takes the message to be displayed and loops through to get a valid integer input
        public static int GetNumberInput(string message)
        {
            string input = "";
            bool validInput = false;

            Console.WriteLine(message);

            while (!validInput)
            {
                input = Console.ReadLine();
                validInput = IsValidIntegerInput(input);

                if (!validInput)
                {
                    Console.WriteLine("\nInvalid input. Enter a valid integer.");
                }
            }

            return int.Parse(input);
        }

        public static int GetPokemonTypeInput()
        {
            int validNumber = 0;
            bool validInput = false;

            Console.WriteLine(
                    "\n(1) = Normal" +
                    "\n(2) = Fire" +
                    "\n(3) = Grass" +
                    "\n(4) = Water" +
                    "\n(5) = Electric" +
                    "\n(6) = Psychic" +
                    "\n(7) = Ice" +
                    "\n(8) = Dragon" +
                    "\n(9) = Dark" +
                    "\n(10) = Fairy" +
                    "\n(11) = Flying" +
                    "\n(12) = Fighting" +
                    "\n(13) = Poison" +
                    "\n(14) = Ground" +
                    "\n(15) = Rock" +
                    "\n(16) = Bug" +
                    "\n(17) = Ghost" +
                    "\n(18) = Steel");

            while (!validInput)
            {
                validNumber = GetNumberInput("\nEnter the Pokemon's type: ");

                //check if the number is valid pokemon type menu input
                if (validNumber > 0 && validNumber < 19)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Enter a number from 1 to 18");
                    validInput = false;
                }
            }

            return validNumber;
        }

        public static bool ShouldExitMenu()
        {
            bool validInput = false;
            bool ret = false;

            Console.WriteLine("\nWould you like to enter another pokemon? Enter 1 for \"Yes\" and 0 for \"No\".\n");

            while (!validInput)
            {
                string input = "";
                int number = 0;

                input = Console.ReadLine();

                validInput = IsValidExitMenuInput(input);

                if (validInput)
                {
                    number = int.Parse(input);

                    if (number == 0)
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Enter 1 for \"Yes\" and 0 for \"No\".");
                    ret = false;
                }
            }
            return ret;
        }

        public static bool IsValidExitMenuInput(string input)
        {
            int validNumber = 0;

            //check if input is a valid integer
            if (!IsValidIntegerInput(input))
            {
                return false;
            }

            validNumber = int.Parse(input);

            //check if the number is valid exit menu input
            if (validNumber == 0 || validNumber == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidIntegerInput(string input)
        {
            int validNumber = 0;
            //attempt to parse the user's input to a number
            try
            {
                validNumber = int.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CommitPokemonToDatabase(Pokemon pokemon)
        {
            try
            {
                //create variable to hold the connection string
                string connectionString = ConfigurationManager.ConnectionStrings["PokemonDatabase"].ConnectionString;

                //create a SQL connection with the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //use a SqlCommand to perform the query
                    using (SqlCommand command = new SqlCommand())
                    {
                        //specify the connection string to the SqlCommand
                        command.Connection = connection;

                        //specify that the command type is of text
                        command.CommandType = CommandType.Text;

                        //set the command's text to the SQL query
                        command.CommandText =
                            "INSERT INTO Pokemon (Name, Description, Level, Hitpoints, Speed, PokemonType) " +
                            "VALUES (@name, @description, @level, @hitpoints, @speed, @pokemontype)";

                        //add the parameters and pass the values for the command's query
                        command.Parameters.AddWithValue("@name", pokemon.Name);
                        command.Parameters.AddWithValue("@description", pokemon.Description);
                        command.Parameters.AddWithValue("@level", pokemon.Level);
                        command.Parameters.AddWithValue("@hitpoints", pokemon.HitPoints);
                        command.Parameters.AddWithValue("@speed", pokemon.Speed);
                        command.Parameters.AddWithValue("@pokemontype", pokemon.PokemonType);
                        
                        //execute the command's query
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n****ERROR****");
                Console.WriteLine("\nSomething went wrong:\n\n" + e.Message);
                Console.ReadLine();
            }
        }

        public static void DataBaseTutorialStuff()
        {
            //this all came from SeeSharpCode's database programming for beginners tutorial
            SqlConnection connection;
            string connectionString = ConfigurationManager.ConnectionStrings["PokemonDatabase"].ConnectionString;

            try
            {
                string query = "SELECT * FROM Pokemon";

                //establish the SqlConnection
                using (connection = new SqlConnection(connectionString))
                //fill a Pokemon table
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable pokemonTable = new DataTable();
                    adapter.Fill(pokemonTable);
                }

                Console.WriteLine("Filled the Pokemon Table");

                query = "SELECT * FROM PokemonMoves";

                //fill a PokemonMove table
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable pokemonMoveTable = new DataTable();
                    adapter.Fill(pokemonMoveTable);
                }

                Console.WriteLine("Filled the Pokemon Moves Table");
                Console.WriteLine("SUCCESS!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong:\n\n" + e.Message);
            }

            Console.ReadLine();
        }
    }
}

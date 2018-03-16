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
            PokemonTypesEnum type = 0;

            Console.WriteLine("Enter the Pokemon's name: ");
            name = Console.ReadLine();

            Console.WriteLine("Enter the Pokemon's description: ");
            description = Console.ReadLine();

            Console.WriteLine("Enter the Pokemon's type: ");
            type = (PokemonTypesEnum) int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Pokemon's hp: ");
            hp = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Pokemon's speed: ");
            speed = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Pokemon's level: ");
            level = int.Parse(Console.ReadLine());

            Console.WriteLine("Committing the pokemon to the database...");

            Console.ReadLine();
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

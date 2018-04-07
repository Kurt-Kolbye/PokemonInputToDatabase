using PokemonClassLibrary.PokemonMoveFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonClassLibrary
{
    public class Pokemon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int HitPoints { get; set; }
        public int Speed { get; set; }
        public int Level { get; set; }
        public PokemonTypesEnum PokemonType { get; set; }

        public Pokemon(string name, string description, int hitPoints, int speed, int level, PokemonTypesEnum pokemonType)
        {
            Name = name;
            Description = description;
            HitPoints = hitPoints;
            Speed = speed;
            Level = level;
            PokemonType = pokemonType;
        }

        public void UseAbility(PokemonMove ability)
        {//TODO: verify if more logic needs to be processed here
            ability.Use();
        }

        public override string ToString()
        {
            return (
                "\nName: " + Name
                + "\nDescription: " + Description
                + "\nPokemon Type: " + PokemonType.ToString()
                + "\nHit Points: " + HitPoints.ToString()
                + "\nSpeed: " + Speed.ToString()
                + "\nLevel: " + Level.ToString());
        }
    }
}

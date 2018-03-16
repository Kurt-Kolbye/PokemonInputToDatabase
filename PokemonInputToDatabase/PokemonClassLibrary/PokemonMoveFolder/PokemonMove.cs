using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonClassLibrary.PokemonMoveFolder
{
    public abstract class PokemonMove
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public PokemonTypesEnum MoveType { get; set; }

        public PokemonMove(string name, string description, PokemonTypesEnum moveType)
        {
            Name = name;
            Description = description;
            MoveType = moveType;
        }

        public abstract void Use();
    }
}
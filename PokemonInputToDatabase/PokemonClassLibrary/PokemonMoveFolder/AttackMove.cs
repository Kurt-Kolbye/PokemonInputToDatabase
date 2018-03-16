using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonClassLibrary.PokemonMoveFolder
{
    public class AttackMove : PokemonMove
    {
        private int Power { get; set; }

        private int Accuracy { get; set; }

        public AttackMove(string name, string description, PokemonTypesEnum moveType, int power, int accuracy) 
            : base(name, description, moveType)
        {
            Power = power;
            Accuracy = accuracy;
        }


        public override void Use()
        {
            //TODO: implement the Attack Move's "use" method
            throw new NotImplementedException();
        }
    }
}

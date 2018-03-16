using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonClassLibrary.PokemonMoveFolder
{
    public class OtherMove : PokemonMove
    {//TODO: create extra fields for "other moves"
        public OtherMove(string name, string description, PokemonTypesEnum moveType) : base(name, description, moveType)
        {
            //TODO: implement other factors for "other moves"
        }
        public override void Use()
        {
            //TODO: implement the Other Ability's "use" method
            throw new NotImplementedException();
        }
    }
}

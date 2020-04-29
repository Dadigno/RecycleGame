using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gioco_generico
{
    public class AiMngnt
    {
        Character _char;
        Character.walk state;
        List<Character> _chars;
        public AiMngnt(Character _char)
        {
            this._char = _char;
        }

        
        public void run(Random rnd)
        {

            if (_char.collide)
            {
                int b = rnd.Next(0, 4);
                Character.walk ca = (Character.walk)b;
                _char.setAction(ca);
            }
        }
       
    }
}

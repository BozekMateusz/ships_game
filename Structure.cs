using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki
{
    class Structure
    {
        public int type;
        public Field[] fields;
        public bool Alive;


        public Structure(int type, Field[] fields)
        {
            this.type = type;
            this.fields = fields;
            this.Alive = true;
        }
    }
}

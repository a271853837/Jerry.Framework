using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    public class PersonYield
    {
        public string Name;
        public void SayHi()
        {
            Console.WriteLine("Hello: {0}", this.Name);
        }
    }

    public class PersonList
    {
        PersonYield[] pers = new PersonYield[4];
        public PersonList()
        {
            pers[0] = new PersonYield() { Name = "1" };
            pers[1] = new PersonYield() { Name = "2" };
            pers[2] = new PersonYield() { Name = "3" };
            pers[3] = new PersonYield() { Name = "4" };

        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in pers)
            {
                yield return item;
            }
        }

    }
}

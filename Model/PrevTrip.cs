using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class PrevTrip
    {
        private int id;
        private int user_id;
        private List<int> roads = new List<int>();
        private int length;
        private DateTime created_at;
        private int start;
        private int end;

        public int ID { get => this.id; }
        public int USER_ID { get => this.user_id; }
        public List<int> ROADS { get => this.roads; }
        public int LENGTH { get => this.length; }
        public DateTime CREATED_AT { get => this.created_at; }
        public int START { get => this.start; }
        public int END { get => this.end; }

        public PrevTrip(int Id, int User_Id, string Roads, 
            int Length, DateTime Created_at, int Start, int End)
        {
            this.id = Id;
            this.user_id = User_Id;

            string[] s = Roads.Split(' ');
            foreach (string x in s)
                if(x != "")
                    roads.Add(int.Parse(x));

            this.length = Length;
            this.created_at = Created_at;
            this.start = Start;
            this.end = End;
        }

        public override string ToString()
        {
            return this.id.ToString();
        }
    }
}

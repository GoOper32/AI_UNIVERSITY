using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
namespace AI_1_Csh
{
    public enum ColorElement
    {
        Blue, Green, Purle, Scarlet, Orange
    }
    public class GameCondition
    {
        public GameCondition parentCondition = null;

        private string passwordHash;
        public int posClick;
        public ColorElement[] arr { get; } = new ColorElement[] {
                ColorElement.Blue, ColorElement.Blue , ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue,
            ColorElement.Blue, ColorElement.Blue , ColorElement.Orange, ColorElement.Blue, ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue,
                            ColorElement.Blue, ColorElement.Blue , ColorElement.Blue
            };
        public string hash { get { return arrString; } }

        public string arrString
        {
            get
            {
                string tmp = "";
                int pos = 1;
                foreach (var item in arr)
                    tmp += item.ToString().Substring(0,1);
                return tmp;
            }
        }
        public GameCondition()
        {
            //hash = BCrypt.Net.BCrypt.HashString(arrString);
        }
        public GameCondition(ColorElement[] arr,ref GameCondition parent)
        {
            this.parentCondition = parent;
            if (arr.Count() > 18)
                for(int i = 0; i < 19; i++)
                    this.arr[i] = arr[i];
            parentCondition = parent;
            //hash = BCrypt.Net.BCrypt.HashString(arrString);
        }

        public bool Equals(GameCondition newCondition)
        {
            return this.arrString == newCondition.arrString;//BCrypt.Net.BCrypt.Verify(arrString, newCondition.hash);
        }

        private string[] sequence = {
                                        "50","51","55","59","58","53", "61","62","66","610","69","64",
            "93","94","99","913","912","97",   "104","105","1010","1014","1013","108","115",   "116","1111","1115","1114","119",
                                    "148","149","1414","1417","1416","1412",   "159","1510","1515","1518","1517","1513",

        };

        public GameCondition nextCondition(int posEl)
        {
            string pos = posEl.ToString();
            List<int> seq = sequence.Where(x => x.Substring(0, pos.Length) == pos).Select(x=>Convert.ToInt32(x.Substring(pos.Length,x.Length- pos.Length))).ToList();
            GameCondition parrentnul = null;
            GameCondition ans = new GameCondition(arr, ref parrentnul);
            ColorElement tmp = ans.arr[seq[0]];
            ColorElement tmp2= tmp;
            for (int i = 1; i < seq.Count; i++)
            {
                tmp2 = ans.arr[seq[i]];
                ans.arr[seq[i]] = tmp;
                tmp = tmp2;
            }
            ans.arr[seq[0]] = tmp;
            ans.posClick = posEl;
            return ans;
        }

        public List<GameCondition> generateListActions()
        {
            List<GameCondition> ans = new List<GameCondition>();
                            ans.Add(this.nextCondition(5)); ans.Add(this.nextCondition(6));
            ans.Add(this.nextCondition(9)); ans.Add(this.nextCondition(10)); ans.Add(this.nextCondition(11));
                            ans.Add(this.nextCondition(14)); ans.Add(this.nextCondition(15));
            foreach (var item in ans)
            {
                if(!item.Equals(this))
                    item.parentCondition = this;
            }
            return ans;
        }

        public GameCondition Clone()
        {
            GameCondition parrentnul = null;
            return  new GameCondition(this.arr,ref parrentnul);
        }
        public void Shuffle()
        {
            Random random = new Random();
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = arr[j];
                arr[j] = arr[i];
                arr[i] = temp;
            }
        }

    }
}

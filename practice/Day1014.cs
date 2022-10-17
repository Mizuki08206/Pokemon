using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    internal class Day1014
    {
        public static void Main(string[] args)
        {
            //a a = new a();
            //a.Log();

            inspection tmp=new inspection();
            tmp.show();
        }
    }


    //仕様書が読み解けないため、やりたくない。。。
    public class Game
    {
        int total = 0;//3回の釣りの合計得点
        int kaisu = 3;//釣りを行う回数
        public Game(int kaisu)
        {
            this.kaisu = kaisu;
        }
        public void Play()//ゲームの本体
        {
            this.Start();





            this.Finish();
        }
        private void Start()
        {
            this.total = 0;
            Console.WriteLine("ゲーム開始");
        }
        private void Finish()
        {
            Console.WriteLine("ゲーム終了");
            Console.WriteLine("得点は{0}",this.total);
        }
    }

    public class Place
    {
        public List<Fish> pbox = new List<Fish>();
        public Place()//constructor
        {

        }
        public Fish GetFish(int index)
        {
            return pbox[index];
        }
        public int NumOfPlaces()
        {
            return pbox.Count;
        }
    }

    public class FishList
    {
        private Random rnd = new Random();
        private List<Fish> box = new List<Fish>();
        public FishList()
        {

        }
        public Fish RandomFish()
        {
            return box[rnd.Next(box.Count)];
        }
    }

    public class Fish
    {
        private string name;//fish name
        private int Point { get; set; }//fish point
        public Fish(string name, int point)
        {
            this.name = name;
            this.Point = point;
        }
        public string toString()
        {

            return "tmp";//今後変更予定
        }
    }

    public class inspection//検証用
    {
        public static void q()//データの重複について
        {
            HashSet<string> a = new HashSet<string>();
            a.Add("abc");
            a.Add("Abc");//string型ならば大文字小文字の区別あり
            a.Add("1");
            a.Add("01");
            Console.WriteLine("---string---");
            foreach (string s in a)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("---int---");
            HashSet<int> b = new HashSet<int>();
            b.Add(1);
            b.Add(01);//これは重複し、追加されない
            foreach (int i in b)
            {
                Console.WriteLine(i);
            }
        }
        private int num = 0;
        public void show()//フィールドのインクリメント
        {
            Console.WriteLine(this.num++);
            Console.WriteLine(this.num++);

        }
    }
    public class a//集合研修
    {
        private string path = @"C:\Users\ia\Desktop\tmp\access.log.txt";
        //'@'は逐語的文字列と呼ばれ、エスケープシーケンスを無視するため、'\'はひとつでよいはず
        private string access;
        private int num = 0;
        public a()
        {
            //Console.WriteLine(this.path);
            Console.WriteLine("最終アクセスを記録");
        }
        public void Log()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                if (!sr.EndOfStream)
                {
                    //this.access = sr.ReadLine();
                    //this.num = int.Parse(sr.ReadLine());

                    //前回アクセスを消さないように改修
                    while (!sr.EndOfStream)
                    {
                        this.access = sr.ReadLine();
                        this.num = int.Parse(sr.ReadLine());
                    }

                    Console.WriteLine("前回のアクセスは\n{0}\n{1}回目でした", this.access, this.num);
                }
                else
                {
                    Console.WriteLine("最初のアクセスです。");
                }
            }

            //前回アクセスを消さないように改修、false（前回消して上書き）→true（追記）
            using (StreamWriter sw = new StreamWriter(this.path, true, Encoding.UTF8))
            {
                DateTime t = DateTime.Now;
                string result = t.ToString("yyyy年MM月dd日 HH時mm分ss秒:");
                //Console.WriteLine(result);
                sw.WriteLine(result);
                //Console.WriteLine(this.num+1);
                sw.WriteLine(this.num+1);//writeしたあとにインクリメントしてしまうため、this.num++はダメ
                //++this.numはおそらく大丈夫
            }
        }
    }
}

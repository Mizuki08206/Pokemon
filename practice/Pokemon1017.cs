using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practice
{
    public class Pokemon1017
    {
        public static void Main(string[] args)
        {
            string dirpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//実行ファイルのpathを取得
            Console.WriteLine(dirpath);
            Console.WriteLine(dirpath);
            Console.WriteLine(dirpath);
            string filepath = dirpath.Replace(@"bin\Debug\net6.0", "PokemonData.txt");//置換
            Console.WriteLine(filepath);
            Console.WriteLine(filepath);
            Console.WriteLine(filepath);
            using (StreamWriter sw=new StreamWriter(filepath,false,Encoding.UTF8))
            {
                sw.WriteLine("abcde");
            };

            string path = "tmp.txt";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine("abcde");
            };



            //Random rand = new Random();
            ////Monster（名前,HP,攻撃力,防御力,スピード,属性）
            //List<Monster> monsters = new List<Monster>();
            //monsters.Add(new Monster("ヒトカゲ", 100, 30, 10, rand.Next(20, 30), MonsterType.Fire));
            //monsters.Add(new Monster("ゼニガメ", 100, 30, 10, rand.Next(20, 30), MonsterType.Water));
            //monsters.Add(new Monster("フシギダネ", 100, 30, 10, rand.Next(20, 30), MonsterType.Grass));

            //Human main = new Human(monsters);
            //Human rival = new Human(monsters, "ライバル");
            //Console.WriteLine("{0} が {1} にバトルを仕掛けてきた", rival.Name, main.Name);

            ////一旦全部出す
            //foreach (Monster monster in monsters)
            //{
            //    Console.WriteLine("\n{0}のスキルセット", monster.Name);
            //    foreach (Skill skill in monster.SkillSet)
            //    {
            //        Console.WriteLine("\n・{0}\n{1}\t{2}\t{3}\t{4}", skill.SkillName, skill.Type, skill.Attack, skill.Heal, skill.Num);
            //    }
            //}
            //Console.WriteLine("---終了---");

            ////ここからバトルロジック
            //bool win = false;
            //bool lose = false;
            //bool turn = false;
            //Monster tmp;

            ////スタメンを決める
            //foreach (Monster monster in main.Monsters)
            //{
            //    Console.WriteLine("・{0}", monster.Name);
            //}
            //for (int i = 0; i < main.Monsters.Count; i++)
            //{
            //    Console.WriteLine("【{0}】 {1}", i + 1, main.Monsters[i].Name);
            //}
            //while (true)
            //{
            //    Console.WriteLine("どれを出す？");
            //    string input = Console.ReadLine();
            //    if (int.TryParse(input, out int num))
            //    {
            //        if (0 < num && num < 4)
            //        {
            //            //入力をもらう途中
            //        }
            //    }
            //}

            ////先行を決める


            ////ここからバトルロジック（マジ）
            //while (true)
            //{





            //    if (main.RemainCheck())
            //    {
            //        win = true;
            //    }
            //    if (rival.RemainCheck())
            //    {
            //        lose = true;
            //    }
            //}
            //if (win)
            //{
            //    Console.WriteLine("{0}は{1}の戦いに勝った。", main.Name, rival.Name);
            //}
            //else if (lose)
            //{
            //    Console.WriteLine("{0}は{1}の戦いに負けた", main.Name, rival.Name);
            //    Console.WriteLine("{0}は目の前が真っ暗になった。", main.Name);
            //}


            //これは検証
            //Console.WriteLine(MonsterType.Fire);
            //int num = -1;
            //while (true)
            //{
            //    Console.WriteLine("入力町");
            //    string input = Console.ReadLine();
            //    if (int.TryParse(input, out num))
            //    {
            //        break;
            //    }
            //}
            //for (int i = 0; i < 4; i++)
            //{
            //    if (i==(int)SkillType.Fire)//列挙型の数値比較
            //    {
            //        Console.WriteLine(i);
            //        Console.WriteLine(SkillType.Fire);
            //    }
            //    else if(i == (int)SkillType.Water)
            //    {
            //        Console.WriteLine(i);
            //        Console.WriteLine(SkillType.Water);
            //    }
            //    else if (i == (int)SkillType.Grass)
            //    {
            //        Console.WriteLine(i);
            //        Console.WriteLine(SkillType.Grass);
            //    }
            //}
        }
    }
    public class Human
    {
        public string Name { set; get; } = "mizuki";

        public List<Monster> Monsters { get; set; }
        public Human(List<Monster> monsters)
        {
            this.Monsters = monsters;
        }
        public Human(List<Monster> monsters, string name)
        {
            this.Monsters = monsters;
            this.Name = name;
        }
        public bool RemainCheck()
        {
            bool ans = true;
            foreach (Monster monster in Monsters)
            {
                if (!monster.isDead)
                {
                    ans = false;
                }
            }
            return ans;//後で変更
        }

    }
    public class Monster
    {
        public string Name { get; set; }//monster's name
        public int Hp { set; get; }//monster's hp
        public int Attack { set; get; }//monster's attack
        public Skill[] SkillSet { get; set; } = new Skill[4];
        public int Defence { set; get; }//monster's defence
        public int Speed { set; get; }//monster's Speed,dicide which is presedence
        public MonsterType Type { get; set; }//monster's type
        public bool isDead { set; get; } = false;//monster died?

        //public int Level { set; get; } = 5;//monster's level
        //public int ExperienceNow { set; get; }//monster's experience
        //public int Experiencelimit { set; get; }//monster's experience limit

        public Monster(string name, int hp, int attack, int defence, int speed, MonsterType type)
        {
            this.Name = name;
            this.Hp = hp;
            this.Type = type;
            this.Defence = defence;
            this.Attack = attack;
            this.Speed = speed;
            //this.Level = 5;
            //this.ExperienceNow = 100;
            //this.Experiencelimit = this.Level * 25;
            string[] fire = { "パンチ", "ニトロチャージ", "かえんほうしゃ", "かいふく" };
            string[] water = { "パンチ", "なみのり", "ハイドロポンプ", "かいふく" };
            string[] grass = { "パンチ", "マジカルリーフ", "ソーラービーム", "かいふく" };

            //Random rand=new Random();
            //int[] att = { rand.Next(5, 10), rand.Next(15, 20), rand.Next(20, 30), 0 };//3体の攻撃力をバラバラにさせたいが、

            SkillSet[0] = new Skill("パンチ", SkillType.Normal, new Random().Next(5, 10), 0);
            for (int i = 1; i < this.SkillSet.Length - 1; i++)
            {
                Random rand = new Random();
                Thread.Sleep(100);
                if (i == 1)
                {
                    if (this.Type == MonsterType.Fire)
                    {
                        SkillSet[i] = new Skill(fire[i], SkillType.Fire, rand.Next(15, 20), 0);
                    }
                    else if (this.Type == MonsterType.Water)
                    {
                        SkillSet[i] = new Skill(water[i], SkillType.Water, rand.Next(15, 20), 0);
                    }
                    else if (this.Type == MonsterType.Grass)
                    {
                        SkillSet[i] = new Skill(grass[i], SkillType.Grass, rand.Next(15, 20), 0);
                    }
                }
                else
                {
                    if (this.Type == MonsterType.Fire)
                    {
                        SkillSet[i] = new Skill(fire[i], SkillType.Fire, rand.Next(20, 30), 0);
                    }
                    else if (this.Type == MonsterType.Water)
                    {
                        SkillSet[i] = new Skill(water[i], SkillType.Water, rand.Next(20, 30), 0);
                    }
                    else if (this.Type == MonsterType.Grass)
                    {
                        SkillSet[i] = new Skill(grass[i], SkillType.Grass, rand.Next(20, 30), 0);
                    }
                }
            }
            SkillSet[3] = new Skill("かいふく", SkillType.Normal, 0, 20);
        }

        public bool SkillRemain()//return true if you use up all skills
        {
            bool ans = true;
            foreach (var skill in SkillSet)
            {
                if (skill.Num != 0)
                {
                    ans = true;
                }
            }
            return ans;
        }
    }
    public class Skill
    {
        public string SkillName { get; set; }//skill name
        public SkillType Type { set; get; }//skill type
        public int Attack { set; get; }//skill base of attack
        public int Heal { set; get; } = 0;
        public int Num { set; get; } = 3;//how many used this skill 
        public Skill(string name, SkillType type, int attack, int heal)
        {
            this.SkillName = name;
            this.Type = type;
            this.Attack = attack;
            this.Heal = heal;
        }
        public void SkillUse()
        {
            this.Num -= 1;
        }


    }
    public enum MonsterType { Fire = 0, Water = 1, Grass = 2 }//
    public enum SkillType { Fire = 0, Water = 1, Grass = 2, Normal = 3 }
}
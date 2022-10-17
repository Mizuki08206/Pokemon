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
            
            Random rand = new Random();
            //Monster（名前,HP,攻撃力,防御力,スピード,属性）
            List<Monster> monsters = new List<Monster>();
            monsters.Add(new Monster("ヒトカゲ", 100, 30, 10, rand.Next(20, 30), MonsterType.ほのお));
            monsters.Add(new Monster("ゼニガメ", 100, 30, 10, rand.Next(20, 30), MonsterType.みず));
            monsters.Add(new Monster("フシギダネ", 100, 30, 10, rand.Next(20, 30), MonsterType.くさ));

            Human main = new Human(monsters,"mizuki");
            Human rival = new Human(monsters);
            Console.WriteLine("{0} が {1} にバトルを仕掛けてきた", rival.Name, main.Name);

            //一旦全部出す
            foreach (Monster monster in main.Monsters)
            {
                Console.WriteLine("\n{0}\tHp:{1}/{2}\t状態:{3}", monster.Name,monster.RemainHp,monster.MaxHp,monster.isDead);
                foreach (Skill skill in monster.SkillSet)
                {
                    Console.WriteLine("\n・{0}\n{1}\t{2}\t{3}\t{4}", skill.SkillName, skill.Type, skill.Attack, skill.Heal, skill.Num);
                }
            }
            Console.WriteLine("---終了---");

            //ここからバトルロジック
            bool win = false;
            bool lose = false;
            bool turn = false;
            Monster now;//今戦っているモンスター主人公
            Monster rivalNow;//今戦っているモンスターライバル

            //スタメンを決める
            for (int i = 0; i < main.Monsters.Count; i++)//主人公のモンスターリストから
            {
                if (!main.Monsters[i].isDead)
                {
                    Console.WriteLine("【{0}】 {1}", i + 1, main.Monsters[i].Name);
                }
                else
                {
                    Console.WriteLine("【{0}】 {1}(ひんし)", i + 1, main.Monsters[i].Name);
                }
            }
            while (true)
            {
                Console.WriteLine("どれを出す？");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (1 <= num && num <= 3)
                    {
                        if (!main.Monsters[num-1].isDead)
                        {
                            now = main.Monsters[num-1];
                            Console.WriteLine("{0}は{1}をくりだした。", main.Name, now.Name);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("{0}はひんしです", main.Monsters[num-1].Name);
                        }
                    }
                }
                Console.WriteLine("1～{0}で入力してください", main.Monsters.Count);
            }

            //ライバルのスタメン
            rivalNow = rival.Monsters[rand.Next(0,rival.Monsters.Count)];
            Console.WriteLine("{0}は{1}をくりだした",rival.Name,rivalNow.Name);

            //先行を決める
            if (rivalNow.Speed < now.Speed)
            {
                turn = true;//主人公先行
            }
            else if(rivalNow.Speed > now.Speed)
            {
                turn = false;//ライバル先行
            }
            else
            {
                int tmp = rand.Next(0, 2);
                if (tmp == 0)
                {
                    turn = true;//主人公先行
                }else if (tmp == 1)
                {
                    turn = false;//ライバル先行
                }
            }

            //ここからバトルロジック（マジ）
            while (true)
            {
                if (turn)//主人公のターン
                {

                }
                else//ライバルのターン
                {

                }







                if (main.RemainCheck())
                {
                    win = true;
                    break;
                }






                if (rival.RemainCheck())
                {
                    lose = true;
                    break;
                }
            }
            if (win)
            {
                Console.WriteLine("{0}は{1}の戦いに勝った。", main.Name, rival.Name);
            }
            else if (lose)
            {
                Console.WriteLine("{0}は{1}の戦いに負けた", main.Name, rival.Name);
                Console.WriteLine("{0}は目の前が真っ暗になった。", main.Name);
            }



        }
    }
    public class Inspection1
    {
        public static void q()
        {
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

            //ファイルの相対パスの検証
            //string dirpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//実行ファイルのpathを取得
            //Console.WriteLine(dirpath);
            //Console.WriteLine(dirpath);
            //Console.WriteLine(dirpath);
            //string filepath = dirpath.Replace(@"bin\Debug", "PokemonData.txt");//置換
            //Console.WriteLine(filepath);
            //Console.WriteLine(filepath);
            //Console.WriteLine(filepath);
            //using (StreamWriter sw = new StreamWriter(filepath, false, Encoding.UTF8))
            //{
            //    sw.WriteLine("abcde");
            //};
            //上と下やっていることは同じ、プロジェクトのexeファイルが基点の相対パス
            //string path = @"..\..\tmp.txt";
            //using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            //{
            //    sw.WriteLine("abcde");
            //};

            //検証
            //ans:参照型だから、片方で変更してももう片方に反映される
            //now.RemainHp -= 20;
            //Console.WriteLine("aaaaaaaaaaaaa:{0}", now.RemainHp);//出力：80
            //Console.WriteLine("aaaaaaaaaaaaa:{0}", main.Monsters[num].RemainHp);//出力：80
        }
    }
    public class Human
    {
        public string Name { set; get; } = "ライバル";

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
            return ans;//手持ちモンスターが全てひんしならtrue
        }

    }
    public class Monster
    {
        public string Name { get; set; }//monster's name
        public int RemainHp { set; get; }//monster's hp
        public int MaxHp { set; get; }
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
            this.RemainHp = hp;
            this.MaxHp = hp;
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

            SkillSet[0] = new Skill("パンチ", SkillType.ノーマル, new Random().Next(5, 10), 0);
            for (int i = 1; i < this.SkillSet.Length - 1; i++)
            {
                Random rand = new Random();
                Thread.Sleep(100);
                if (i == 1)
                {
                    if (this.Type == MonsterType.ほのお)
                    {
                        SkillSet[i] = new Skill(fire[i], SkillType.ほのお, rand.Next(15, 20), 0);
                    }
                    else if (this.Type == MonsterType.みず)
                    {
                        SkillSet[i] = new Skill(water[i], SkillType.みず, rand.Next(15, 20), 0);
                    }
                    else if (this.Type == MonsterType.くさ)
                    {
                        SkillSet[i] = new Skill(grass[i], SkillType.くさ, rand.Next(15, 20), 0);
                    }
                }
                else
                {
                    if (this.Type == MonsterType.ほのお)
                    {
                        SkillSet[i] = new Skill(fire[i], SkillType.ほのお, rand.Next(20, 30), 0);
                    }
                    else if (this.Type == MonsterType.みず)
                    {
                        SkillSet[i] = new Skill(water[i], SkillType.みず, rand.Next(20, 30), 0);
                    }
                    else if (this.Type == MonsterType.くさ)
                    {
                        SkillSet[i] = new Skill(grass[i], SkillType.くさ, rand.Next(20, 30), 0);
                    }
                }
            }
            SkillSet[3] = new Skill("かいふく", SkillType.ノーマル, 0, 20);
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
    public enum MonsterType { ほのお = 0, みず = 1, くさ = 2 }//
    public enum SkillType { ほのお = 0, みず = 1, くさ= 2, ノーマル = 3 }
}
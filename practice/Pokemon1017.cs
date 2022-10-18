using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practice
{
    public class Pokemon1017
    {
        public static void Main(string[] args)
        {
            /*問題点
             * 1.きゅうしょはやっていない
             */

            Random rand = new Random();
            //Monster（名前,HP,攻撃力,防御力,スピード,属性）
            List<Monster> monsters = new List<Monster>();
            monsters.Add(new Monster("ヒバニー", 200, 30, 10, rand.Next(20, 30), MonsterType.ほのお));
            monsters.Add(new Monster("メッソン", 200, 30, 10, rand.Next(20, 30), MonsterType.みず));
            monsters.Add(new Monster("サルノリ", 200, 30, 10, rand.Next(20, 30), MonsterType.くさ));
            List<Monster> rivalmonsters = new List<Monster>();
            rivalmonsters.Add(new Monster("ポカブ", 200, 30, 10, rand.Next(20, 30), MonsterType.ほのお));
            rivalmonsters.Add(new Monster("ミジュマル", 200, 30, 10, rand.Next(20, 30), MonsterType.みず));
            rivalmonsters.Add(new Monster("ツタージャ", 200, 30, 10, rand.Next(20, 30), MonsterType.くさ));

            Human main = new Human(monsters, "mizuki");
            Human rival = new Human(rivalmonsters);
            Console.WriteLine("{0} が {1} にバトルを仕掛けてきた", rival.Name, main.Name);
            Thread.Sleep(1000);

            //一旦全部出す
            //foreach (Monster monster in main.Monsters)
            //{
            //    Console.WriteLine("\n{0}\tHp:{1}/{2}\t状態:{3}", monster.Name,monster.RemainHp,monster.MaxHp,monster.isDead);
            //    foreach (Skill skill in monster.SkillSet)
            //    {
            //        Console.WriteLine("\n・{0}\n{1}\t{2}\t{3}\t{4}", skill.SkillName, skill.Type, skill.Attack, skill.Heal, skill.Num);
            //    }
            //}
            //Console.WriteLine("---終了---");

            //ここからバトルロジック
            //int rivalMonsterNum = rival.Monsters.Count;//ライバル手持ちのランダム生成用
            bool win = false;
            bool lose = false;
            bool turn = false;
            Monster now;//今戦っているモンスター主人公
            Monster rivalNow;//今戦っているモンスターライバル

            //主人個のスタメンを決める
            now = main.NowSelect();

            //ライバルのスタメン
            rivalNow = rival.Monsters[rand.Next(0, rival.Monsters.Count)];
            Console.WriteLine("\n{0}は{1}をくりだした", rival.Name, rivalNow.Name);

            //先行を決める
            if (rivalNow.Speed < now.Speed)
            {
                turn = true;//主人公先行
            }
            else if (rivalNow.Speed > now.Speed)
            {
                turn = false;//ライバル先行
            }
            else
            {
                int precedence = rand.Next(0, 2);
                if (precedence == 0)
                {
                    turn = true;//主人公先行
                }
                else if (precedence == 1)
                {
                    turn = false;//ライバル先行
                }
            }
            bool a=false;
            //ここからバトルロジック（マジ）
            while (true)
            {
                if (a)
                {
                    //Console.WriteLine("ふたたび先行を決める");
                    //先行を決める
                    if (rivalNow.Speed < now.Speed)
                    {
                        turn = true;//主人公先行
                    }
                    else if (rivalNow.Speed > now.Speed)
                    {
                        turn = false;//ライバル先行
                    }
                    else
                    {
                        int precedence = rand.Next(0, 2);
                        if (precedence == 0)
                        {
                            turn = true;//主人公先行
                        }
                        else if (precedence == 1)
                        {
                            turn = false;//ライバル先行
                        }
                    }
                    a = false;
                }
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("\n{0} HP:{1}\nvs\n{2} HP:{3}\n", now.Name, now.RemainHp, rivalNow.Name, rivalNow.RemainHp);
                
                if (turn)//主人公のターン
                {
                    Console.WriteLine("---{0}のターン---\n",main.Name);
                    Console.WriteLine("{0}の{1}のこうげき", main.Name, now.Name);

                    if (now.SkillRemain())//使えるスキルが残っていた場合
                    {
                        //ワザの入力受付と攻撃も行う
                        int i = 0;
                        int num = 0;

                        while (true)//ワザセレクト
                        {
                            for (i = 1; i <= now.SkillSet.Length; i++)
                            {
                                Console.WriteLine("【{0}】{1}\tpp:{2}回", i, now.SkillSet[i - 1].SkillName, now.SkillSet[i - 1].Num);
                            }
                            Console.WriteLine("【{0}】こうたい", i);
                            Console.WriteLine("どのワザをだす？");
                            string ss = Console.ReadLine();
                            if (int.TryParse(ss, out num))
                            {
                                if (1 <= num && num <= 4)
                                {
                                    if (now.SkillSet[num - 1].Num == 0)
                                    {
                                        Console.WriteLine("ppがない");
                                    }
                                    else
                                    {
                                        now.AttackEnemy(main.Name, rivalNow, num - 1);
                                        break;
                                    }
                                }
                                else if (num == 5)
                                {
                                    now = main.NowSelect();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("1～5で入力してください");
                                }
                            }

                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("---{0}のターン---\n", main.Name);
                            Console.WriteLine("{0}の{1}のこうげき", main.Name, now.Name);
                            Console.WriteLine("\n{0} HP:{1}\nvs\n{2} HP:{3}\n", now.Name, now.RemainHp, rivalNow.Name, rivalNow.RemainHp);
                        }
                    }
                    else//使えるスキルが残っていなかった場合
                    {
                        now.AttackEnemy(rivalNow);
                    }

                    
                    //ライバルのモンスターが倒れたら交代
                    if (rivalNow.isDead)
                    {
                        Console.WriteLine("{1}の{0}はたおれた", rivalNow.Name,rival.Name);

                        //残りの手持ちの生存確認
                        if (rival.RemainCheck())
                        {
                            Console.WriteLine("{0}はもうたたかえるポケモンがいない", rival.Name);
                            win = true;
                            break;
                        }
                        while (true)
                        {
                            rivalNow = rival.Monsters[rand.Next(0, rival.Monsters.Count)];
                            if (!rivalNow.isDead)//ひんしもとってくるが、if文ではじいている
                            {
                                break;
                            }
                        }
                        a = true;
                        Console.WriteLine("\n{0}は{1}をくりだした", rival.Name, rivalNow.Name);
                    }
                    Thread.Sleep(1000);
                    turn = false;
                }
                else//ライバルのターン
                {
                    Console.WriteLine("---{0}のターン---\n", rival.Name);
                    rivalNow.AttackEnemy(rival.Name, now, rand.Next(0, 4));

                    //主人公のモンスターが倒れたら交代
                    if (now.isDead)
                    {
                        Console.WriteLine("{0}はたおれた", now.Name);
                        if (main.RemainCheck())
                        {
                            Console.WriteLine("{0}はもうたたかえるポケモンがいない", main.Name);
                            lose = true;
                            break;
                        }
                        a = true;
                        now = main.NowSelect();
                    }
                    turn = true;
                    Thread.Sleep(3000);
                }
                Console.WriteLine("\n-----turn change-----");
            }
            if (win)
            {
                Console.WriteLine("\n\n{0}は{1}の戦いに勝った。\n", main.Name, rival.Name);
            }
            else if (lose)
            {
                Console.WriteLine("\n\n{0}は{1}の戦いに負けた\n", main.Name, rival.Name);
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
            //string path = @"pokemonData.txt";
            //using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            //{
            //    sw.WriteLine("abcde");
            //};

            //検証
            //ans:参照型だから、片方で変更してももう片方に反映される
            //now.RemainHp -= 20;
            //Console.WriteLine("aaaaaaaaaaaaa:{0}", now.RemainHp);//出力：80
            //Console.WriteLine("aaaaaaaaaaaaa:{0}", main.Monsters[num].RemainHp);//出力：80

            //参照型の検証2
            //List<Aaa> array = new List<Aaa>();
            //array.Add(new Aaa("aaaaa"));
            //array.Add(new Aaa("bbbbb"));
            //array.Add(new Aaa("ccccc"));
            //Aaa now = array[0];
            //now.name = "aaadd";
            //Console.WriteLine("{0} / {1}", now.name, array[0].name);//出力：aaadd / aaadd
            //array.RemoveAt(0);
            //Console.WriteLine("{0} / {1}", now.name, array[0].name);//出力：aaadd / bbbbb
            //結論
            //参照型のコピーは、removeや再代入をしても、ヒープ領域にオブジェクトが残る。
            //使わなくなったオブジェクトはガベージコレクションで削除されるか、明示的にガベージコレクション「GC.Collect();」を呼び出す必要がある。
            //検証のしようがない気がする
        }
    }
    public class Aaa
    {
        public string name { get; set; }
        public Aaa(string name)
        {
            this.name = name;
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
        public Monster NowSelect()
        {
            Monster now=null;
            //スタメンを決める
            
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < this.Monsters.Count; i++)//主人公のモンスターリストから
                {
                    if (!this.Monsters[i].isDead)
                    {
                        Console.WriteLine("【{0}】 {1}", i + 1, this.Monsters[i].Name);
                    }
                    else
                    {
                        Console.WriteLine("【{0}】 {1}(ひんし)", i + 1, this.Monsters[i].Name);
                    }
                }
                Console.Write("\nどれを出す？>>");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int num))
                {
                    if (1 <= num && num <= 3)
                    {
                        if (!this.Monsters[num - 1].isDead)
                        {
                            now = this.Monsters[num - 1];
                            Console.WriteLine("{0}は{1}をくりだした。", this.Name, now.Name);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("{0}はひんしです", this.Monsters[num - 1].Name);
                        }
                    }
                }
                Console.WriteLine("1～{0}で入力してください", this.Monsters.Count);
                Thread.Sleep(1000);
                Console.Clear();
            }
            return now;
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
            bool ans = false;
            foreach (var skill in SkillSet)
            {
                if (skill.Num != 0)
                {
                    ans = true;
                }
            }
            return ans;
        }
        public void AttackEnemy(string name,Monster target,int skillNum)//ダメージ計算=monster.Attack+skill.Attack-target.Defence、急所なら1.5倍
        {
            if(skillNum == 3)//回復
            {
                this.RemainHp += this.SkillSet[skillNum].Heal;
                int tmp = 0;
                if (this.RemainHp > this.MaxHp)
                {
                    tmp = this.RemainHp-this.MaxHp;
                    this.RemainHp=this.MaxHp;
                    Console.WriteLine("{0} は {1} かいふくした", this.Name, tmp);
                }
                else
                {
                    Console.WriteLine("{0} は {1} かいふくした", this.Name, this.SkillSet[skillNum].Heal);
                }
                this.SkillSet[skillNum].Num--;
            }
            else
            {
                Console.WriteLine("{0}の{1}は{2}をくりだした", name, this.Name, this.SkillSet[skillNum].SkillName);
                int att = 0;
                if (this.SkillSet[skillNum].Type == SkillType.ほのお && target.Type == MonsterType.くさ)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack * 1.5) - target.Defence;
                    Console.WriteLine("こうかはばつぐんだ！");
                }
                else if (this.SkillSet[skillNum].Type == SkillType.みず && target.Type == MonsterType.ほのお)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack * 1.5) - target.Defence;
                    Console.WriteLine("こうかはばつぐんだ！");
                }
                else if (this.SkillSet[skillNum].Type == SkillType.くさ && target.Type == MonsterType.みず)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack *1.5) - target.Defence;
                    Console.WriteLine("こうかはばつぐんだ！");
                }
                else if (this.SkillSet[skillNum].Type == SkillType.ほのお && target.Type == MonsterType.みず)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack * 0.7) - target.Defence;
                    Console.WriteLine("こうかはいまひとつのようだ");
                }
                else if (this.SkillSet[skillNum].Type == SkillType.みず && target.Type == MonsterType.くさ)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack * 0.7) - target.Defence;
                    Console.WriteLine("こうかはいまひとつのようだ");
                }
                else if (this.SkillSet[skillNum].Type == SkillType.くさ && target.Type == MonsterType.ほのお)
                {
                    att = this.Attack + (int)(this.SkillSet[skillNum].Attack * 0.7) - target.Defence;
                    Console.WriteLine("こうかはいまひとつのようだ");
                }
                else
                {
                    att = this.Attack + this.SkillSet[skillNum].Attack - target.Defence;
                }
                target.RemainHp -= att;
                Console.WriteLine("{0}に{1}のダメージ", target.Name, att);
                this.SkillSet[skillNum].Num--;//プロパティあるからメソッドいらない
                                              //HPのチェック
                if (target.RemainHp < 0)
                {
                    target.isDead = true;
                    target.RemainHp = 0;
                }
            }




            
            /*問題点
             * 1.
             * 2.
             */



            
        }
        public void AttackEnemy(Monster target)
        {
            Console.WriteLine("{0}はじたばたした",this.Name);
            int att= new Random().Next(5, 11);
            target.RemainHp -= att;
            Console.WriteLine("{0}に{1}のダメージ", target.Name, att);
        }
    }
    public class Skill
    {
        public string SkillName { get; set; }//skill name
        public SkillType Type { set; get; }//skill type
        public int Attack { set; get; }//skill base of attack
        public int Heal { set; get; } = 0;
        public int Num { set; get; } = 3;//how many used this skill ,but rival is ignore
        public Skill(string name, SkillType type, int attack, int heal)
        {
            this.SkillName = name;
            this.Type = type;
            this.Attack = attack;
            this.Heal = heal;
        }


    }
    public enum MonsterType { ほのお = 0, みず = 1, くさ = 2 }
    public enum SkillType { ほのお = 0, みず = 1, くさ = 2, ノーマル = 3 }
}
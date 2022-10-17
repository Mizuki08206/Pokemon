using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practice
{
    internal class Day1013
    {
        public static void Main(string[] args)
        {
            //Cs1.q();
            //Cs7.q();
            //Cs9.q();
            //Cs10.q();
            //Cs14.Inner inn = new Cs14.Inner();//インナークラスのインスタンス生成
            //inn.q();
            //Cs14.q();

            //3=!
            //5=?
            //15==!?
            //others=そのまま

            Calc.q();
        }
    }
    public class Calc
    {
        public static void q()
        {
            //計算式入力
            Console.WriteLine("計算式を入力してください");
            var inputs= Console.ReadLine().Split(' ');

            if (Check(inputs))//入力された文字列のチェック
            {
                Console.WriteLine("入力問題なし");
            }
            else
            {
                Console.WriteLine("error");
            }


            //bool isca = false;
            //foreach(var input in inputs)
            //{
            //    if(input==)
            //}

        }
        public static bool Check(string[] inputs)//入力された計算式をチェックする
        {
            bool che = true;
            bool numch = false;
            bool opech = false;
            int num;//捨て変数
            foreach(var input in inputs)
            {
                if (int.TryParse(input, out num))
                {
                    if (!numch)
                    {
                        numch = true;
                        opech= false;
                    }
                    else
                    {
                        che = false;
                    }
                }
                else
                {
                    if (!opech)
                    {
                        switch (input)
                        {
                            case "+":
                            case "-":
                            case "*":
                            case "/":
                                opech = true;
                                numch = false;
                                break;
                            default:
                                che = false;
                                break;
                        }
                    }
                    else
                    {
                        che = false;
                    }
                    
                }
            }

            return che;
        }
    }//電卓やろうとしたけどやめちゃった。。。
    public class Inspection//検証用クラス
    {
        public static void a()//readとreadlineの違い
        {
            Console.Write("string>>");
            string str = Console.ReadLine();
            Console.Write("int>>");
            int num = Console.Read();//1文字読みとりで戻り値はint型の文字コード
            Console.WriteLine("文字列：{0}\n数値:{1}", str, (char)num);
        }
    }
    
    public class Cs1//社内研修資料_CS-1
    {
        public static void q()
        {
            //Console.WriteLine("Whats your name?");
            //string? name = Console.ReadLine();
            //Console.WriteLine("Hello {0}!!", name);
        }
    }
    public class Cs6//社内研修資料_CS-6
    {
        public static void q()
        {
            var num = 123;
            var str = "string";
        }
    }
    public class Cs7//社内研修資料_CS-7
    {
        public static void q()
        {
            var str=@"""ab""c\nde";//\nが改行としてみなされない
            Console.WriteLine("@を付けているパターン：{0}",str);
        }
    }
    public class Cs9//社内研修資料_CS-9
    {
        public static void q()
        {
            var nums = new int[] {1,2,3};
            var copy = new int[nums.Length];

            Console.WriteLine("----------------");
            foreach(int num in nums)
            {
                Console.WriteLine(num);
            }
            nums[0] = 100;

            Console.WriteLine("copy=nums");
            copy = nums;
            foreach (int num in copy)
            {
                Console.WriteLine(num);
            }
            //var tmp1;//コメント外すとエラー
            var tmp = 123;

        }
    }
    public class Cs10//社内研修資料_CS-10
    {
        public static void q()
        {
            int age=0;
            a("mizuki",age=24);//24だけ渡すのと違うのは下のWriteLineに影響するかどうかだけ？
            Console.WriteLine(age);//出力：24
        }
        public static void a(string name,int age)
        {
            Console.WriteLine("{0}は{1}歳", name, age);//出力：mizukiは24歳
        }
    }
    public class Cs14//社内研修資料_CS-14
    {
        public static void q()
        {
            int num;//ブロック外にも通用するように外で定義
            while (true)
            {
                Console.WriteLine("数値を入力してください>>");
                string input = Console.ReadLine();
                //TryParseメソッドの戻り値はbool、outキーワードで代入まで行っている
                if (int.TryParse(input,out num))
                {
                    Console.WriteLine("数値が入力されました");
                    break;
                }
                Console.WriteLine("数値が入力されませんでした。");
            }
            Console.WriteLine(num);


            Inner inn = new Inner();
            inn.q();
        }
        public class Inner
        {
            public void q()
            {
                Console.WriteLine("インナークラス");
            }
        }
    }
    
}

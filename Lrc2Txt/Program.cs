
using System.Text;
using System;
using System.IO;
//"[" ASCII码为91，"]"ASCII码为93
namespace Lrc_trimer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("---------\n本程序可以将*.lrc中的时间标签去除，并批量装换为*.txt\n----------\n任意键继续");
            Console.ReadKey();

            //一些提示语句
            FileStream Read = null;
            FileStream Write = null;
            bool ok = false;
            int x = 0;

            //定义一些变量和类
            string[] filenames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.lrc");
            if (filenames == null)
            {
                Console.WriteLine("请将本程序放在lrc文件所在目录运行\n任意键退出");
                Console.ReadKey();
            }

            //以字符串数组的形式获取所有lrc文件名，要是没有就直接退出

            foreach (string i in filenames)

            //用foreach语句对每个文件进行处理
            {
                Read = File.Open(i, FileMode.Open);
                Write = File.Open(i.Replace("lrc", "txt"), FileMode.OpenOrCreate);

                //创建.txt，并初始化读写流
                while (x > -1)

                //"粗略"判断是否读到了结尾
                {
                    x = Read.ReadByte();

                    //读一个字节
                    switch (x)
                    {
                        case 91:

                            ok = false;
                            break;

                        //读到“[”，意味着时间标志开始，关闭bool值ok。隐含着跳过这个字节

                        case 93:

                            ok = true;
                            break;

                        //读到“]”，意味着时间标志结束，打开bool值ok。隐含着跳过这个字节

                        default:

                            if (ok == true & x != -1)
                            { Write.WriteByte(Convert.ToByte(x)); }

                            //通过写入旗帜bool值ok来判断是否要将字节转存到txt文件。是用x!=-1精确判断是否读到了末尾

                            break;

                    }


                }

                x = 0;
                //重要！清除x=-1，否则会出乱子
            }

            //foreach帮助我们遍历并处理所有lrc文件
        }
    }
}
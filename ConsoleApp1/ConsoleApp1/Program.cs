using MvcFrameForProjectModel;
using SQlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //样例
            DemoClass tt = new DemoClass();
            //执行SQL语句
            UsersDbSet_Search_Result entity = tt.DEMOSql();
            Console.WriteLine("entity ID：{0}" ,entity.Id);
            //执行存储过程
            int count = 0;
            List<UsersDbSet_Search_Result> list = tt.DEMOProc(out count);
            foreach (var item in list)
            {
                Console.WriteLine("list{0} ID：{1}", list.IndexOf(item), entity.Id);
            }
            
            Console.WriteLine("列表数量：{0}", count);
            Console.ReadKey();
        }
    }
}

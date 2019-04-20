using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MvcFrameForProjectModel;

namespace SQlHelper
{
    public class DemoClass
    {
        /// <summary>
        /// 样例
        /// </summary>
        public UsersDbSet_Search_Result DEMOSql()
        {
            //执行SQL语句并返回数据
            return ExecuteStoredSQlText<UsersDbSet_Search_Result>.Interface.ExecuteStoredProceduresBackEntity(
                //连接字符串
                ConnectString.Interface.MvcFrameForProjectDb,
                //SQL语句
                "select A.Id,A.ClassId,A.Name,A.Username,A.Passwd,b.ClassName from dbo.UsersDbSet A inner join dbo.UserClassDbSet B on a.ClassId = b.Id where a.Id = 1"
                );

            
        }

        public List<UsersDbSet_Search_Result> DEMOProc(out int Count)
        {
            //返回值
            int _count = 0;
            //执行存储过程
            List<UsersDbSet_Search_Result> Proc = ExecuteStoredProcedures<UsersDbSet_Search_Result>.Interface.ExecuteStoredProceduresBackList(
                //连接字符串
                ConnectString.Interface.MvcFrameForProjectDb,
               //存储过程名
               "UsersDbSet_Search",
               p => {
                   //输入参数
                   p.Add("Id", 1);
               },
               output => {
                   //输出参数
                   _count = output.GetOrDefault<int>("Count", 0);
               });
            Count = _count;
            return Proc;

        }
    }
}

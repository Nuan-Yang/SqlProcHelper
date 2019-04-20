using SQlHelper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQlHelper
{
    /// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <typeparam name="T">返回值实体类型 无返回值类型时给OBJ即可</typeparam>
    public class ExecuteStoredSQlText<T> where T : new()
    {
        #region 单例模式
        private static ExecuteStoredSQlText<T> _Interface { get; set; }

        public static ExecuteStoredSQlText<T> Interface
        {
            get
            {
                if (_Interface == null)
                {
                    _Interface = new ExecuteStoredSQlText<T>();
                }
                return _Interface;
            }
        }
        #endregion

        #region 返回默认值
        /// <summary>
        /// 执行SQL语句并返回默认值
        /// </summary>
        /// <param name="ConString">连接字符串</param>
        /// <param name="ProcName">SQL语句</param>
        public object ExecuteStoredProceduresBackExecuteScalar(string ConString, string ProcName)
        {
            object Rows = null;
            try
            {
                SQLConnect(ConString, ProcName, CommandType.Text, (con, com) => {
                    //打开数据库连接  
                    con.Open();
                    SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(com);
                    //获取影响行数
                    Rows = com.ExecuteScalar();
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Rows;

        }

        #endregion 

        #region 返回实例

        /// <summary>
        /// 执行SQL语句并返回结果实例
        /// </summary>
        /// <param name="ConString">连接字符串</param>
        /// <param name="ProcName">SQL语句</param>
        public T ExecuteStoredProceduresBackEntity(string ConString, string ProcName)
        {
            T Back = new T();
            try
            {
                SQLConnect(ConString, ProcName, CommandType.Text, (con, com) => {
                    //打开数据库连接  
                    con.Open();
                    SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(com);
                    DataSet DS = new DataSet();
                    SqlDataAdapter1.Fill(DS);
                    //转化成实体列表形式
                    Back = ModelHandler<T>.Interface.FillModel(DS.Tables[0].Rows[0]);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Back;

        }
        #endregion

        #region 返回列表
        /// <summary>
        /// 执行SQL语句并返回结果列表
        /// </summary>
        /// <param name="ConString">连接字符串</param>
        /// <param name="ProcName">SQL语句</param>
        public List<T> ExecuteStoredProceduresBackList(string ConString, string ProcName)
        {
            List<T> Back = new List<T>();
            try
            {
                SQLConnect(ConString, ProcName, CommandType.Text, (con, com) => {
                    //打开数据库连接  
                    con.Open();
                    SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(com);
                    DataSet DS = new DataSet();
                    SqlDataAdapter1.Fill(DS);
                    //转化成实体列表形式
                    Back = ModelHandler<T>.Interface.FillModel(DS.Tables[0]);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Back;

        }

        #endregion

        #region 返回结果集
        /// <summary>
        /// 执行SQL语句并返回结果集（可多张表结构），建议少使用影响性能
        /// </summary>
        /// <param name="ConString">连接字符串</param>
        /// <param name="ProcName">SQL语句</param>
        /// <returns></returns>
        public List<List<dynamic>> ExecuteStoredProceduresBackSet(string ConString, string ProcName)
        {
            List<List<dynamic>> Back = new List<List<dynamic>>();
            try
            {
                SQLConnect(ConString, ProcName, CommandType.Text, (con, com) => {
                    //打开数据库连接  
                    con.Open();
                    SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(com);
                    DataSet DS = new DataSet();
                    SqlDataAdapter1.Fill(DS);
                    //遍历DataSet(用于OutPut)
                    List<dynamic> obj = null;
                    foreach (DataTable item in DS.Tables)
                    {
                        //实例化
                        obj = new List<dynamic>();
                        //把当前datatable赋值给obj
                        obj = FillModel(item);
                        Back.Add(obj);
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Back;

        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 表转化为动态类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<dynamic> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            //返回值列表
            List<object> modelList = new List<object>();
            //生成（列名）字典集合

            foreach (DataRow dr in dt.Rows)
            {

                dynamic obj = new System.Dynamic.ExpandoObject();
                var dic = obj as IDictionary<string, object>;
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    dic[dr.Table.Columns[i].ColumnName] = dr[i] != DBNull.Value ? dr[i] : null;
                }
                modelList.Add(obj);
            }
            return modelList;
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="ConString">连接字符串</param>
        /// <param name="ProcName">存储过程名/T-SQL语句</param>
        /// <param name="Type">执行类型
        // 摘要:
        //     SQL 文本命令。 （默认值）。
        //Text = 1,
        //
        // 摘要:
        //     存储过程的名称。
        //StoredProcedure = 4,）
        /// </param>
        /// <param name="Function"></param>
        private void SQLConnect(string ConString, string ProcName, CommandType Type, Action<SqlConnection, SqlCommand> Function)
        {
            using (SqlConnection conStr = new SqlConnection(ConString))
            {
                //SQL语句执行对象，第一个参数是要执行的语句，第二个是数据库连接对象  
                SqlCommand comStr = new SqlCommand(ProcName, conStr);
                //因为要使用的是存储过程，所以设置执行类型为存储过程 
                comStr.CommandType = Type;
                //执行外部方法
                Function(conStr, comStr);
            }
        }
        #endregion
    }
}

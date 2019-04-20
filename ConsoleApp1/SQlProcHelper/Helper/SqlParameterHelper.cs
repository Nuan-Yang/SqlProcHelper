using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQlHelper.Helper
{
    public class SqlParameterHelper
    {
        private SqlParameterCollection Collection { get; set; }
        public SqlParameterHelper(SqlParameterCollection tempInfo)
        {
            Collection = tempInfo;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数数据类型</param>
        /// <param name="direction">参数类型（输入/输出等）</param>
        /// <returns></returns>
        public void Add(string parameterName,object value, SqlDbType dbType, ParameterDirection direction)
        {
            //实例化并设置参数名和值
            SqlParameter back = new SqlParameter(parameterName, value);
            //设置数据类型
            back.SqlDbType = dbType;
            //设置参数类型
            back.Direction = direction;
            //添加
            Collection.Add(back);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">参数类型（输入/输出等）</param>
        /// <returns></returns>
        public void Add(string parameterName, object value,  ParameterDirection direction)
        {
            //实例化并设置参数名和值
            SqlParameter back = new SqlParameter(parameterName, value);
            //设置参数类型
            back.Direction = direction;
            //添加
            Collection.Add(back);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public void Add(string parameterName, object value)
        {
            //实例化并设置参数名和值
            SqlParameter back = new SqlParameter(parameterName, value);
            //添加
            Collection.Add(back);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQlHelper.Helper
{
    public class IReader
    {
        private DataTable Collection { get; set; }
        public IReader(DataTable tempInfo)
        {
            Collection = tempInfo;
        }

        /// <summary>
        /// 获取返回字段（多表时的其他表中）
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="Name">字段名</param>
        /// <returns></returns>
        public T Get<T>(string Name)
        {
            object Back = null;
            //获取该变量
            for (int i = 0; i < Collection.Rows[0].Table.Columns.Count; i++)
            {
                //判断列明是否一致
                bool flag = Collection.Rows[0].Table.Columns[i].ColumnName.ToLower() == Name.ToLower() ? true : false;
                if (flag && Collection.Rows[0][i] != DBNull.Value)
                {
                    //赋值并停止循环
                    Back = Collection.Rows[0][i];
                    break;
                }
                    
            }
            return (T)Back;
        }

        /// <summary>
        /// 获取返回字段（多表时的其他表中）
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="Name">字段名</param>
        /// <param name="Default">默认值</param>
        /// <returns></returns>
        public T GetOrDefault<T>(string Name,T Default)
        {
            object Back = null;
            //获取该变量
            for (int i = 0; i < Collection.Rows[0].Table.Columns.Count; i++)
            {
                //判断列明是否一致
                bool flag = Collection.Rows[0].Table.Columns[i].ColumnName.ToLower() == Name.ToLower() ? true : false;
                if (flag && Collection.Rows[0][i] != DBNull.Value)
                {
                    //赋值并停止循环
                    Back = Collection.Rows[0][i];
                    break;
                }
            }
            if (Back == null)
                return Default;
            return (T)Back;
        }

    }
}

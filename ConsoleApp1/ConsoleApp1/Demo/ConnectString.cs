using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcFrameForProjectModel
{
    public class ConnectString
    {
        #region 单例模式
        private static ConnectString _Interface { get; set; }

        public static ConnectString Interface
        {
            get
            {
                if (_Interface == null)
                {
                    _Interface = new ConnectString();
                }
                return _Interface;
            }
        }
        #endregion

        public string MvcFrameForProjectDb
        {
            get {
                return ConfigurationManager.ConnectionStrings["MvcFrameForProjectDb"].ConnectionString.ToString();
            }
        }
        
    }
}

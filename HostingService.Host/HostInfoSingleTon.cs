using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingService
{
    public class ExeParameter
    {
        public string ExePath { get; set; }
        public string Parameter { get; set; }

        public string ProcessName { get; set; }
    }

    public class HostInfo
    {
        #region 业务属性
        public List<ExeParameter> Exe { get; set; }

        #endregion
    }
    public class HostInfoSingleTon
    {
        private static readonly object _lock = new();
        private static HostInfo _instance { get; set; }
        public static HostInfo Instance { get { return GetInstance(); } }

        private HostInfoSingleTon() { }


        private static HostInfo GetInstance()
        {
            if (_instance != null) return _instance;
            lock (_lock)
            {
                //避免多次创建
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = AppSettingsHelper.GetObject<HostInfo>("HostExe");
            }

            return _instance;
        }

    }
}

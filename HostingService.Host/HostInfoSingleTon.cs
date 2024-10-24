using System.Collections.Generic;

namespace HostingService
{
    public class ExeParameter
    {
        public string ExePath { get; set; }
        public string Parameter { get; set; }

        public string ProcessName { get; set; }

        /// <summary>
        /// 是否需要绕过UAC - 服务启动时，如果需要启动GUI，则需要设置此项为true - windows系统有效
        /// </summary>
        public bool NeedBypassUAC { get; set; }
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

using Abp.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YiyiCook.Infrastruction.Handler
{
    public class ConfigHelper
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        static ConfigHelper()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                Configuration = builder.Build();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取Config文件appSettings节点配置值
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="defval">默认值</param>
        /// <returns></returns>
        public static string GetAppSettingString(string key, string defval = "")
        {
            if (Configuration == null)
            {
                return defval;
            }
            var conn = Configuration.GetSection("appSettings:" + key);
            if (!conn.Exists())
            {
                return defval;
            }

            return conn.Value;
        }

        /// <summary>
        /// 获取Config文件appSettings节点配置值
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="defval">默认值</param>
        /// <returns></returns>
        public static string GetSectionValue(string key, string defval = "")
        {
            if (Configuration == null)
            {
                return defval;
            }
            var conn = Configuration.GetSection(key);
            if (!conn.Exists())
            {
                return defval;
            }
            return conn.Value;
        }

        /// <summary>
        /// 获取Config文件appSettings节点配置值
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="defval">默认值</param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key)
        {
            if (Configuration == null)
            {
                return default;
            }
            return Configuration.GetSection(key);
        }

        public static void Bind(string key, object instance)
        {
            Configuration?.Bind(key, instance);
        }
    }
}

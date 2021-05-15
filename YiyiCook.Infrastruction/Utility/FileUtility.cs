using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YiyiCook.Infrastruction.Utility
{
    public static class FileUtility
    {
        public static string GetFileNameWithExtension(string filename)
        {
            var random = new Random().Next(1123, 9789);
            return String.Format("{0}-{1}{2}", random, Guid.NewGuid().ToString(), System.IO.Path.GetExtension(filename));
        }
        /// <summary>
        /// 注册表获取文件类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetContentTypeForFileName(string fileName)
        {
            //获取文件后缀
            string ext = Path.GetExtension(fileName);
            using (Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext))
            {
                if (registryKey == null)
                    return null;
                var value = registryKey.GetValue("Content Type");
                return value?.ToString();
            }
        }

    }
}

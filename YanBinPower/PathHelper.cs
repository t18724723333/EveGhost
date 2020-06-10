using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanBinPower
{
    public static class PathHelper
    {
        /// <summary>
        /// 返回程序运行目录
        /// </summary>
        /// <returns></returns>
        public static string GetPath() { return AppDomain.CurrentDomain.BaseDirectory; }
        /// <summary>
        /// 返回程序运行目录下的某个目录
        /// </summary>
        /// <param name="str">目录名</param>
        /// <returns></returns>
        public static string GetPath(string str) { return Path.Combine(GetPath(), str); }
        /// <summary>
        /// 返回某个文件的完整路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">文件名</param>
        /// <param name="pattern">扩展名</param>
        /// <returns></returns>
        public static string GetFilePath(string path, string name, string pattern) { return Path.Combine(GetPath(path), name + "." + pattern); }
        /// <summary>
        /// 返回某个文件的完整路径
        /// </summary>
        /// <param name="pathList">PathFileList</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static string GetFilePath(PathFileList pathList, string filename)
        {
            switch (pathList)
            {
                case PathFileList.Account:
                    return GetFilePath("Account", filename, "Acc");
                case PathFileList.Config:
                    return GetFilePath("Config", filename, "Config");
                case PathFileList.Script:
                    return GetFilePath("Script", filename, "Script");
                case PathFileList.UserScript:
                    return GetFilePath("UserScript", filename, "Script");
                default:
                    return null;
            }
        }

        public static string[] GetList(string path, string pattern, bool showdirectory)
        {
            List<string> _ls = new List<string>();
            try
            {
                foreach (string item in Directory.GetFiles(path, "*." + pattern, SearchOption.TopDirectoryOnly))
                {
                    _ls.Add(showdirectory ? item : item.ToLower().Replace("." + pattern.ToLower(), "").Replace(path.ToLower(), "").Replace("\\", ""));
                }
            }
            catch (Exception e)
            {
                throw new System.Exception(e.ToString());
            }
            return _ls.ToArray();
        }
        public static string[] GetList(PathFileList pfl)
        {
            string _pattern;
            switch (pfl)
            {
                case PathFileList.Account:
                    _pattern = "acc";
                    break;
                case PathFileList.Config:
                    _pattern = "cfg";
                    break;
                case PathFileList.Script:
                    _pattern = "script";
                    break;
                case PathFileList.UserScript:
                    _pattern = "script";
                    break;
                default:
                    return null;
            }

            return GetList(GetPath(pfl.ToString()), _pattern, false);
        }
    }
    /// <summary>
    /// 程序路径文件选择
    /// </summary>
    public enum PathFileList
    {
        Account = 1,
        Config = 2,
        Script = 4,
        UserScript = 8
    }
}

using System;
using System.Collections.Generic;
using System.IO;

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
        public static string GetFilePath(PathFileList pathList, string filename) { return GetFilePath(pathList.ToString(), filename, GetPattern(pathList)); }
        public static string[] GetList(PathFileList pfl) { return GetList(GetPath(pfl.ToString()), GetPattern(pfl), false); }
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
                //throw new System.Exception(e.ToString());
            }
            return _ls.ToArray();
        }

        public static string GetPattern(PathFileList pfl)
        {
            switch (pfl)
            {
                case PathFileList.Account:
                    return "Acc";
                case PathFileList.Config:
                    return "Config";
                case PathFileList.Script:
                    return "Script";
                case PathFileList.UserScript:
                    return "Script";
                case PathFileList.Resources:
                    return "Png";
                default:
                    return null;
            }
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
        UserScript = 8,
        Resources = 16
    }
}

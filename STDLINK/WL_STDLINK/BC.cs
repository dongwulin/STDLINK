using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace STDLINK
{
    public class BC:IBC
    {
        public string helloword(string arg)
        {
            return arg + " helloword";
        }

        /// <summary>
        /// 上传 SQLSERVER
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
        public byte SQL_Upload(string[] arg) { return 0; }

        /// <summary>
        /// 读取 SQLSERVER
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>NULL失败</returns>
        public DataSet SQL_Download(string SQL) { return null; }

        /// <summary>
        /// 本地保存 ACCESS
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
        public byte Access_Save(string[] arg) { return 0; }

        /// <summary>
        /// 读取本地 ACCESS
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>NULL失败</returns>
        public DataSet Access_GetData(string SQL) { return null; }

        /// <summary>
        /// 从本地获取IC卡信息
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string GetCardNO() { return null; }

        /// <summary>
        /// 从SQLSERVER获取IC卡权限 00无权限01-98权限等级99网络异常
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public byte GetCardPrivilege(string arg) { return 0; }

        /// <summary>
        /// 发送工单 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
        public byte WS_SendData(string[] arg) { return 0; }

        /// <summary>
        /// 发送设备状态 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
        public byte WS_SendState(string[] arg) { return 0; }

        /// <summary>
        /// 获取工单 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>string[]=“***”，“调用失败”，“网络异常”</returns>
        public string[] WS_GetData(string[] arg) { return null; }

        /// <summary>
        /// 获取心跳包 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>string[]=“***”，“调用失败”，“网络异常”</returns>
        public string[] WS_GetHeart(string[] arg) { return null; }

        /// <summary>
        /// 获取IC卡权限 WEBSERVER
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00无权限01-98权限等级99网络异常</returns>
        public byte WS_GetCardPrivilege(string[] arg) { return 0; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace STDLINK
{
    public interface IBC
    {
         string helloword(string arg);

        /// <summary>
        /// 上传 SQLSERVER
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
         byte SQL_Upload(string[] arg);

        /// <summary>
        /// 读取 SQLSERVER
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>NULL失败</returns>
         DataSet SQL_Download(string SQL);

        /// <summary>
        /// 本地保存 ACCESS
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
         byte Access_Save(string[] arg);

        /// <summary>
        /// 读取本地 ACCESS
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>NULL失败</returns>
         DataSet Access_GetData(string SQL);

        /// <summary>
        /// 从本地获取IC卡信息
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
         string GetCardNO();

        /// <summary>
        /// 从SQLSERVER获取IC卡权限 00无权限01-98权限等级99网络异常
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
         byte GetCardPrivilege(string arg);

        /// <summary>
        /// 发送工单 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
         byte WS_SendData(string[] arg);

        /// <summary>
        /// 发送设备状态 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00成功01-08失败09网络异常</returns>
         byte WS_SendState(string[] arg);

        /// <summary>
        /// 获取工单 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>string[]=“***”，“调用失败”，“网络异常”</returns>
         string[] WS_GetData(string[] arg);

        /// <summary>
        /// 获取心跳包 WEBSERVER 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>string[]=“***”，“调用失败”，“网络异常”</returns>
         string[] WS_GetHeart(string[] arg);

        /// <summary>
        /// 获取IC卡权限 WEBSERVER
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>00无权限01-98权限等级99网络异常</returns>
         byte WS_GetCardPrivilege(string[] arg);
    }
}

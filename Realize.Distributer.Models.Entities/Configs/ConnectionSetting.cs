using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.Configs
{
    /// <summary>
    /// 接続設定情報
    /// </summary>
    public class ConnectionSetting
    {
        /// <summary>
        /// 環境Id
        /// </summary>
        public int EnvironmentInfoId { get; set; }
        /// <summary>
        /// 環境識別子
        /// </summary>
        public string EnvIdentifier { get; set; }
        /// <summary>
        /// DB接続文字列
        /// </summary>
        public string MainteDbConnectionString { get; set; }
        /// <summary>
        /// DB接続文字列
        /// </summary>
        public string MstDbConnectionString { get; set; }
        /// <summary>
        /// ストレージ接続文字列
        /// </summary>
        public string StorageConnectionString { get; set; }
    }
}

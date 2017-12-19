using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Results
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueMessageCheckResult
    {
        /// <summary>
        /// キュー名
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// メッセージ数
        /// </summary>
        public int MessageCount { get; set; }
    }
}

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
    public class HealthCheckResult
    {
        /// <summary>
        /// エンドポイント
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 状態
        /// </summary>
        public List<string> Status { get; set; }
    }
}

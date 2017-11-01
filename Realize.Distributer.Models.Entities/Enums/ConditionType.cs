using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.Enums
{
    /// <summary>
    /// 配布状態
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 未配布
        /// </summary>
        NotDistribute = 1,
        /// <summary>
        /// 配布済み
        /// </summary>
        Deployed = 2
    }
}

using Realize.Distributer.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.MaintenanceDb
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributionTask
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 環境ID
        /// </summary>
        [Index]
        public int EnvironmentInfoId { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        [Index]
        public ConditionType Condition { get; set; }
        /// <summary>
        /// Blob名
        /// </summary>
        public string BlobName { get; set; }
        /// <summary>
        /// 管理メモ
        /// </summary>
        public string Remarks { get; set; }
    }
}
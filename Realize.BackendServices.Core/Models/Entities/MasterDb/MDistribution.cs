using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Entities.MasterDb
{
    /// <summary>
    /// 
    /// </summary>
    public class MDistribution
    {
        /// <summary>
        /// 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Index]
        public DateTimeOffset DistributeDate { get; set; }
        /// <summary>
        /// 無期限か否か
        /// </summary>
        public bool IsIndefinitePeriod { get; set; }
        /// <summary>
        /// ユーザー作成日前の受け取りを許可するか
        /// </summary>
        public bool IsUserCreateDateBeforeApproval { get; set; }
        /// <summary>
        /// Blob名
        /// </summary>
        public string BlobName { get; set; }
    }
}

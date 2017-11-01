using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.Requests.Distribution
{
    public class ApplyRequest
    {
        /// <summary>
        /// 環境ID
        /// </summary>
        [Required]
        public int EnvironmentInfoId { get; set; }
        /// <summary>
        /// 配布物Id
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// 配布開始日時
        /// </summary>
        [Required]
        public DateTimeOffset ApplyDate { get; set; }
        /// <summary>
        /// 無期限か否か
        /// </summary>
        [Required]
        public bool IsIndefinitePeriod { get; set; }
        /// <summary>
        /// ユーザー作成日前の受け取りを許可するか
        /// </summary>
        [Required]
        public bool IsUserCreateDateBeforeApproval { get; set; }
    }
}

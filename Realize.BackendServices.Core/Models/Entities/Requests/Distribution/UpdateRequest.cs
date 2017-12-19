using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Entities.Requests.Distribution
{
    public class UpdateRequest
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
        /// 配布物
        /// </summary>
        [Required]
        public DistributionInfo DistributionInfo { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}

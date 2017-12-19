using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Requests
{
    public class WarmingupRequest
    {
        /// <summary>
        /// 環境ID
        /// </summary>
        [Required]
        public int EnvironmentInfoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClusterNumber { get; set; }
    }
}

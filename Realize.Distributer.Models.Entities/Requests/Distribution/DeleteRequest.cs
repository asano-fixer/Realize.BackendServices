using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.Requests.Distribution
{
    public class DeleteRequest
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
    }
}

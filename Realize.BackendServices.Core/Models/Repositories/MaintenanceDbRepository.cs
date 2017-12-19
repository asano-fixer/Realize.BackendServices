using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realize.BackendServices.Core.Models.DbContexts;
using Realize.BackendServices.Core.Models.Entities.MaintenanceDb;

namespace Realize.BackendServices.Core.Models.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    class MaintenanceDbRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MaintenanceDbRepository(MaintenanceDbContext context)
        {
            this.context = context;
        }

        private MaintenanceDbContext context = null;

        /// <summary>
        /// 
        /// </summary>
        internal void SaveChanges()
        {
            this.context.SaveChanges();
        }

        #region DistributionTasks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributionTask"></param>
        internal void AddDistributionTask(DistributionTask distributionTask)
        {
            this.context.DistributionTasks.Add(distributionTask);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IQueryable<DistributionTask> FindDistributionTasks()
        {
            return this.context.DistributionTasks.AsQueryable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributionTask"></param>
        internal void RemoveDistributionTask(DistributionTask distributionTask)
        {
            this.context.DistributionTasks.Remove(distributionTask);
        }
        #endregion
    }
}

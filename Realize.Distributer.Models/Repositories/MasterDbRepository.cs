using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realize.Distributer.Models.Entities.MasterDb;
using Realize.Distributer.Models.DbContexts;

namespace Realize.Distributer.Models.Repositories
{
    class MasterDbRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MasterDbRepository(MasterDbContext context)
        {
            this.context = context;
        }

        private MasterDbContext context = null;

        /// <summary>
        /// 
        /// </summary>
        internal void SaveChanges()
        {
            this.context.SaveChanges();
        }

        #region MDistributions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distribution"></param>
        internal void AddDistribution(MDistribution distribution)
        {
            this.context.MDistributions.Add(distribution);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IQueryable<MDistribution> FindDistributions()
        {
            return this.context.MDistributions.AsQueryable();
        }
        #endregion
    }
}

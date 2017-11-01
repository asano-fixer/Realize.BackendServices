using Realize.Distributer.Models.DbContexts;
using Realize.Distributer.Models.Properties;
using Realize.Distributer.Models.Repositories;

namespace Realize.Distributer.Models
{
    /// <summary>
    /// 操作ヘルパー
    /// </summary>
    public class OperationHelper
    {
        /// <summary>
        /// 操作ヘルパー
        /// </summary>
        public OperationHelper()
        {
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="environmentInfoId"></param>
        public void Connect(int environmentInfoId)
        {
            Initialize(environmentInfoId);
        }
        /// <summary>
        /// 環境Id
        /// </summary>
        internal int EnvironmentInfoId { get; set; }
        /// <summary>
        /// メンテナンスDbのコンテキスト
        /// </summary>
        MaintenanceDbContext MaintenanceDbContext { get; set; }
        /// <summary>
        /// マスターDbのコンテキスト
        /// </summary>
        MasterDbContext MasterDbContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal string StorageConnectionKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal MaintenanceDbRepository MaintenanceDbRepository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal MasterDbRepository MasterDbRepository { get; set; }

        /// <summary>
        /// 操作ヘルパー
        /// </summary>
        /// <param name="environmentInfoId"></param>
        void Initialize(int environmentInfoId)
        {
            MaintenanceDbContext = new MaintenanceDbContext("MaintenanceDbContext");
            MasterDbContext = new MasterDbContext($"MasterDbContext.{EnvironmentInfoId}");
            StorageConnectionKey = $"StorageConnectionKey.{EnvironmentInfoId}";

            MaintenanceDbRepository = new MaintenanceDbRepository(MaintenanceDbContext);
            MasterDbRepository = new MasterDbRepository(MasterDbContext);
        }
    }
}
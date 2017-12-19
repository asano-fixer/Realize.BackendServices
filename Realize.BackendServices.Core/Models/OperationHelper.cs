using Realize.BackendServices.Core.Models.DbContexts;
using Realize.BackendServices.Core.Models.Entities.Configs;
using Realize.BackendServices.Core.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Realize.BackendServices.Core.Models
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
        public void Connect(List<ConnectionSetting> connectionSettings, int environmentInfoId)
        {
            Initialize(connectionSettings, environmentInfoId);
        }
        /// <summary>
        /// 環境Id
        /// </summary>
        internal int EnvironmentInfoId { get; set; }
        /// <summary>
        /// 環境識別子
        /// </summary>
        internal string EnvIdentifier { get; set; }
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
        internal string StorageConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal MaintenanceDbRepository MaintenanceDbRepository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal MasterDbRepository MasterDbRepository { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        internal IEnumerable<string> MatchmakerEndpoints { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal IEnumerable<string> MultiplayWorkers { get; set; }

        /// <summary>
        /// 操作ヘルパー
        /// </summary>
        /// <param name="environmentInfoId"></param>
        public void Initialize(List<ConnectionSetting> connectionSettings, int environmentInfoId)
        {
            var connectionSetting = connectionSettings.Find(x => x.EnvironmentInfoId == environmentInfoId);

            EnvironmentInfoId = environmentInfoId;
            EnvIdentifier = connectionSetting.EnvIdentifier;

            //MaintenanceDbContext = new MaintenanceDbContext(connectionSetting.MainteDbConnectionString);
            //MasterDbContext = new MasterDbContext(connectionSetting.MstDbConnectionString);
            //MaintenanceDbRepository = new MaintenanceDbRepository(MaintenanceDbContext);
            //MasterDbRepository = new MasterDbRepository(MasterDbContext);

            StorageConnectionString = connectionSetting.StorageConnectionString;
            MatchmakerEndpoints = connectionSetting.MatchmakerEndpoints;
            MultiplayWorkers = connectionSetting.MultiplayWorkers;
        }
    }
}
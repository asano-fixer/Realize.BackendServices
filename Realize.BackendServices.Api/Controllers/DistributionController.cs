using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Realize.BackendServices.Core.Models.Entities.Configs;
using Realize.BackendServices.Core.Models;
using Realize.BackendServices.Core.Models.Entities.Requests.Distribution;
using Realize.BackendServices.Core.Models.Entities.MaintenanceDb;
using Realize.BackendServices.Core.Models.Services;

namespace Realize.BackendServices.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    public class DistributionController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<ConnectionSetting> connectionSettings;
        /// <summary>
        /// 
        /// </summary>
        protected OperationHelper operationHelper = null;

        /// <summary>
        /// 
        /// </summary>
        public DistributionController(IOptions<List<ConnectionSetting>> connectionSettings)
        {
            this.connectionSettings = connectionSettings.Value;
            operationHelper = new OperationHelper();
        }

        /// <summary>
        /// 【未確認】配布情報を登録する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Distribution/Register")]
        public async Task<DistributionTask> Register([FromBody]RegisterRequest request)
        {
            operationHelper.Connect(connectionSettings, request.EnvironmentInfoId);
            return await new DistributionService(operationHelper).RegisterAsync(
                request.DistributionInfo,
                request.Remarks);
        }
        /// <summary>
        /// 【未確認】配布情報の一覧を取得する
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/List/{EnvironmentInfoId}")]
        public async Task<IEnumerable<DistributionTask>> List(int environmentInfoId)
        {
            operationHelper.Connect(connectionSettings, environmentInfoId);
            return new DistributionService(operationHelper).List();
        }
        /// <summary>
        /// 【未確認】配布情報の一覧を取得する（履歴）
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/History/{EnvironmentInfoId}")]
        public async Task<IEnumerable<DistributionTask>> History(int environmentInfoId)
        {
            operationHelper.Connect(connectionSettings, environmentInfoId);
            return new DistributionService(operationHelper).History();
        }
        /// <summary>
        /// 【未確認】配布情報を取得する（Id検索）
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/Find/{EnvironmentInfoId}/{Id}")]
        public async Task<DistributionTask> Find(int environmentInfoId, int id)
        {
            operationHelper.Connect(connectionSettings, environmentInfoId);
            return new DistributionService(operationHelper).FindById(id);
        }
        /// <summary>
        /// 【未確認】配布情報を更新する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Api/Distribution/Update")]
        public async Task<DistributionTask> Update([FromBody]UpdateRequest request)
        {
            operationHelper.Connect(connectionSettings, request.EnvironmentInfoId);
            return await new DistributionService(operationHelper).UpdateAsync(
                request.Id,
                request.DistributionInfo,
                request.Remarks);
        }
        /// <summary>
        /// 【未確認】配布情報をゲームに反映する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Api/Distribution/Apply")]
        public async Task<DistributionTask> Apply([FromBody]ApplyRequest request)
        {
            operationHelper.Connect(connectionSettings, request.EnvironmentInfoId);
            return await new DistributionService(operationHelper).ApplyAsync(
                request.Id,
                request.ApplyDate,
                request.IsIndefinitePeriod,
                request.IsUserCreateDateBeforeApproval);
        }
        /// <summary>
        /// 【未確認】配布情報を削除する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Api/Distribution/Delete")]
        public async Task<DistributionTask> Delete([FromBody]DeleteRequest request)
        {
            operationHelper.Connect(connectionSettings, request.EnvironmentInfoId);
            return await new DistributionService(operationHelper).DeleteAsync(
                request.Id);
        }
    }
}
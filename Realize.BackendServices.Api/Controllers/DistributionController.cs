using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realize.Distributer.Models.Entities.MaintenanceDb;
using Realize.Distributer.Models.Entities.Requests.Distribution;
using Realize.Distributer.Models;
using Microsoft.Extensions.Options;

namespace Realize.BackendServices.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    public class DistributionController : Controller
    {
        protected OperationHelper operationHelper = null;

        /// <summary>
        /// 
        /// </summary>
        public DistributionController()
        {
            operationHelper = new OperationHelper();
        }

        /// <summary>
        /// 配布情報を登録する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Distribution/Register")]
        public async Task<DistributionTask> Register([FromBody]RegisterRequest request)
        {
            operationHelper.Connect(request.EnvironmentInfoId);
            return await new Distributer.Models.Services.DistributionService(operationHelper).RegisterAsync(
                request.DistributionInfo,
                request.Remarks);
        }
        /// <summary>
        /// 配布情報の一覧を取得する
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/List/{EnvironmentInfoId}")]
        public async Task<IEnumerable<DistributionTask>> List(int environmentInfoId)
        {
            operationHelper.Connect(environmentInfoId);
            return new Distributer.Models.Services.DistributionService(operationHelper).List();
        }
        /// <summary>
        /// 配布情報の一覧を取得する（履歴）
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/History/{EnvironmentInfoId}")]
        public async Task<IEnumerable<DistributionTask>> History(int environmentInfoId)
        {
            operationHelper.Connect(environmentInfoId);
            return new Distributer.Models.Services.DistributionService(operationHelper).History();
        }
        /// <summary>
        /// 配布情報を取得する（Id検索）
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Distribution/Find/{EnvironmentInfoId}/{Id}")]
        public async Task<DistributionTask> Find(int environmentInfoId, int id)
        {
            operationHelper.Connect(environmentInfoId);
            return new Distributer.Models.Services.DistributionService(operationHelper).FindById(id);
        }
        /// <summary>
        /// 配布情報を更新する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Api/Distribution/Update")]
        public async Task<DistributionTask> Update([FromBody]UpdateRequest request)
        {
            operationHelper.Connect(request.EnvironmentInfoId);
            return await new Distributer.Models.Services.DistributionService(operationHelper).UpdateAsync(
                request.Id,
                request.DistributionInfo,
                request.Remarks);
        }
        /// <summary>
        /// 配布情報をゲームに反映する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Api/Distribution/Apply")]
        public async Task<DistributionTask> Apply([FromBody]ApplyRequest request)
        {
            operationHelper.Connect(request.EnvironmentInfoId);
            return await new Distributer.Models.Services.DistributionService(operationHelper).ApplyAsync(
                request.Id,
                request.ApplyDate,
                request.IsIndefinitePeriod,
                request.IsUserCreateDateBeforeApproval);
        }
        /// <summary>
        /// 配布情報を削除する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Api/Distribution/Delete")]
        public async Task<DistributionTask> Delete([FromBody]DeleteRequest request)
        {
            operationHelper.Connect(request.EnvironmentInfoId);
            return await new Distributer.Models.Services.DistributionService(operationHelper).DeleteAsync(
                request.Id);
        }
    }
}
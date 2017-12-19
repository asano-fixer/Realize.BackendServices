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
        /// �y���m�F�z�z�z����o�^����
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
        /// �y���m�F�z�z�z���̈ꗗ���擾����
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
        /// �y���m�F�z�z�z���̈ꗗ���擾����i�����j
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
        /// �y���m�F�z�z�z�����擾����iId�����j
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
        /// �y���m�F�z�z�z�����X�V����
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
        /// �y���m�F�z�z�z�����Q�[���ɔ��f����
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
        /// �y���m�F�z�z�z�����폜����
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
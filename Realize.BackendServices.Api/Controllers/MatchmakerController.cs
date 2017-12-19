using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realize.BackendServices.Core.Models.Services;
using Realize.BackendServices.Core.Models.Requests;
using Microsoft.Extensions.Options;
using Realize.BackendServices.Core.Models.Entities.Configs;
using Realize.BackendServices.Core.Models;
using Realize.BackendServices.Core.Models.Results;

namespace Realize.BackendServices.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    public class MatchmakerController : Controller
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
        public MatchmakerController(IOptions<List<ConnectionSetting>> connectionSettings)
        {
            this.connectionSettings = connectionSettings.Value;
            operationHelper = new OperationHelper();
        }

        /// <summary>
        /// �y����m�F�ς݁z�}�b�`���C�J�[���E�H�[�~���O�A�b�v����
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Api/Matchmaker/Warmingup")]
        public async Task Warmingup([FromBody]WarmingupRequest request)
        {
            operationHelper.Connect(connectionSettings, request.EnvironmentInfoId);
            await new MatchmakerService(operationHelper).WarmingupAsync(request.ClusterNumber);
        }
        /// <summary>
        /// �y����m�F�ς݁z�}�b�`���C�J�[���w���X�`�F�b�N����
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Matchmaker/HealthCheck")]
        public async Task<IEnumerable<HealthCheckResult>> HealthCheck(int environmentInfoId)
        {
            operationHelper.Connect(connectionSettings, environmentInfoId);
            return await new MatchmakerService(operationHelper).HealthCheckAsync();
        }
        /// <summary>
        /// �L���[�̐����擾����
        /// </summary>
        /// <param name="environmentInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Matchmaker/QueueMessageCheck")]
        public async Task<IEnumerable<QueueMessageCheckResult>> QueueCheck(int environmentInfoId)
        {
            operationHelper.Connect(connectionSettings, environmentInfoId);
            return await new MatchmakerService(operationHelper).QueueCheckAsync();
        }
    }
}
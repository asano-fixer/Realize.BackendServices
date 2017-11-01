using Realize.Distributer.Models.Entities.MaintenanceDb;
using Realize.Distributer.Models.Entities.MasterDb;
using Realize.Distributer.Models.Entities.Requests.Distribution;
using Realize.Helpers;
using Realize.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Services
{
    /// <summary>
    /// 配布物に関するサービスクラス
    /// </summary>
    public class DistributionService : AbstractService
    {
        /// <summary>
        /// 配布物に関するサービスクラス
        /// </summary>
        /// <param name="operationHelper"></param>
        public DistributionService(OperationHelper operationHelper)
            : base(operationHelper)
        {
        }

        /// <summary>
        /// 配布情報を登録する
        /// </summary>
        /// <param name="distributionInfo">配布物</param>
        /// <param name="remarks">備考</param>
        /// <returns></returns>
        public async Task<DistributionTask> RegisterAsync(DistributionInfo distributionInfo, string remarks = null)
        {
            try
            {
                if (distributionInfo == null)
                    throw new Exception("parameter 'distributionItem' is required.");

                return await ExecuteAsync(
                    () =>
                    {
                        var distributionTask = new DistributionTask();
                        distributionTask.EnvironmentInfoId = operationHelper.EnvironmentInfoId;
                        distributionTask.Condition = Entities.Enums.ConditionType.NotDistribute;
                        distributionTask.BlobName = Guid.NewGuid().ToString();
                        distributionTask.Remarks = remarks;

                        operationHelper.MaintenanceDbRepository.AddDistributionTask(distributionTask);
                        operationHelper.MaintenanceDbRepository.SaveChanges();

                        new BlobHelper(operationHelper.StorageConnectionKey, $"distribution-{distributionTask.EnvironmentInfoId}")
                            .Upload($"{distributionTask.BlobName}", distributionInfo.Serialize());

                        return distributionTask;
                    });
            }
            catch (Exception ex)
            {
                await SendmessageAsync(ex);
                throw;
            }
        }
        /// <summary>
        /// 一覧を取得する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DistributionTask> List()
        {
            try
            {
                return operationHelper.MaintenanceDbRepository
                    .FindDistributionTasks()
                    .Where(x => x.EnvironmentInfoId == operationHelper.EnvironmentInfoId)
                    .Where(x => x.Condition == Entities.Enums.ConditionType.NotDistribute)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Sendmessage(ex);
                throw;
            }
        }
        /// <summary>
        /// 一覧を取得する（履歴）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DistributionTask> History()
        {
            try
            {
                return operationHelper.MaintenanceDbRepository
                    .FindDistributionTasks()
                    .Where(x => x.EnvironmentInfoId == operationHelper.EnvironmentInfoId)
                    .Where(x => x.Condition == Entities.Enums.ConditionType.Deployed)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Sendmessage(ex);
                throw;
            }
        }
        /// <summary>
        /// 配布タスクを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DistributionTask FindById(int id)
        {
            try
            {
                return operationHelper.MaintenanceDbRepository
                    .FindDistributionTasks()
                    .Where(x => x.EnvironmentInfoId == operationHelper.EnvironmentInfoId)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Sendmessage(ex);
                throw;
            }
        }
        /// <summary>
        /// 配布内容を更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="distributionInfo">配布物</param>
        /// <param name="remarks">備考</param>
        /// <returns></returns>
        public async Task<DistributionTask> UpdateAsync(int id, DistributionInfo distributionInfo, string remarks = null)
        {
            try
            {
                if (id == 0)
                    throw new Exception("parameter 'id' is required.");
                if (distributionInfo == null)
                    throw new Exception("parameter 'distributionItem' is required.");

                return await ExecuteAsync(
                    () =>
                    {
                        var original = FindById(id);
                        var oldBlobName = original.BlobName;

                        original.BlobName = Guid.NewGuid().ToString();
                        original.Remarks = remarks;

                        operationHelper.MaintenanceDbRepository.SaveChanges();

                        var blob = new BlobHelper(operationHelper.StorageConnectionKey, $"distribution-{original.EnvironmentInfoId}");
                        blob.Delete($"{oldBlobName}");
                        blob.Upload($"{original.BlobName}", distributionInfo.Serialize());

                        return original;
                    });
            }
            catch (Exception ex)
            {
                await SendmessageAsync(ex);
                throw;
            }
        }
        /// <summary>
        /// 配布する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applyDate"></param>
        /// <returns></returns>
        public async Task<DistributionTask> ApplyAsync(int id, DateTimeOffset applyDate,bool isIndefinitePeriod,bool isUserCreateDateBeforeApproval)
        {
            try
            {
                if (id == 0)
                    throw new Exception("parameter 'id' is required.");
                if (applyDate < DateTimeOffset.Now)
                    throw new Exception("please set a future date for the parameter 'applyDate'.");

                return await ExecuteAsync(
                    () =>
                    {
                        var distributionTask = FindById(id);

                        var distribution = new MDistribution();
                        distribution.Id = distributionTask.Id;
                        distribution.DistributeDate = applyDate;
                        distribution.IsIndefinitePeriod = isIndefinitePeriod;
                        distribution.IsUserCreateDateBeforeApproval = isUserCreateDateBeforeApproval;
                        distribution.BlobName = distributionTask.BlobName;
                        operationHelper.MasterDbRepository.AddDistribution(distribution);
                        operationHelper.MasterDbRepository.SaveChanges();

                        distributionTask.Condition = Entities.Enums.ConditionType.Deployed;
                        operationHelper.MaintenanceDbRepository.SaveChanges();

                        return distributionTask;
                    });
            }
            catch (Exception ex)
            {
                await SendmessageAsync(ex);
                throw;
            }
        }
        /// <summary>
        /// 配布
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DistributionTask> DeleteAsync(int id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("parameter 'id' is required.");

                return await ExecuteAsync(
                    () =>
                    {
                        var distributionTask = FindById(id);
                        if (distributionTask.Condition == Entities.Enums.ConditionType.Deployed)
                        {
                            var distribution = operationHelper.MasterDbRepository
                                .FindDistributions()
                                .Where(x => x.Id == distributionTask.Id)
                                .FirstOrDefault();

                            if (distribution.DistributeDate > DateTimeOffset.Now.AddMinutes(1))
                                throw new Exception("could not delete.");
                        }

                        operationHelper.MaintenanceDbRepository.RemoveDistributionTask(distributionTask);
                        operationHelper.MaintenanceDbRepository.SaveChanges();

                        new BlobHelper(operationHelper.StorageConnectionKey, $"distribution-{distributionTask.EnvironmentInfoId}").Delete($"{distributionTask.BlobName}");

                        return distributionTask;
                    });
            }
            catch (Exception ex)
            {
                await SendmessageAsync(ex);
                throw;
            }
        }
    } 
}

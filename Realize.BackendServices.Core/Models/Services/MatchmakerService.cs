using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Realize.BackendServices.Core.Models.Helpers;
using Realize.BackendServices.Core.Models.Results;
using Realize.Entities.Queue;
using Realize.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MatchmakerService : AbstractService
    {
        /// <summary>
        /// 配布物に関するサービスクラス
        /// </summary>
        /// <param name="operationHelper"></param>
        public MatchmakerService(OperationHelper operationHelper)
            : base(operationHelper)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusterNumber"></param>
        /// <returns></returns>
        public async Task WarmingupAsync(string clusterNumber = null)
        {
            BlobHelper.Setup(operationHelper.StorageConnectionString, "cloudqueueclustersettings");
            var json = clusterNumber.IsEmpty()
                ? BlobHelper.Download($"CloudQueueClusterSettings.{operationHelper.EnvIdentifier}.json")
                : BlobHelper.Download($"CloudQueueClusterSettings.{operationHelper.EnvIdentifier}.{clusterNumber}.json");

            var clusters = JsonConvert.DeserializeObject<QueueCluster[]>(json);

            foreach (var cluster in clusters)
            {
                foreach (var queueNode in cluster.QueueNodes)
                {
                    await Task.Run(() => 
                    {
                        var account = CloudStorageAccount.Parse(queueNode.CloudStorageAccount);
                        var client = account.CreateCloudQueueClient();
                        var queue = client.GetQueueReference($"{operationHelper.EnvIdentifier}-{queueNode.Name}".ToLower());
                        queue.CreateIfNotExists();

                        for (var i = 0; i < 100; i++)
                        {
                            queue.AddMessageAsync(new Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage("0"));
                            Task.Delay(10);
                        }
                    });
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HealthCheckResult>> HealthCheckAsync()
        {
            var results = new List<HealthCheckResult>();

            foreach (var matchmakerEndpoint in operationHelper.MatchmakerEndpoints)
            {
                var result = new HealthCheckResult();
                result.Endpoint = matchmakerEndpoint;
                result.Status = new List<string>();

                using (var runspace = RunspaceFactory.CreateRunspace())
                {
                    runspace.Open();

                    using (var ps = PowerShell.Create())
                    {
                        ps.Runspace = runspace;

                        var connect = new PSCommand();
                        connect.AddCommand($"Connect-ServiceFabricCluster");
                        connect.AddParameter("ConnectionEndpoint", matchmakerEndpoint);
                        ps.Commands = connect;
                        ps.Invoke();

                        foreach (var multiplayWorker in operationHelper.MultiplayWorkers)
                        {
                            var commands = new PSCommand();
                            commands.AddCommand($"Get-ServiceFabricServiceHealth");
                            commands.AddParameter("ServiceName", multiplayWorker);
                            ps.Commands = commands;
                            var status = ps.Invoke();

                            foreach (var state in status)
                                result.Status.Add(state.BaseObject.ToString());
                        }
                    }
                }

                results.Add(result);
            }

            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clusterNumber"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QueueMessageCheckResult>> QueueCheckAsync(string clusterNumber = null)
        {
            BlobHelper.Setup(operationHelper.StorageConnectionString, "cloudqueueclustersettings");
            var json = clusterNumber.IsEmpty()
                ? BlobHelper.Download($"CloudQueueClusterSettings.{operationHelper.EnvIdentifier}.json")
                : BlobHelper.Download($"CloudQueueClusterSettings.{operationHelper.EnvIdentifier}.{clusterNumber}.json");

            var clusters = JsonConvert.DeserializeObject<QueueCluster[]>(json);

            var results = new List<QueueMessageCheckResult>();

            foreach (var cluster in clusters)
            {
                foreach (var queueNode in cluster.QueueNodes)
                {
                    var result = new QueueMessageCheckResult();

                    var account = CloudStorageAccount.Parse(queueNode.CloudStorageAccount);
                    var client = account.CreateCloudQueueClient();
                    var queue = client.GetQueueReference($"{operationHelper.EnvIdentifier}-{queueNode.Name}".ToLower());
                    queue.CreateIfNotExists();

                    result.QueueName = queue.Name;
                    result.MessageCount = queue.ApproximateMessageCount ?? 0;

                    results.Add(result);
                }
            }

            return results;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Realize.BackendServices.BatchManager
{
    /// <summary>
    /// Service Fabric ランタイムによって、このクラスのインスタンスがサービス レプリカごとに作成されます。
    /// </summary>
    internal sealed class BatchManager : StatefulService
    {
        public BatchManager(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// このサービス レプリカがクライアント要求やユーザー要求を処理するためのリスナー (HTTP、Service Remoting、WCF など) を作成する、省略可能なオーバーライド。
        /// </summary>
        /// <remarks>
        ///サービスの通信の詳細については、https://aka.ms/servicefabricservicecommunication を参照してください
        /// </remarks>
        /// <returns>リスナーのコレクション。</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
        }

        /// <summary>
        /// これは、サービス レプリカのメイン エントリ ポイントです。
        /// このメソッドは、サービスのこのレプリカがプライマリになって、書き込み状態になると実行されます。
        /// </summary>
        /// <param name="cancellationToken">Service Fabric がこのサービス レプリカをシャットダウンする必要が生じると、キャンセルされます。</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: 次のサンプル コードを独自のロジックに置き換えるか、
            //       サービスで不要な場合にはこの RunAsync オーバーライドを削除します。

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // CommitAsync の呼び出し前に例外がスローされると、トランザクションが強制終了し、すべての変更が 
                    // 破棄されます。セカンダリ レプリカには何も保存されません。
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}

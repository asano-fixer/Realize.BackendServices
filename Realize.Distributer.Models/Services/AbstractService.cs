using Realize.Distributer.Models.Properties;
using Realize.Distributer.Models.Repositories;
using Realize.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Realize.Distributer.Models.Services
{
    /// <summary>
    /// サービスの規定クラス
    /// </summary>
    public abstract class AbstractService
    {
        /// <summary>
        /// サービスの規定クラス
        /// </summary>
        public AbstractService(OperationHelper operationHelper)
        {
            this.operationHelper = operationHelper;
        }

        /// <summary>
        /// 操作ヘルパー
        /// </summary>
        protected OperationHelper operationHelper = null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implement"></param>
        /// <returns></returns>
        internal async Task<T> ExecuteAsync<T>(Func<T> implement)
        {
            return await Task.Run(() => 
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        var response = implement.Invoke();
                        scope.Complete();

                        return response;
                    }
                    catch
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal void Sendmessage(Exception ex)
        {
            SendmessageAsync(ex).Wait();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        internal async Task SendmessageAsync(Exception ex)
        {
            await Task.Yield();
            ChatworkUtility.SendmessageAsync($"<{Properties.Settings.Default.EnvironmentInfoId}> Realize.Distributer", ex);
        } 
    }
}

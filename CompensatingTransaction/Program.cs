namespace CompensatingTransaction
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompensatingTransaction.Handler;
    using CompensatingTransaction.Interfaces;
    using CompensatingTransaction.Order.Context;
    using CompensatingTransaction.Order.Workers;

    class Program
    {
        static async Task Main(string[] args)
        {
            var workers = new List<IWorker<OrderRequest, OrderResponse>>()
            {
                new Worker1(),
                new Worker2()
            };

            var orderWorkHandler = new WorkHandler<OrderRequest, OrderResponse>(workers);
            await orderWorkHandler.Execute(new OrderRequest
            {
                OperationId = 1002
            });
        }
    }
}

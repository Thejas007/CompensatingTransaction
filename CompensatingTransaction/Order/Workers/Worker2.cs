namespace CompensatingTransaction.Order.Workers
{
    using System;
    using System.Threading.Tasks;
    using CompensatingTransaction.Interfaces;
    using CompensatingTransaction.Order.Context;

    public class Worker2 : IWorker<OrderRequest, OrderResponse>
    {
        private Worker2RollBackContext rollBackContext;

        public int Order => 2;

        public Task<OrderResponse> Execute(OrderRequest request)
        {
            Console.WriteLine("Executing " + nameof(Worker2));
            rollBackContext = new Worker2RollBackContext();
            throw new Exception("Failed to process " + nameof(Worker2));
        }

        public Task RollBack()
        {
            Console.WriteLine("Rolling back" + nameof(Worker2));
            return Task.CompletedTask;
        }
    }
}

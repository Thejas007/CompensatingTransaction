namespace CompensatingTransaction.Order.Workers
{
    using System;
    using System.Threading.Tasks;
    using CompensatingTransaction.Interfaces;
    using CompensatingTransaction.Order.Context;

    public class Worker1 : IWorker<OrderRequest, OrderResponse>
    {
        private Worker1RollBackContext rollBackContext;

        public int Order => 1;

        public Task<OrderResponse> Execute(OrderRequest request)
        {
            Console.WriteLine("Executing " + nameof(Worker1) );
            rollBackContext = new Worker1RollBackContext();

            return Task.FromResult(new OrderResponse
            {
                IsSuccess = true,
                OperationId = request.OperationId,
                SubStatus = nameof(Worker1) + " Completed",
            });
        }

        public Task RollBack()
        {
            Console.WriteLine("Rolling back " + nameof(Worker1));

            return Task.CompletedTask;
        }
    }
}

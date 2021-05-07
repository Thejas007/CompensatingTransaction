namespace CompensatingTransaction.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CompensatingTransaction.Interfaces;

    public class WorkHandler<TIn, TOut> where TIn : BaseBackgroundOperationRequest where TOut : BaseBackgroundOperationResponse
    {
        private readonly IList<IWorker<TIn, TOut>> workers;

        public WorkHandler(IList<IWorker<TIn, TOut>> workers)
        {
            this.workers = workers;
        }

        public async Task Execute(TIn request)
        {
            // update background job status to inprogress
            Console.WriteLine($"Updated status to IN_PROGRESS for background operation with id {request.OperationId} ");
            var completedWorkers = new Stack<IWorker<TIn, TOut>>();
            foreach (var worker in workers.OrderBy(p => p.Order))
            {
                try
                {
                    // update background job sub status to name of Worker
                    var response = await worker.Execute(request);
                    if (response.IsSuccess)
                    {
                        completedWorkers.Push(worker);
                    }
                    else
                    {
                        await worker.RollBack();
                        break;
                    }

                    Console.WriteLine($"Updated Sub status to {response.SubStatus} for background operation with id {request.OperationId} ");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // update background job status to Failed
                    Console.WriteLine($"Updated status to FAILED for background operation with id {request.OperationId} ");
                    await worker.RollBack();
                    break;
                }
            }

            foreach (var completedWorker in completedWorkers)
            {
                try
                {
                    await completedWorker.RollBack();
                }
                catch (Exception e)
                {
                    // Log and continue for other
                    Console.WriteLine(e);
                }
            }
        }
    }
}

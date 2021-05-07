namespace CompensatingTransaction.Interfaces
{
    using System.Threading.Tasks;

    public interface IWorker<in TIn, TOut>
    {
        public int Order { get; }

        public Task<TOut> Execute(TIn request);

        public Task RollBack();
    }
}
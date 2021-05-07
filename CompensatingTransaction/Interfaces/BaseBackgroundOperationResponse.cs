namespace CompensatingTransaction.Interfaces
{
    public class BaseBackgroundOperationResponse
    {
        public int OperationId { get; set; }

        public bool IsSuccess { get; set; }

        public string SubStatus { get; set; }
    }
}
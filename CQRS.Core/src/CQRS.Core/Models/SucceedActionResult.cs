namespace CQRS.Core.Models
{
    public class SucceedActionResult<T> : ActionResult
    {
        public T Content { get; }

        public SucceedActionResult(T result) : base(true)
        {
            Content = result;
        }
    }
}
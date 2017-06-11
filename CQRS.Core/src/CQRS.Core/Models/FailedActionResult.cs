namespace CQRS.Core.Models
{
    public class FailedActionResult : ActionResult
    {
        public string ErrorMessage { get; }

        public FailedActionResult(string errorMessage) : base((bool) false)
        {
            ErrorMessage = errorMessage;
        }
    }
}
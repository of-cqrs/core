namespace CQRS.Core.Models
{
    public class ActionResult
    {
        public bool IsSucceed { get; }

        public ActionResult(bool isSucceed)
        {
            IsSucceed = isSucceed;
        }
    }
}
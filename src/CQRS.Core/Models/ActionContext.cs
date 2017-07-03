namespace CQRS.Core.Models
{
    public class ActionContext<TAction, TResult>
    {
        public ActionType Type { get; set; }

        public TAction Action { get; set; }
        public TResult Result { get; set; }
    }
}
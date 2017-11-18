using System;
using System.Threading.Tasks;

namespace CQRS.Core.Models
{
    public delegate Task DispatchAction(ActionContextBase context);

    public class ActionContext<TAction, TResult> : ActionContextBase
    {
        public ActionContext() : base(typeof(TAction), typeof(TResult))
        {
        }
    }

    public abstract class ActionContextBase
    {
        public ActionType Type { get; set; }

        public ActionBase Action { get; set; }
        public ActionResult Result { get; set; }
        public Type ResultType { get; }
        public Type ActionType { get; }

        protected ActionContextBase(Type actionType, Type resultType)
        {
            ActionType = actionType ?? throw new ArgumentException(nameof(actionType));
            ResultType = resultType ?? throw new ArgumentException(nameof(resultType));
        }
    }
}
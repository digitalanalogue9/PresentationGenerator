using System.Linq;

namespace PresentationGenerator.Core.Utility.Voodoo
{
    public interface IProjectionExpression
    {
        IQueryable<TResult> To<TResult>();
    }
}
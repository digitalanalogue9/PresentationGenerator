using System;
using System.Linq;
using System.Linq.Expressions;

namespace PresentationGenerator.Core.Utility.Voodoo
{

    public static class QueryableExtensions
    {
        public static IProjectionExpression Project<TSource>(
            this IQueryable<TSource> source)
        {
            return new ProjectionExpression<TSource>(source);
        }



    }
}
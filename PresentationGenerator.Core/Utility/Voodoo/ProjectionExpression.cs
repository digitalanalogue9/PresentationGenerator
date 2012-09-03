using System;
using System.Linq;
using System.Linq.Expressions;

namespace PresentationGenerator.Core.Utility.Voodoo
{
    public class ProjectionExpression<TSource>
        : IProjectionExpression
    {
        private readonly IQueryable<TSource> _source;

        public ProjectionExpression(IQueryable<TSource> source)
        {
            _source = source;
        }

        public IQueryable<TResult> To<TResult>()
        {
            Expression<Func<TSource, TResult>> expr = BuildExpression<TResult>();

            return _source.Select(expr);
        }

        public static Expression<Func<TSource, TResult>> BuildExpression<TResult>()
        {
            var sourceMembers = typeof(TSource).GetProperties();
            var destinationMembers = typeof(TResult).GetProperties();

            var name = "src";

            var parameterExpression = Expression.Parameter(typeof(TSource), name);

            return Expression.Lambda<Func<TSource, TResult>>(
                Expression.MemberInit(
                    Expression.New(typeof(TResult)),
                    destinationMembers.Select(dest => Expression.Bind(dest,
                                                                      Expression.Property(
                                                                          parameterExpression,
                                                                          sourceMembers.First(pi => pi.Name == dest.Name)
                                                                          )
                                                          )).ToArray()
                    ),
                parameterExpression
                );
        }
    }
}
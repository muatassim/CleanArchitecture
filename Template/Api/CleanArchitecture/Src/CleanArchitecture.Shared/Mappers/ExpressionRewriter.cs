using System;
using System.Linq.Expressions;
namespace CleanArchitecture.Shared.Mappers
{
public class ExpressionRewriter<TSource, TTarget> : ExpressionVisitor
{
    private readonly ParameterExpression _parameter = Expression.Parameter(typeof(TTarget), "x");
    public Expression<Func<TTarget, bool>> Rewrite(Expression<Func<TSource, bool>> sourceExpression)
    {
        switch (sourceExpression)
        {
            case null:
                throw new ArgumentNullException(nameof(sourceExpression));
            default:
            {
                var body = Visit(sourceExpression.Body);
                return Expression.Lambda<Func<TTarget, bool>>(body, _parameter);
            }
        }
    }
    protected override Expression VisitMember(MemberExpression node)
    {
        // Map properties from source to target
        if (node.Member.DeclaringType == typeof(TSource))
        {
            var targetProperty = typeof(TTarget).GetProperty(node.Member.Name);
            if (targetProperty == null)
                throw new InvalidOperationException($"Property '{node.Member.Name}' not found on target type '{typeof(TTarget).Name}'.");
            return Expression.Property(_parameter, targetProperty);
        }
        return base.VisitMember(node);
    }
    protected override Expression VisitParameter(ParameterExpression node)
    {
        // Replace the parameter with the target parameter
        return _parameter;
    }
    }
    }

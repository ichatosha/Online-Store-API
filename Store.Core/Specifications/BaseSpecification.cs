using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; } = null;

        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();


        public Expression<Func<TEntity, object>> OrderByAsec { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; } = null;

        // if the code has filter then it will use this : 
        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Filter = expression;
        }

        public BaseSpecification()
        {
            // Empty to use it without Filteratoin = Where
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Memphis.BusinessLogic.Lib
{
    public class Sorter<TEntity>
    {
        public Sorter(params SorterSortName<TEntity>[] sortnames)
        {
            this.SortNames = sortnames.ToList();
        }

        public List<SorterSortName<TEntity>> SortNames { get; set; }

        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> query, string sortName, bool sortAscending)
        {
            var sort = SortNames.Single(x => x.SortName == sortName);

            IOrderedQueryable<TEntity> result;

            if (sortAscending)
            {
                result = query.OrderBy(sort.SortExpressions[0]);
            }
            else
            {
                result = query.OrderByDescending(sort.SortExpressions[0]);
            }

            for (int i = 1; i < sort.SortExpressions.Count; i++)
            {
                if (sortAscending)
                {
                    result = result.ThenBy(sort.SortExpressions[i]);
                }
                else
                {
                    result = result.ThenByDescending(sort.SortExpressions[i]);
                }
            }

            return result;
        }
    }

    public class SorterSortName<TEntity>
    {
        public SorterSortName(string sortName, params Expression<Func<TEntity, string>>[] expressions)
        {
            this.SortName = sortName;
            this.SortExpressions = expressions.ToList();
        }

        public string SortName { get; set; }
        public List<Expression<Func<TEntity, string>>> SortExpressions { get; set; }
    }
}

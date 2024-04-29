﻿using Hepsi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hepsi.Application.Interface.Repositories
{
    public interface IReadRepository<T> where T : class,IEntityBase,new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T,bool>> predicate=null,
            Func<IQueryable<T>,IIncludableQueryable<T,object>>?include=null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? orderby=null,
            bool enableTracking=false);
        Task<IList<T>> GetAllByPaggingAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
            bool enableTracking = false,int currentPage=1,int pageSize=3);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate=null);
    }
}

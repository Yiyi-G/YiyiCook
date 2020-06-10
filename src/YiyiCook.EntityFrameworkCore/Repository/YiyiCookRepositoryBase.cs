using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiyiCook.EntityFrameworkCore.Repositories
{
    public abstract class YiyiCookRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<YiyiCookDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public YiyiCookRepositoryBase(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
    public abstract class YiyiCookRepositoryBase<TEntity> : EfCoreRepositoryBase<YiyiCookDbContext, TEntity, long>
        where TEntity : class, IEntity<long>
    {
        public YiyiCookRepositoryBase(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

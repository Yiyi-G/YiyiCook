using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Repository
{
    public class FileRepository : YiyiCookRepositoryBase<File>, IFileRepository
    {
        public FileRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Application.Dto.File;

namespace YiyiCook.Application.Abstractions
{
    public interface IFileService : Abp.Application.Services.IApplicationService
    {
        Task<string> GetFilePath(long fid);
        Task<long> SaveFile(long fid, string path = null);
        Task<TempFileDto> GetTempFilePath(string fileName, string path = null);
    }
}

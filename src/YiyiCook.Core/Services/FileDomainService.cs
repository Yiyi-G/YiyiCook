using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiyiCook.Core.IRepositories;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.Services
{
    public interface IFileDomainService : Abp.Domain.Services.IDomainService
    {
        Task<File> GetFile(long fid);
        Task<long> AddTempFile(string fileName);
        Task<File> AddFile(string fileName);
    }
    public class FileDomainService : IFileDomainService
    {
        private readonly IFileRepository _FileyRepository;
        public FileDomainService(IFileRepository fileRepository)
        {
            _FileyRepository = fileRepository;
        }
        public async Task<File> GetFile(long fid)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _FileyRepository.FirstOrDefault(p => p.Id == fid);
            });
        }
        public async Task<long> AddTempFile(string fileName)
        {
            return await _FileyRepository.InsertAndGetIdAsync(new File()
            {
                FileName = fileName,
                IsTemp = true
            });
        }
        public async Task<File> AddFile(string fileName)
        {
            return await _FileyRepository.InsertAsync(new File()
            {
                FileName = fileName,
                IsTemp = false,
                
            });
        }
    }
}

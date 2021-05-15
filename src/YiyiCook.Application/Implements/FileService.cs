using Abp.Domain.Uow;
using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.File;
using YiyiCook.Core.Services;
using YiyiCook.Infrastruction.Handler;
using YiyiCook.Infrastruction.Utility;

namespace YiyiCook.Application.Implements
{

    public class FileService : IFileService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFileDomainService _FileDomainService;
        private readonly IUnitOfWorkManager _UnitOfWorkManager;
        private string _path = "";
        private string _tempPath = "";
        public FileService(
            IObjectMapper objectMapper,
            IFileDomainService fileDomainService,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _objectMapper = objectMapper;
            _FileDomainService = fileDomainService;
            _UnitOfWorkManager = unitOfWorkManager;
            var filePath = ConfigHelper.GetSection("FilePath");
            _path = filePath["Formal"];
            _tempPath = filePath["Temp"];
        }
        public async Task<string> GetFilePath(long fid)
        {
            ExceptionHelper.ThrowIfNotId(fid, nameof(fid));
            var source = await _FileDomainService.GetFile(fid);
            ExceptionHelper.ThrowIfNull(source, nameof(source));
            var filePath = System.IO.Path.Combine(source.IsTemp?_tempPath:_path, source.FileName);
            return filePath;
        }

        public async Task<long> SaveFile(long fid, string path = null)
        {
            ExceptionHelper.ThrowIfNotId(fid, nameof(fid));
            var formalPath = _path;
            var source = await _FileDomainService.GetFile(fid);
            ExceptionHelper.ThrowIfNull(source, nameof(source));
            if (!source.IsTemp) return fid;
            //查找缓存文件
            var sourcePath = System.IO.Path.Combine(_tempPath, source.FileName);
            ExceptionHelper.ThrowIfTrue(!System.IO.File.Exists(sourcePath), "源文件不存在");
            
            //复制缓存文件到正式文件中
            var fileName = FileUtility.GetFileNameWithExtension(source.FileName);
            var relativePath = fileName;
            //若路径不存在，新建路径
            if (!string.IsNullOrWhiteSpace(path))
            {
                path = path.Replace(" ", "");
                formalPath = System.IO.Path.Combine(formalPath, path);
                if (!System.IO.Directory.Exists(formalPath))
                    System.IO.Directory.CreateDirectory(formalPath);
                relativePath = System.IO.Path.Combine(path, fileName);
            }
            var file = await _FileDomainService.AddFile(relativePath);
            formalPath = System.IO.Path.Combine(formalPath, fileName);
            if (System.IO.File.Exists(formalPath))
                System.IO.File.Delete(formalPath);
            System.IO.File.Copy(sourcePath, formalPath);
            await _UnitOfWorkManager.Current.SaveChangesAsync();
            return file.Id;
        }
        public async Task<TempFileDto> GetTempFilePath(string fileName, string path = null)
        {

            var tempPath = _tempPath;
            fileName = FileUtility.GetFileNameWithExtension(fileName);
            var relativePath = fileName;
            if (!string.IsNullOrWhiteSpace(path))
            {
                path = path.Replace(" ", "");
                tempPath = System.IO.Path.Combine(tempPath, path);
                if (!System.IO.Directory.Exists(tempPath))
                    System.IO.Directory.CreateDirectory(tempPath);
                relativePath = System.IO.Path.Combine(path, fileName);
            }
            var fid = await _FileDomainService.AddTempFile(relativePath);
            tempPath = System.IO.Path.Combine(tempPath, fileName);
            return new TempFileDto()
            {
                Fid = fid,
                FilePath = tempPath
            };

        }

    }
}

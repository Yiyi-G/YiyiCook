using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TgnetAbp.Api;
using YiyiCook.Application.Abstractions;
using YiyiCook.Infrastruction.Utility;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ImageController : YiyiCookControllerBase
    {
        private readonly IFileService _FileService;
        public ImageController(IFileService fileService)
        {
            _FileService = fileService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] long fid)
        {
            var path = await _FileService.GetFilePath(fid);
            if (!System.IO.File.Exists(path)) return NotFound();
            using (var sw = new FileStream(path, FileMode.Open))
            {
                var contenttype = FileUtility.GetContentTypeForFileName(path);
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, contenttype);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadImg( List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            List<long> fids = new List<long>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var tempFile = await _FileService.GetTempFilePath(file.FileName, "YiyiCook");
                    using (var stream = System.IO.File.Create(tempFile.FilePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fids.Add(tempFile.Fid);
                }
            }
            return this.JsonApiResult(ErrorCode.None,new { fids = fids });
        }
    }
}

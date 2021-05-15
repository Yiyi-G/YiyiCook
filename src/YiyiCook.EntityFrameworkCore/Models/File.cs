using System;
using System.Collections.Generic;

namespace YiyiCook.Models
{
    public partial class File
    {
        public long Fid { get; set; }
        public string FileName { get; set; }
        public bool IsTemp { get; set; }
    }
}

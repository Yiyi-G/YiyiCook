using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiyiCook.Infrastruction.Enum.FoodOrder
{
    public enum FoodOrderState : byte
    {
        [Description("None")]
        None = 0,
        [Description("取消")]
        Cancel = 1,
        [Description("准备中")]
        Prepairing = 2,
        [Description("完成")]
        Done = 3,
    }
}

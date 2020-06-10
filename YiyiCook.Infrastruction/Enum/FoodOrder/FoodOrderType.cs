using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiyiCook.Infrastruction.Enum.FoodOrder
{
    public enum FoodOrderType: byte
    {
        [Description("None")]
        None = 0,
        [Description("早餐")]
        Breakfast = 1,
        [Description("午餐")]
        Lunch = 2,
        [Description("晚餐")]
        Dinner = 3,
        [Description("夜宵")]
        MidnightSnack = 4,
        [Description("小吃")]
        OrtherSnack = 5,
    }
}

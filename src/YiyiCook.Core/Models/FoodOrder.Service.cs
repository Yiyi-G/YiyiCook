using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Infrastruction.Enum.FoodOrder;

namespace YiyiCook.Core.Models
{
    public partial class FoodOrder
    {
        public FoodOrderType  DefaultOrderType
        {
            get {
                var hour = this.Date.Hour;
                if (hour <= 4)
                    return FoodOrderType.OrtherSnack;
                if (hour <= 10)
                    return FoodOrderType.Breakfast;
                if (hour <= 14)
                    return FoodOrderType.Lunch;
                if (hour <= 16)
                    return FoodOrderType.OrtherSnack;
                if (hour <= 21)
                    return FoodOrderType.Dinner;
                return FoodOrderType.MidnightSnack;
            }
        }
       
    }
}

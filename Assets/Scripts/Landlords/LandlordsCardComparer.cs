using System;
using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsCardComparer : IComparer<List<LandlordsCard>>
    {
        public int Compare(List<LandlordsCard> x, List<LandlordsCard> y)
        {
            var xCardType = LandlordsCardChecker.GetCardType(x);
            var yCardType = LandlordsCardChecker.GetCardType(y);
            if (yCardType == LandlordsCardType.Unknown) throw new NotImplementedException();
            if (yCardType == LandlordsCardType.Rocket) return -1;
            // 以下在确定了牌型的同时, 也确定了张数, 所以不用另做张数检查.
            if (xCardType == LandlordsCardType.Single)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return SingleCompare(x, y);
            }
            if (xCardType == LandlordsCardType.Pair)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return PairCompare(x, y);
            }
                
            if (xCardType == LandlordsCardType.Triplet)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return TripletCompare(x, y);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 单张的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int SingleCompare(List<LandlordsCard> x, IList<LandlordsCard> y)
        {
            return x[0].CompareTo(y[0]);
        }

        /// <summary>
        /// 一对的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int PairCompare(List<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是一对的, 所以取其中一个进行比较, 点数相同时, 未实现
            if (x[0].Number > y[0].Number) return 1;
            if (x[0].Number < y[0].Number) return -1;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三张时的检查方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TripletCompare(List<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是三张的, 所以取其中一个进行比较, 点数相同时, 未实现
            if (x[0].Number > y[0].Number) return 1;
            if (x[0].Number < y[0].Number) return -1;
            throw new NotImplementedException();
        }
    }


}
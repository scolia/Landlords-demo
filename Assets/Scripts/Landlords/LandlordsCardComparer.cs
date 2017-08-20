using System;
using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsCardComparer : IComparer<IList<LandlordsCard>>
    {
        public int Compare(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xCardType = LandlordsCardChecker.GetCardType(x);
            var yCardType = LandlordsCardChecker.GetCardType(y);
            if (yCardType == LandlordsCardType.Unknown) throw new NotImplementedException();
            if (yCardType == LandlordsCardType.Rocket) return -1;
            // 以下在确定了牌型的同时, 也确定了张数, 所以不用另做张数检查.
            if (xCardType == LandlordsCardType.Single)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return SingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.Pair)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return PairComparer(x, y);
            }
            if (xCardType == LandlordsCardType.Triplet)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return TripletComparer(x, y);
            }
            if (xCardType == LandlordsCardType.TripletWithSingle)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return TripletWithSingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.TripletWithPair)
            {
                if (yCardType == LandlordsCardType.Bomb) return -1;
                return TripletWithPairComparer(x, y);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 将扑克牌映射为字典
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        private Dictionary<LandlordsCardNumber, int> PokersToDict(IList<LandlordsCard> pokers)
        {
            var result = new Dictionary<LandlordsCardNumber, int>();
            foreach(var poker in pokers)
            {
                if (!result.ContainsKey(poker.Number)) result[poker.Number] = 1;
                else result[poker.Number] += 1;
            }
            return result;
        }

        /// <summary>
        /// 单张的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int SingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            return x[0].CompareTo(y[0]);
        }

        /// <summary>
        /// 一对的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int PairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是一对的, 所以取其中一个进行比较, 点数相同时, 未实现
            if (x[0].Number > y[0].Number) return 1;
            if (x[0].Number < y[0].Number) return -1;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三张时的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TripletComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是三张的, 所以取其中一个进行比较, 点数相同时, 未实现
            if (x[0].Number > y[0].Number) return 1;
            if (x[0].Number < y[0].Number) return -1;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三带一的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TripletWithSingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xDict = PokersToDict(x);
            var yDict = PokersToDict(y);
            LandlordsCardNumber? xMax = null;
            foreach(KeyValuePair<LandlordsCardNumber, int> item in xDict)
            {
                if (item.Value == 3) xMax = item.Key;
            }
            LandlordsCardNumber? yMax = null;
            foreach (KeyValuePair<LandlordsCardNumber, int> item in yDict)
            {
                if (item.Value == 3) yMax = item.Key;
            }
            // 在牌型确认的情况下, 不可能出现null, 但为了健壮性, 还是做了null的检查
            if (xMax == null || yMax == null) throw new NullReferenceException();
            if (xMax > yMax) return 1;
            if (xMax < yMax) return -1;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三带二比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int TripletWithPairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 和三带一的方法一致, 因为两种都仅关系三张的大小, 而不关系所带的牌的大小
            return TripletWithSingleComparer(x, y);
        }
    }


}
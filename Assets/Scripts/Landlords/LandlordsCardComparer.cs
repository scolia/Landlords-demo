using System;
using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsCardComparer : IComparer<IList<LandlordsCard>>
    {
        public int Compare(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            return MakeCompare(x, y) ? 1 : -1;
        }

        /// <summary>
        /// x是否大于y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool MakeCompare(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xCardType = LandlordsCardChecker.GetCardType(x);
            var yCardType = LandlordsCardChecker.GetCardType(y);
            if (xCardType == LandlordsCardType.Unknown || yCardType == LandlordsCardType.Unknown) throw new NotImplementedException();
            // 火箭是最大的, 当其中一个是火箭, 可以立即得出结果
            if (xCardType == LandlordsCardType.Rocket) return true;
            if (yCardType == LandlordsCardType.Rocket) return false;
            // 炸弹时
            if (xCardType == LandlordsCardType.Bomb)
            {
                if (yCardType == LandlordsCardType.Bomb) return BombComparer(x, y);
                return true;
            }
            // 以下在确定了牌型的同时, 也确定了张数, 所以不用另做张数检查.
            if (xCardType == LandlordsCardType.Single)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return SingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.Pair)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return PairComparer(x, y);
            }
            if (xCardType == LandlordsCardType.Triplet)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return TripletComparer(x, y);
            }
            if (xCardType == LandlordsCardType.TripletWithSingle)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return TripletWithSingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.TripletWithPair)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return TripletWithPairComparer(x, y);
            }
            if (xCardType == LandlordsCardType.QuadplexWithTwoSingle)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return QuadplexWithTwoSingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.QuadplexWithTwoPair)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                return QuadplexWithTwoPairComparer(x, y);
            }
            // 下面的同时需要检查牌的数量
            if (xCardType == LandlordsCardType.Sequence)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                if (x.Count == y.Count) return SequenceComparer(x, y);
            }
            if (xCardType == LandlordsCardType.SequenceOfPairs)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                if (x.Count == y.Count) return SequenceOfPairsComparer(x, y);
            }
            if (xCardType == LandlordsCardType.SequenceOfTriplets)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                if (x.Count == y.Count) return SequenceOfTripletsComparer(x, y);
            }
            if (xCardType == LandlordsCardType.SequenceOfTripletsWithSingle)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                if (x.Count == y.Count) return SequenceOfTripletsWithSingleComparer(x, y);
            }
            if (xCardType == LandlordsCardType.SequenceOfTripletsWithPair)
            {
                if (yCardType == LandlordsCardType.Bomb) return false;
                if (x.Count == y.Count) return SequenceOfTripletsWithPairComparer(x, y);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将扑克牌映射为字典
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        private static Dictionary<LandlordsCardNumber, int> PokersToDict(IList<LandlordsCard> pokers)
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
        private static bool SingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xCard = x[0].Number;
            var yCard = y[0].Number;
            if (xCard == yCard) throw new NotImplementedException();    //点数相同时, 未实现
            return x[0].Number > y[0].Number;
        }

        /// <summary>
        /// 一对的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool PairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是一对的, 所以取其中一个进行比较, 点数相同时, 未实现
            return SingleComparer(x, y);
        }

        /// <summary>
        /// 三张时的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool TripletComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 检查类型后, 这里保证是三张的, 所以取其中一个进行比较, 点数相同时, 未实现
            return SingleComparer(x, y);
        }

        /// <summary>
        /// 三带一的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool TripletWithSingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
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
            if (xMax > yMax) return true;
            if (xMax < yMax) return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三带二比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool TripletWithPairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 和三带一的方法一致, 因为两种都仅关系三张的大小, 而不关系所带的牌的大小
            return TripletWithSingleComparer(x, y);
        }

        /// <summary>
        /// 四带两张单牌的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool QuadplexWithTwoSingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xDict = PokersToDict(x);
            var yDict = PokersToDict(y);
            LandlordsCardNumber? xMax = null;
            foreach (KeyValuePair<LandlordsCardNumber, int> item in xDict)
            {
                if (item.Value == 4) xMax = item.Key;
            }
            LandlordsCardNumber? yMax = null;
            foreach (KeyValuePair<LandlordsCardNumber, int> item in yDict)
            {
                if (item.Value == 4) yMax = item.Key;
            }
            // 在牌型确认的情况下, 不可能出现null, 但为了健壮性, 还是做了null的检查
            if (xMax == null || yMax == null) throw new NullReferenceException();
            if (xMax > yMax) return true;
            if (xMax < yMax) return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 四带两对的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool QuadplexWithTwoPairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 因为只比较4张牌的大小, 所以比较方法是一样的.
            return QuadplexWithTwoSingleComparer(x, y);
        }

        /// <summary>
        /// 炸弹的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool BombComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 因为是炸弹, 所以四张牌一定是相对的, 故只需要比较其中一张的大小
            return SingleComparer(x, y);
        }

        /// <summary>
        /// 顺子的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool SequenceComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            LandlordsCardNumber xMax = x[0].Number;
            LandlordsCardNumber yMax = y[0].Number;
            foreach(var item in x)
            {
                if (item.Number > xMax) xMax = item.Number;
            }
            foreach(var item in y)
            {
                if (item.Number > yMax) yMax = item.Number;
            }
            if (xMax > yMax) return true;
            if (xMax < yMax) return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 双顺的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool SequenceOfPairsComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 因为类型确定, 所以只是比较其中最大的牌的大小, 和单顺的比较方法基本一致
            return SequenceComparer(x, y);
        }

        /// <summary>
        /// 三顺的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool SequenceOfTripletsComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 同样, 只比较最大牌的大小
            return SequenceComparer(x, y);
        }

        /// <summary>
        /// 三顺带单张的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool SequenceOfTripletsWithSingleComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            var xDict = PokersToDict(x);
            var yDict = PokersToDict(y);
            var xList = new List<LandlordsCardNumber>();
            var yList = new List<LandlordsCardNumber>();
            foreach (KeyValuePair<LandlordsCardNumber, int> item in xDict)
            {
                if (item.Value == 3) xList.Add(item.Key);
            }
            foreach (KeyValuePair<LandlordsCardNumber, int> item in yDict)
            {
                if (item.Value == 3) yList.Add(item.Key);
            }
            var xMax = xList[0];
            var yMax = yList[0];
            foreach(var num in xList)
            {
                if (num > xMax) xMax = num;
            }
            foreach (var num in yList)
            {
                if (num > xMax) yMax = num;
            }
            if (xMax > yMax) return true;
            if (xMax < yMax) return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 三顺带对的比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool SequenceOfTripletsWithPairComparer(IList<LandlordsCard> x, IList<LandlordsCard> y)
        {
            // 只关注三张中的最大牌的大小比较, 所以逻辑和带单牌的时候一样
            return SequenceOfTripletsWithSingleComparer(x, y);
        }
    }


}
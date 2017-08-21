using System;
using System.Collections.Generic;

namespace Landlords
{ 
    public class LandlordsCardChecker
    {
        private class CardCheckResult
        {
            public Dictionary<LandlordsCardNumber, int> Map;
            public LandlordsCardNumber Min;
            public LandlordsCardNumber Max;
        }

        /// <summary>
        /// 将多张扑克牌映射为数字和对应的张数的字典, 并获取其中的最大最小值
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        private static CardCheckResult MakeCheckResult(IList<LandlordsCard> pokers)
        {
            var result = new CardCheckResult()
            {
                Map = new Dictionary<LandlordsCardNumber, int>()
            };
            result.Min = pokers[0].Number;
            result.Max = pokers[0].Number;
            foreach (var item in pokers)
            {
                if (!result.Map.ContainsKey(item.Number))
                {
                    result.Map[item.Number] = 1;
                }
                else
                {
                    result.Map[item.Number] += 1;
                }
                if (item.Number < result.Min) result.Min = item.Number;
                if (item.Number > result.Max) result.Max = item.Number;
            }
            return result;
        }

        /// <summary>
        /// 获取牌的类型
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static LandlordsCardType GetCardType(IList<LandlordsCard> pokers)
        {
            if (pokers == null) throw new ArgumentNullException("Pokers can not be null");
            if (pokers.Count == 0) throw new ArgumentException("Request at least one poker");
            if (IsSingle(pokers)) return LandlordsCardType.Single;
            if (IsPair(pokers)) return LandlordsCardType.Pair;
            if (IsTriplet(pokers)) return LandlordsCardType.Triplet;
            if (IsTripletWithSingle(pokers)) return LandlordsCardType.TripletWithSingle;
            if (IsTripletWithPair(pokers)) return LandlordsCardType.TripletWithPair;
            if (IsSequence(pokers)) return LandlordsCardType.Sequence;
            if (IsSequenceOfPairs(pokers)) return LandlordsCardType.SequenceOfPairs;
            if (IsSequenceOfTriplets(pokers)) return LandlordsCardType.SequenceOfTriplets;
            if (IsSequenceOfTripletsWithSingle(pokers)) return LandlordsCardType.SequenceOfTripletsWithSingle;
            if (IsSequenceOfTripletsWithPair(pokers)) return LandlordsCardType.SequenceOfTripletsWithPair;
            if (IsQuadplexWithTwoSingle(pokers)) return LandlordsCardType.QuadplexWithTwoSingle;
            if (IsQuadplexWithTwoPair(pokers)) return LandlordsCardType.QuadplexWithTwoPair;
            if (IsBomb(pokers)) return LandlordsCardType.Bomb;
            if (IsRocket(pokers)) return LandlordsCardType.Rocket;
            return LandlordsCardType.Unknown;
        }

        

        /// <summary>
        /// 判断是否是单张
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSingle (IList<LandlordsCard> pokers)
        {
            return pokers.Count == 1;
        }

        /// <summary>
        /// 判断是否是一对
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsPair(IList<LandlordsCard> pokers)
        {
            if (pokers.Count == 2)
            {
                var result = MakeCheckResult(pokers);
                // 不能包含王
                if (!result.Map.ContainsKey(LandlordsCardNumber.Joker))
                {
                    return result.Map.ContainsValue(2);
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否是三张
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsTriplet(IList<LandlordsCard> pokers)
        {
            if (pokers.Count == 3)
            {
                var result = MakeCheckResult(pokers);
                return result.Map.ContainsValue(3);
            }
            return false;
        }

        /// <summary>
        /// 是否是三带一
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsTripletWithSingle(IList<LandlordsCard> pokers)
        {
            if(pokers.Count == 4)
            {
                var result = MakeCheckResult(pokers);
                return result.Map.ContainsValue(3);
            }
            return false;
        }

        /// <summary>
        /// 是否是三带二
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsTripletWithPair(IList<LandlordsCard> pokers)
        {
            if(pokers.Count == 5)
            {
                var result = MakeCheckResult(pokers);
                return result.Map.ContainsValue(3) && result.Map.ContainsValue(2);
            }
            return false;
        }

        /// <summary>
        /// 检查是否为单顺, 检查的要点是: 张数>=5, 不能有重复的数值, 且: 最大值-最小值==牌数-1, 不包含2或双王
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSequence(IList<LandlordsCard> pokers)
        {
            if (pokers.Count < 5) return false;
            var result = MakeCheckResult(pokers);
            //不包含2或双王
            if (result.Map.ContainsKey(LandlordsCardNumber.Two) || result.Map.ContainsKey(LandlordsCardNumber.Joker)) return false;
            foreach (var count in result.Map.Values)
            {
                if (count != 1) return false;
            }
            // 如果最大的数减去最小的数等于排数-1, 则是顺子, 例如(34567, 对照LandlordsCardNumber枚举, 可得3为0, 7为4, 此时 4-0 = 5 - 1
            if ((int)result.Max - (int)result.Min == pokers.Count - 1) return true;
            // 否则不是
            return false;
        }

        /// <summary>
        /// 是否是双顺, 检查的要点是: 张数>=6, 所有的数值都成对, 最大值-最小值==张数/2-1, 不包含2或双王
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSequenceOfPairs(IList<LandlordsCard> pokers)
        {
            if (pokers.Count < 6) return false;
            var result = MakeCheckResult(pokers);
            //不包含2或双王
            if (result.Map.ContainsKey(LandlordsCardNumber.Two) || result.Map.ContainsKey(LandlordsCardNumber.Joker)) return false;
            foreach (var count in result.Map.Values)
            {
                if (count != 2) return false;
            }
            // 检查最大最小值的间距
            if ((int)result.Max - (int)result.Min == pokers.Count / 2 - 1) return true;
            // 否则不是
            return false;
        }

        /// <summary>
        /// 是否是三顺, 检查要点: 张数>=6, 所有数值都有3个, 最大值-最小值==张数/3-1, 不包括2和双王
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSequenceOfTriplets(IList<LandlordsCard> pokers)
        {
            if (pokers.Count < 6) return false;
            var result = MakeCheckResult(pokers);
            //不包含2或双王
            if (result.Map.ContainsKey(LandlordsCardNumber.Two) || result.Map.ContainsKey(LandlordsCardNumber.Joker)) return false;
            foreach (var count in result.Map.Values)
            {
                if (count != 3) return false;
            }
            if ((int)result.Max - (int)result.Min == pokers.Count / 3 - 1) return true;
            return false;
        }

        /// <summary>
        /// 是否是三顺带单牌, 对于三顺不能是2或双王, 但单张却可以, 甚至两个单张可以是一样的数, 即: 33344455 为合法的.
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSequenceOfTripletsWithSingle(IList<LandlordsCard> pokers)
        {
            if (pokers.Count < 8) return false;
            var result = MakeCheckResult(pokers);
            var numList = new List<LandlordsCard>();
            foreach(var num in result.Map.Keys)
            {
                // 将其中数量为3的牌重新组合成新列表, 再调用相应的函数做判断, 判断时, 牌的花色其实并不重要, 但构造函数要求提供花色, 所以随便提供一个花色, 对判断结果不会有影响.
                if (result.Map[num] == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        numList.Add(new LandlordsCard(num, LandlordsCardSuits.Dianmond));
                    }
                }
            }
            // 牌的总数应该是每一个三张带一个单牌, 而numList中的数量就表示有几个三张, 所以总数为下面公式所示
            if (IsSequenceOfTriplets(numList) && pokers.Count == (numList.Count * 3 + numList.Count * 1)) return true;
            return false;
        }

        /// <summary>
        /// 是否是三顺带对
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsSequenceOfTripletsWithPair(IList<LandlordsCard> pokers)
        {
            if (pokers.Count < 10) return false;
            var result = MakeCheckResult(pokers);
            var numList = new List<LandlordsCard>();
            foreach (var num in result.Map.Keys)
            {
                // 对牌进行计数后, 其值只能是3或2
                if (result.Map[num] != 3 || result.Map[num] != 2) return false;
                // 将其中数量为3的牌重新组合成新列表, 再调用相应的函数做判断, 判断时, 牌的花色其实并不重要, 但构造函数要求提供花色, 所以随便提供一个花色, 对判断结果不会有影响.
                if (result.Map[num] == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        numList.Add(new LandlordsCard(num, LandlordsCardSuits.Dianmond));
                    }
                }
            }
            // 牌的总数应该是每一个三张带一对, 而numList中的数量就表示有几个三张, 所以总数为下面公式所示
            if (IsSequenceOfTriplets(numList) && pokers.Count == (numList.Count * 3 + numList.Count * 2)) return true;
            return false;
        }

        /// <summary>
        /// 是否是四个带两个单张
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsQuadplexWithTwoSingle(IList<LandlordsCard> pokers)
        {
            if (pokers.Count != 6) return false;
            var result = MakeCheckResult(pokers);
            if (result.Map.ContainsValue(4) && !result.Map.ContainsValue(2)) return true;
            return false;
        }

        /// <summary>
        /// 是否是四个带两对
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsQuadplexWithTwoPair(IList<LandlordsCard> pokers)
        {
            if (pokers.Count != 8) return false;
            var result = MakeCheckResult(pokers);
            if (result.Map.ContainsValue(4) && result.Map.ContainsValue(2)) return true;
            return false;
        }

        /// <summary>
        /// 是否是炸弹
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsBomb(IList<LandlordsCard> pokers)
        {
            if(pokers.Count == 4)
            {
                var result = MakeCheckResult(pokers);
                return result.Map.ContainsValue(4);
            }
            return false;
        }

        /// <summary>
        /// 是否是火箭
        /// </summary>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public static bool IsRocket(IList<LandlordsCard> pokers)
        {
            if(pokers.Count == 2)
            {
                var result = MakeCheckResult(pokers);
                if (result.Map.ContainsKey(LandlordsCardNumber.Joker)) return result.Map[LandlordsCardNumber.Joker] == 2;
            }
            return false;
        }

    }

}

using System;

namespace Landlords
{
    /// <summary>
    /// 斗地主牌的点数, 调整了2的顺序
    /// </summary>
    public enum LandlordsCardNumber
    {
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
        Two,
        Joker
    }

    /// <summary>
    /// 斗地主不分花色, 所以这里的顺序无所谓
    /// </summary>
    public enum LandlordsCardSuits
    {
        Dianmond,
        Club,
        Heart,
        Spade,
        LittleJoker,
        BigJoker
    }

    /// <summary>
    /// 出牌的卡牌类型
    /// </summary>
    public enum LandlordsCardType
    {
        Unknown, // 未知
        Single, // 单张
        Pair,   // 一对
        Triplet, // 三张
        TripletWithSingle,  // 三带一
        TripletWithPair,    // 三带二
        Sequence, // 单顺, 五张或更多的连续单牌(如：45678 或 78910JQK); 不包括2和双鬼
        SequenceOfPairs,    // 双顺, 三对或更多的连续对牌(如：334455 、77 88 99 1010 JJ); 不包括2和双鬼
        SequenceOfTriplets, // 三顺, 二个或更多的连续三张牌(如：333444 、 555 666 777 888); 不包括2和双鬼 
        SequenceOfTripletsWithSingle,   // 三顺带单张
        SequenceOfTripletsWithPair,     // 三顺带一对
        QuadplexWithTwoSingle, // 四带两个单张
        QuadplexWithTwoPair, // 四带两对
        Bomb, // 炸弹
        Rocket, // 火箭
    }

    public class LandlordsCard :IComparable
    {
        public LandlordsCardNumber Number { get; private set; }
        public LandlordsCardSuits Suits { get; private set; }

        public LandlordsCard(LandlordsCardNumber number, LandlordsCardSuits suits) 
        {
            Number = number;
            Suits = suits;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Suits, Number);
        }

        /// <summary>
        /// 单张的比较顺序, 因为斗地主是不分花色的, 所以数字相等时的比较未实现
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) throw new NotImplementedException();
            var other = obj as LandlordsCard;
            if (other == null) throw new NotImplementedException();
            if (Number > other.Number) return 1;
            if (Number < other.Number) return -1;
            throw new NotImplementedException();
        }
    }
}
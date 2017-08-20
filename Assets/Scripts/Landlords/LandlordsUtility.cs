using System;
using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsUtility: IComparer<LandlordsCard>
    {
        /// <summary>
        /// 创建一副手牌
        /// </summary>
        /// <param name="hasJoker">是否包含Joker</param>
        /// <param name="isRandom">是否随机</param>
        /// <returns></returns>
        public static List<LandlordsCard> CreateDeck(bool hasJoker = true, bool isRandom = true)
        {
            var deck = new List<LandlordsCard>(54);
            for (var n = LandlordsCardNumber.Three; n <= LandlordsCardNumber.Two; n++)
            {
                for (var s = LandlordsCardSuits.Dianmond; s <= LandlordsCardSuits.Spade; s++)
                {
                    deck.Add(new LandlordsCard(n, s));
                }
            }
            if (hasJoker)
            {
                deck.Add(new LandlordsCard(LandlordsCardNumber.Joker, LandlordsCardSuits.LittleJoker));
                deck.Add(new LandlordsCard(LandlordsCardNumber.Joker, LandlordsCardSuits.BigJoker));
            }
            if (isRandom)
            {
                Random random = new Random();
                for (int index = deck.Count - 1; index > 0; index--)
                {
                    var temp = deck[index];
                    int randomIndex = random.Next(index);
                    deck[index] = deck[randomIndex];
                    deck[randomIndex] = temp;
                }
            }
            return deck;
        }

        /// <summary>
        /// 整理牌时的排序, 因为需要按花色整理, 所以新增了数字相同时的比较方式
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(LandlordsCard x, LandlordsCard y)
        {
            if (x.Number == y.Number)
            {
                if (x.Suits > y.Suits)
                {
                    return 1;
                }
                else if(x.Suits < y.Suits)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return x.CompareTo(y);
            }
        }


    }


}

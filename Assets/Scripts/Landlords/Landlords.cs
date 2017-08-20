using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsManager
    {
        /// <summary>
        /// 洗牌
        /// </summary>
        /// <param name="firstPlayer"></param>
        /// <param name="secondPlayer"></param>
        /// <param name="thirdPlayer"></param>
        /// <param name="remain">剩余的地主牌</param>
        public static void Shuffle(out List<LandlordsCard> firstPlayer, out List<LandlordsCard> secondPlayer, out List<LandlordsCard> thirdPlayer, out List<LandlordsCard> remain)
        {
            var deck = LandlordsUtility.CreateDeck();
            firstPlayer = new List<LandlordsCard>(17);
            for(int i = 0; i < 17; i++)
            {
                firstPlayer.Add(deck[i]);
            }
            secondPlayer = new List<LandlordsCard>(17);
            for(int i = 17; i < 34; i++)
            {
                secondPlayer.Add(deck[i]);
            }
            thirdPlayer = new List<LandlordsCard>(17);
            for(int i = 34; i < 51; i++)
            {
                thirdPlayer.Add(deck[i]);
            }
            remain = new List<LandlordsCard>(3);
            for (int i = 51; i < 54; i++)
            {
                remain.Add(deck[i]);
            }

        }
    }
}


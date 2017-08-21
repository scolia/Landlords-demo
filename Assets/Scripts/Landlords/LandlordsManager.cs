using System;
using System.Collections.Generic;

namespace Landlords
{
    public class LandlordsManager
    {
        public static LandlordsManager Instance { get; private set; }
        public LandlordsPalyer  FirstPlayer { get; private set; }
        public LandlordsPalyer SecondPlayer { get; private set; }
        public LandlordsPalyer ThirdPlayer { get; private set; }
        public List<LandlordsCard> Remain { get; private set; } // 剩余的地主牌

        private bool IsDecideLandlord;  // 是否已经决定了地主
        private IList<LandlordsCard> CurrentCard;   // 当出牌轮中, 已经大了的牌

        private LandlordsManager() { }
        static LandlordsManager()
        {
            Instance = new LandlordsManager();
        }

        /// <summary>
        /// 设置地主
        /// </summary>
        /// <param name="player"></param>
        public void SetLandlord(LandlordsPalyer player)
        {
            player.Status = LandlordsStatus.landlord;
            if (!IsDecideLandlord)
            {
                foreach (var item in Remain)
                {
                    player.Pokers.Add(item);
                }
                IsDecideLandlord = true;
            }
            
        }

        /// <summary>
        /// 出牌
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pokers"></param>
        /// <returns></returns>
        public bool PushCard(LandlordsPalyer player, IList<LandlordsCard> pokers)
        {
            if (CurrentCard == null)
            {
                CurrentCard = pokers;
                foreach (var item in pokers)
                {
                    player.Pokers.Remove(item);
                }
                return true;
            }
            else
            {
                try
                {
                    if (LandlordsCardComparer.MakeCompare(pokers, CurrentCard))
                    {
                        CurrentCard = pokers;
                        foreach (var item in pokers)
                        {
                            player.Pokers.Remove(item);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (NotImplementedException)
                {
                    return false;
                }
                
            }
        }

        /// <summary>
        /// 开始下一轮出牌
        /// </summary>
        public void NextRound()
        {
            CurrentCard = null;
        }

        /// <summary>
        /// 开始新一局游戏
        /// </summary>
        public void StartNewGame()
        {
            IsDecideLandlord = false;
            Shuffle();
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        private void Shuffle()
        {
            var deck = LandlordsUtility.CreateDeck();
            FirstPlayer.Pokers = new List<LandlordsCard>(17);
            for(int i = 0; i < 17; i++)
            {
                FirstPlayer.Pokers.Add(deck[i]);
            }
            SecondPlayer.Pokers = new List<LandlordsCard>(17);
            for(int i = 17; i < 34; i++)
            {
                SecondPlayer.Pokers.Add(deck[i]);
            }
            ThirdPlayer.Pokers = new List<LandlordsCard>(17);
            for(int i = 34; i < 51; i++)
            {
                ThirdPlayer.Pokers.Add(deck[i]);
            }
            Remain = new List<LandlordsCard>(3);
            for (int i = 51; i < 54; i++)
            {
                Remain.Add(deck[i]);
            }

        }
    }
}


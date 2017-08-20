using UnityEngine;
using Landlords;
using System;
using System.Reflection;


[Serializable]
public class SameSuitsCardSprite
{
    public Sprite Three;
    public Sprite Four;
    public Sprite Five;
    public Sprite Six;
    public Sprite Seven;
    public Sprite Eight;
    public Sprite Nine;
    public Sprite Ten;
    public Sprite Jack;
    public Sprite Queen;
    public Sprite King;
    public Sprite Ace;
    public Sprite Two;

}

[Serializable]
public class JokerSprite
{
    public Sprite LittleJoker;
    public Sprite BigJoker;
}

[Serializable]
public class CardBackSprite
{
    public Sprite Red;
    public Sprite Blue;
}


public class PokerSpriteManager : MonoBehaviour {

    public SameSuitsCardSprite Spade;
    public SameSuitsCardSprite Heart;
    public SameSuitsCardSprite Club;
    public SameSuitsCardSprite Dianmond;
    public JokerSprite Joker;
    public CardBackSprite CardBack;

    public static PokerSpriteManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 获取对应的精灵
    /// </summary>
    /// <param name="suits"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public Sprite GetPokerSprite(LandlordsCardSuits suits, LandlordsCardNumber number)
    {
        if (number == LandlordsCardNumber.Joker && suits == LandlordsCardSuits.LittleJoker)
        {
            return Joker.LittleJoker;
        }
        else if (number == LandlordsCardNumber.Joker && suits == LandlordsCardSuits.BigJoker)
        {
            return Joker.BigJoker;
        }
        else
        {
            Type t = typeof(SameSuitsCardSprite);
            FieldInfo fld = t.GetField(number.ToString());
            if (suits == LandlordsCardSuits.Spade)
            {
                return (Sprite)fld.GetValue(Spade);
            }
            else if (suits == LandlordsCardSuits.Heart)
            {
                return (Sprite)fld.GetValue(Heart);
            }
            else if (suits == LandlordsCardSuits.Club)
            {
                return (Sprite)fld.GetValue(Club);
            }
            else
            {
                return (Sprite)fld.GetValue(Dianmond);
            }
        }
    }

}

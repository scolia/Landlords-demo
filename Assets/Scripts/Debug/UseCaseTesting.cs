using System.Collections.Generic;
using Landlords;
using UnityEngine;

public class UseCaseTesting : MonoBehaviour {

    public static readonly List<LandlordsCard> Roket = new List<LandlordsCard>
    { 
        new LandlordsCard(LandlordsCardNumber.Joker, LandlordsCardSuits.LittleJoker),
        new LandlordsCard(LandlordsCardNumber.Joker, LandlordsCardSuits.BigJoker),
    };

    private void Start()
    {
        TestSingle();
        TestPair();
    }

    /// <summary>
    /// 单牌测试
    /// </summary>
    public static void TestSingle()
    {
        var singleAce = new List<LandlordsCard> { new LandlordsCard(LandlordsCardNumber.Ace, LandlordsCardSuits.Club) };
        var singleTwo = new List<LandlordsCard> { new LandlordsCard(LandlordsCardNumber.Two, LandlordsCardSuits.Club) };
        var singleJoker = new List<LandlordsCard> { new LandlordsCard(LandlordsCardNumber.Joker, LandlordsCardSuits.LittleJoker) };
        var singleNine = new List<LandlordsCard> { new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Club) };
        var bomb = new List<LandlordsCard>
        {
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Club),
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Dianmond),
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Heart),
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Spade),
        };
        Debug.Assert(LandlordsCardComparer.MakeCompare(singleTwo, singleAce));
        Debug.Assert(LandlordsCardComparer.MakeCompare(singleJoker, singleTwo));
        Debug.Assert(LandlordsCardComparer.MakeCompare(singleAce, singleNine));
        Debug.Assert(LandlordsCardComparer.MakeCompare(bomb, singleJoker));
        Debug.Assert(LandlordsCardComparer.MakeCompare(Roket, singleTwo));
        Debug.Log("单牌比较测试通过");
    }

    public static void TestPair()
    {
        var pairAce = new List<LandlordsCard> {
            new LandlordsCard(LandlordsCardNumber.Ace, LandlordsCardSuits.Club),
            new LandlordsCard(LandlordsCardNumber.Ace, LandlordsCardSuits.Dianmond),

        };
        var pairTwo = new List<LandlordsCard>
        {
            new LandlordsCard(LandlordsCardNumber.Two, LandlordsCardSuits.Club),
            new LandlordsCard(LandlordsCardNumber.Two, LandlordsCardSuits.Dianmond),
        };
        var pairNine = new List<LandlordsCard>
        {
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Club),
            new LandlordsCard(LandlordsCardNumber.Nine, LandlordsCardSuits.Dianmond),
        };
        var bomb = new List<LandlordsCard>
        {
            new LandlordsCard(LandlordsCardNumber.Eight, LandlordsCardSuits.Club),
            new LandlordsCard(LandlordsCardNumber.Eight, LandlordsCardSuits.Dianmond),
            new LandlordsCard(LandlordsCardNumber.Eight, LandlordsCardSuits.Heart),
            new LandlordsCard(LandlordsCardNumber.Eight, LandlordsCardSuits.Spade),
        };
        Debug.Assert(LandlordsCardComparer.MakeCompare(pairTwo, pairAce));
        Debug.Assert(LandlordsCardComparer.MakeCompare(pairAce, pairNine));
        Debug.Assert(LandlordsCardComparer.MakeCompare(bomb, pairTwo));
        Debug.Assert(LandlordsCardComparer.MakeCompare(Roket, pairTwo));
        Debug.Log("对牌比较测试通过");
    }


}

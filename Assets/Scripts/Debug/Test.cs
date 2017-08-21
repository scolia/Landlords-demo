using UnityEngine;
using Landlords;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {

    public GameObject CreatePoint;

    public List<TestCard> cards = new List<TestCard>();

    public static Test Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        var deck = LandlordsUtility.CreateDeck(true, false);
        foreach(var item in deck)
        {
            var temp = new GameObject(item.ToString());
            temp.transform.SetParent(CreatePoint.transform);
            temp.AddComponent<Image>().sprite = PokerSpriteManager.Instance.GetPokerSprite(item.Suits, item.Number);
            temp.AddComponent<TestCard>().card = item;
        }
	}
	
	public void GetCardType()
    {
        var c = new List<LandlordsCard>();
        foreach(var item in cards)
        {
            c.Add(item.card);
        }
        try
        {
            Debug.Log(LandlordsCardChecker.GetCardType(c));
        }
        catch(ArgumentException)
        {
            Debug.Log("no card");
        }
        
    }

    public void Clear()
    {
        foreach(var item in cards)
        {
            item.GetComponent<Image>().color = Color.white;
        }
        cards.Clear();
    }
}

using System.Collections.Generic;
using UnityEngine;
using Landlords;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<LandlordsCard> firstPlay;
        List<LandlordsCard> secondPaly;
        List<LandlordsCard> thirdPlay;
        List<LandlordsCard> remain;
        LandlordsManager.Shuffle(out firstPlay, out secondPaly, out thirdPlay, out remain);
        var helper = new LandlordsUtility();
        firstPlay.Sort(helper);
        firstPlay.Reverse();
        var parent = GameObject.FindWithTag("FirstPlayer");
        foreach (var card in firstPlay)
        {
            var sp = PokerSpriteManager.Instance.GetPokerSprite(card.Suits, card.Number);
            var poker = new GameObject(card.ToString());
            poker.AddComponent<RectTransform>();
            poker.AddComponent<Image>().sprite = sp;
            poker.transform.SetParent(parent.transform);
        }

    }
	
}

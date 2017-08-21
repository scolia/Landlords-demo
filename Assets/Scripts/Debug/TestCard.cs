using System;
using Landlords;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestCard : MonoBehaviour, IPointerClickHandler
{

    public LandlordsCard card;
    public Image image;
    bool IsClick;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClick)
        {
            image.color = Color.red;
            Test.Instance.cards.Add(this);
            IsClick = true;
        }
        else
        {
            image.color = Color.white;
            Test.Instance.cards.Remove(this);
            IsClick = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New CardBoundleData", menuName = "Card Bundle Data", order = 51)]
public class CardBoundleData : ScriptableObject
{
    [SerializeField]
    private int _columns;

    [SerializeField]
    private int _strings;

    [SerializeField]
    private CardData[] _cardData;

    public int columns => _columns;
    public int strings => _strings;

    public CardData[] CardData => _cardData;
}

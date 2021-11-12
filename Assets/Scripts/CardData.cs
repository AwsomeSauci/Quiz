using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CardData
{
    [SerializeField]
    private string _identifer;

    [SerializeField]
    private Sprite _sprite;

    public string Identifer => _identifer;

    public Sprite Sprite => _sprite;

}

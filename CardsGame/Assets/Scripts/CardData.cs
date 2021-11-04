using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField]
    private string _identifier; //идентификатор карточки

    [SerializeField]
    private Sprite _sprite; //Sprite карточки

    public string Identifier => _identifier;

    public Sprite Sprite => _sprite;
}

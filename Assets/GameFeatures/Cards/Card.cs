using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Healing,
    Damage,
    Reverse,
    Blank
}

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 1)]

public class Card : ScriptableObject
{
    [SerializeField] CardType _cardType;

    [SerializeField] string _cardName;
    [SerializeField] Sprite _cardImage;
    [SerializeField] string _description;

    [SerializeField] private List<BaseEffect> cardEffects;
    public CardType CardType { get => _cardType; set => _cardType = value; }
    public string CardName { get => _cardName; set => _cardName = value; }
    public Sprite CardImage { get => _cardImage; set => _cardImage = value; }
    public string Description { get => _description; set => _description = value; }

    public void PlayCard(HitEventArgs? h = null)
    {
        foreach (BaseEffect effect in cardEffects)
        {
            if (h != null)
            {
                if (h.Result == HitResult.Perfect)
                {
                    if (effect is InstantHealing)
                    {
                        InstantHealing i = (InstantHealing)Instantiate(effect);
                        i.healing *= 1.2f;
                        Ball.Instance.AddEffect(i);
                    }
                    else if (effect is InstantDamage)
                    {
                        InstantDamage i = (InstantDamage)Instantiate(effect);
                        i.damage *= 1.2f;
                        Ball.Instance.AddEffect(i);
                    }
                    else
                    {
                        Ball.Instance.AddEffect(Instantiate(effect));
                    }
                    
                }
                else if (h.Result == HitResult.Bad)
                {
                    if (effect is InstantHealing)
                    {
                        InstantHealing i = (InstantHealing)Instantiate(effect);
                        i.healing *= 0.8f;
                        Ball.Instance.AddEffect(i);
                    }
                    else if (effect is InstantDamage)
                    {
                        InstantDamage i = (InstantDamage)Instantiate(effect);
                        i.damage *= 0.8f;
                        Ball.Instance.AddEffect(i);
                    }
                    else
                    {
                        Ball.Instance.AddEffect(Instantiate(effect));
                    }
                }
                else
                {
                    Ball.Instance.AddEffect(Instantiate(effect));
                }
            }
            else
            {
                Ball.Instance.AddEffect(Instantiate(effect));
            }
        }
    }
}

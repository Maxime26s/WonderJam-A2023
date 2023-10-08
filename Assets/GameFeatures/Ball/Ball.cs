using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    public List<BaseEffect> BaseEffectsForTesting = new List<BaseEffect>();
    public List<BaseEffect> effects = new List<BaseEffect>();
    [SerializeField]
    public int baseActionPoints = 4;
    [SerializeField]
    public int actionPoints = 4;

    [SerializeField]
    public TextMeshProUGUI ActionLabel;

    public float pendingDamage = 0f;
    public float pendingHealing = 0f;


    private void Start()
    {
        BeatController.Instance.OnBeatEvent += Tick;
        foreach (BaseEffect effect in BaseEffectsForTesting) 
        {
            effects.Add(Instantiate(effect));
        }
    }

    public void AddEffect(BaseEffect effect)
    {
        effects.Add(effect);
    }


    public void ResetActions()
    {
        actionPoints = baseActionPoints;
        ActionLabel.text = actionPoints.ToString();
    }

    void Tick()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            actionPoints--;
            if (actionPoints <= 0)
            {
                StartCoroutine(GameManager.Instance.TurnOver());
            }
            ActionLabel.text = Mathf.Max(actionPoints, 0).ToString();

            foreach (BaseEffect e in effects)
            {
                if ((TickDamage) e)
                e.Tick();
            }

            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(pendingDamage);
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(pendingHealing);

            // Delete all effects that are over
            effects.RemoveAll(e => e.isOver);
        }
    }

    public void AddActionPoints(int n) => actionPoints += n;
    public void RemoveActionPoints(int n) => actionPoints -= n;
}

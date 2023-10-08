using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    public List<BaseEffect> BaseEffectsForTesting;
    public List<BaseEffect> effects;
    [SerializeField]
    public int baseActionPoints = 4;
    [SerializeField]
    public int actionPoints = 4;

    [SerializeField]
    public TextMeshProUGUI ActionLabel;


    private void Start()
    {
        BeatController.Instance.OnBeatEvent += Tick;
        foreach (var e in BaseEffectsForTesting) 
        {
            effects.Add(Instantiate(e));
        }
    }

    public void AddEffect(BaseEffect e)
    {
        effects.Add(e);
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
            print("ball");
            actionPoints--;
            if (actionPoints <= 0)
            {
                StartCoroutine(GameManager.Instance.TurnOver());
            }
            ActionLabel.text = Mathf.Max(actionPoints, 0).ToString();

            foreach (BaseEffect e in effects)
            {
                e.Tick();
            }

            // Delete all effects that are over
            effects.RemoveAll(e => e.isOver);
        }
    }

    public void AddActionPoints(int n) => actionPoints += n;
    public void RemoveActionPoints(int n) => actionPoints -= n;
}

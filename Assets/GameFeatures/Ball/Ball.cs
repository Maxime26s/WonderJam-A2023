using System.Collections;
using System.Collections.Generic;
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
    public GameObject ActionBoard;


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

    void RenderActionPoint()
    {
        Transform grid = ActionBoard.transform.GetChild(1);
        int i = 0;
        foreach (Transform child in grid)
        {
            child.gameObject.SetActive(i < actionPoints);
            i++;
        }
    }

    void Tick()
    {
        actionPoints--;

        if (actionPoints <= 0) 
        {
            GameManager.Instance.TurnOver();
            actionPoints = baseActionPoints;
        }

        RenderActionPoint();

        foreach(BaseEffect e in effects)
        {
            e.Tick();
        }

        // Delete all effects that are over
        effects.RemoveAll(e => e.isOver);
    }

    public void AddActionPoints(int n) => actionPoints += n;
    public void RemoveActionPoints(int n) => actionPoints -= n;
}

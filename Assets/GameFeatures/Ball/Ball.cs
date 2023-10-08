using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    public List<BaseEffect> effects = new List<BaseEffect>();
    [SerializeField]
    public int baseActionPoints = 4;
    [SerializeField]
    public int actionPoints = 4;

    [SerializeField]
    public TextMeshProUGUI ActionLabel;
    [SerializeField]
    public List<EffectsInfoUI> EffectsListUI = new List<EffectsInfoUI>();

    public float pendingDamage = 0f;
    public float pendingHealing = 0f;
    public float damageMultiplier = 1f;
    public float healingMultiplier = 1f;

    [SerializeField] private EffectsInfoUI _effectUIPrefab = null;
    [SerializeField] private Transform _effectUIParent = null;

    private void Start()
    {
        BeatController.Instance.OnBeatEvent += Tick;

        //BeatController.Instance.EarlyOnBeatEvent += UpdateEffectsList;
    }

    private void FixedUpdate()
    {
        UpdateEffectsList();
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
            pendingDamage = 0f;
            pendingHealing = 0f;

            actionPoints--;
            if (actionPoints <= 0)
            {
                StartCoroutine(GameManager.Instance.TurnOver());
            }
            ActionLabel.text = Mathf.Max(actionPoints, 0).ToString();

            foreach (BaseEffect effect in effects)
            {
                effect.Tick();
            }

            //Take idle damage
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(1);

            //Effects
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().TakeDamage(pendingDamage * damageMultiplier);
            PlayerManager.Instance.PlayerManagerData.GetCurrentPlayer().ReceiveHealing(pendingHealing * healingMultiplier);

            // Delete all effects that are over
            effects.RemoveAll(effect => effect.isOver);
        }

    }

    private void DestroyAndClearEffectListUIs()
    {
        foreach (EffectsInfoUI effectUI in EffectsListUI)
        {
            Destroy(effectUI.gameObject);
        }
        EffectsListUI.Clear();
    }

    void UpdateEffectsList()
    {
        DestroyAndClearEffectListUIs();

        foreach (BaseEffect effect in effects)
        {
            EffectsInfoUI effectInfoUI = (Instantiate(_effectUIPrefab, _effectUIParent));
            effectInfoUI.SetInfo(effect.GetInfo());
            EffectsListUI.Add(effectInfoUI);
        }
    }

    public void AddActionPoints(int n) => actionPoints += n;
    public void RemoveActionPoints(int n) => actionPoints -= n;
}

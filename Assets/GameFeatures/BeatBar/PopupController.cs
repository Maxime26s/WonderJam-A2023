using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject textPrefab;
    public BeatBar beatBar;

    private void Start()
    {
        beatBar.OnHitEvent += ShowPopup;
    }
    private void OnDestroy()
    {
        beatBar.OnHitEvent -= ShowPopup;
    }

    public void ShowPopup(object src, HitEventArgs args)
    {
        var go = Instantiate(textPrefab, transform);
        var text = go.GetComponent<TextMeshProUGUI>();
        text.text = args.Result.ToString() + "!";
        switch (args.Result)
        {
            case HitResult.Miss:
                text.color = Color.red;
                break;
            case HitResult.Bad:
                text.color = Color.magenta;
                break;
            case HitResult.Good:
                text.color = Color.green;
                break;
            case HitResult.Perfect:
                text.color = Color.cyan;
                break;
        }
        text.gameObject.SetActive(true);
        Destroy(go, 1.0f);
    }
}

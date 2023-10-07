using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRays : MonoBehaviour
{
    public List<GameObject> foregroundList;
    public List<GameObject> backgroundList;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < foregroundList.Count; i++)
        {
            foregroundList[i].GetComponent<SpriteRenderer>().color = HSVToRGB(90 * i, 1f, 1f, 0.25f);
            backgroundList[i].GetComponent<SpriteRenderer>().color = HSVToRGB(90 * i, 1f, 1f, 0.125f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Color HSVToRGB(int h, float s, float v, float alpha = 1f)
    {
        // Clamp and normalize the H value.
        float realHue = (h % 360) / 360f;

        // Convert to RGB using Unity's built-in method.
        Color color = Color.HSVToRGB(realHue, s, v);

        // Set the alpha value.
        color.a = Mathf.Clamp01(alpha);

        return color;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinningRays : MonoBehaviour
{
    public GameObject foreground;
    public GameObject background;
    public List<GameObject> foregroundList;
    public List<GameObject> backgroundList;
    public float foregroundSpeed = 1f;
    public float backgroundSpeed = 0.75f;
    public float foregroundAlpha = 0.125f;
    public float backgroundAlpha = 0.0625f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < foregroundList.Count; i++)
        {
            foregroundList[i].GetComponent<SpriteRenderer>().color = HSVToRGB(90 * i, 1f, 1f, foregroundAlpha);
            backgroundList[i].GetComponent<SpriteRenderer>().color = HSVToRGB(90 * (foregroundList.Count - i - 1), 1f, 1f, backgroundAlpha);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreground.transform.Rotate(0, 0, -foregroundSpeed * Time.deltaTime);
        background.transform.Rotate(0, 0, -backgroundSpeed * Time.deltaTime);

        for (int i = 0; i < foregroundList.Count; i++)
        {
            var fgSr = foregroundList[i].GetComponent<SpriteRenderer>();
            fgSr.color = HSVToRGB(90 * i + (int)foreground.transform.rotation.eulerAngles.z, 1f, 1f, fgSr.color.a);

            var bgSr = backgroundList[i].GetComponent<SpriteRenderer>();
            bgSr.color = HSVToRGB(90 * (foregroundList.Count - i - 1) + (int)background.transform.rotation.eulerAngles.z, 1f, 1f, bgSr.color.a);

        }
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

    public int GetHueFromRGB(Color color)
    {
        Color.RGBToHSV(color, out float h, out _, out _);

        return (int)(h * 360f) % 360;
    }
}

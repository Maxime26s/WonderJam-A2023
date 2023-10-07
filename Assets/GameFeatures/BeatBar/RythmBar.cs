using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmBar : MonoBehaviour
{
    public Transform spawnTransform;
    public Transform centerTransform;

    public GameObject beatPrefab;

    private Vector2 startPos;
    private Vector2 centerPos;

    private SineFunction sineFunction;


    // Start is called before the first frame update
    void Start()
    {
        startPos = spawnTransform.position;
        centerPos = centerTransform.position;
        sineFunction = ExtractSineFunction(startPos, centerPos);

        IEnumerator enumerator()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                var go = Instantiate(beatPrefab, spawnTransform.position, Quaternion.identity, null);
                go.GetComponent<Beat>().Init(sineFunction);
            }
        }

        StartCoroutine(enumerator());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static SineFunction ExtractSineFunction(Vector2 start, Vector2 center)
    {
        float A = Mathf.Abs(center.y - start.y);
        float P = 4 * RhythmController.Instance.beatInterval;
        float f = 1 / P;

        return (t) =>
        {
            Vector2 position = Vector2.zero;

            position.x = start.x + (center.x - start.x) / RhythmController.Instance.beatInterval * t;
            position.y = -A * Mathf.Sin(2 * Mathf.PI * f * t);

            return position;
        };
    }
}

public delegate Vector2 SineFunction(float x);

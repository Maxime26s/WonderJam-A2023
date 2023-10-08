using UnityEngine;
using UnityEngine.UI;

public class MenuParallax : MonoBehaviour
{
    public Image backgroundImage; 
    public float moveFactorX = 0.05f;
    public float moveFactorY = 0.05f; 

    private Vector2 originalPosition; 

    void Start()
    {
        originalPosition = backgroundImage.rectTransform.anchoredPosition;
    }

    void Update()
    {
        float deltaX = (Input.mousePosition.x / Screen.width - 0.5f) * moveFactorX * Screen.width;
        float deltaY = (Input.mousePosition.y / Screen.height - 0.5f) * moveFactorY * Screen.height;
        backgroundImage.rectTransform.anchoredPosition = originalPosition + new Vector2(deltaX, deltaY);
    }
}
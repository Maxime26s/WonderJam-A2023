using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInHandUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public TextMeshProUGUI _cardName = null;
    [SerializeField] public Image _cardImage = null;
    [SerializeField] public TextMeshProUGUI _cardDescription = null;

    [SerializeField] private GameObject _cardOutline = null;

    [SerializeField] private Transform _cardVisualParent = null;

    public Transform startingPosition;
    public Vector3 raisedPositionOffset = new Vector3(0, 1, 0);
    public float lerpSpeed = 5.0f; // Speed of lerp movement

    private Vector3 targetPosition;
    private bool isHovering = false;

    public void SetupCardUI(Card card)
    {
        _cardName.text = card.CardName;
        _cardImage.sprite = card.CardImage;
        _cardDescription.text = card.Description;
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isHovering)
        {
            targetPosition = startingPosition.position + raisedPositionOffset;
        }
        else
        {
            targetPosition = startingPosition.position;
        }

        _cardVisualParent.position = Vector3.Lerp(_cardVisualParent.position, targetPosition, lerpSpeed * Time.deltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        _cardOutline.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        _cardOutline.SetActive(false);
    }
}
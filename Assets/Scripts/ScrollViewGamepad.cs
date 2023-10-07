using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollViewGamepad : MonoBehaviour
{
    
	[SerializeField]
	public float scrollSpeed = 1000f;
	[SerializeField]
	public RectTransform contentRectTransform;
	private InputAction scrollEvent;
	private ControllerActions controllerActions;

    private void Awake()
    {
		controllerActions = new ControllerActions();
		scrollEvent = controllerActions.Gameplay.Scroll;
    }

    private void OnEnable()
    {
        scrollEvent.Enable();
    }

    private void OnDisable()
    {
        scrollEvent.Disable();
    }

    private void Update()
    {
		float scrollValue = scrollEvent.ReadValue<float>();
		contentRectTransform.position += new Vector3(0, -(scrollValue * scrollSpeed * Time.deltaTime), 0);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class DeckChecker : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Scroll"].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions["Scroll"].Disable();
    }

    private void Update()
    {
        float scrollUpValue = playerInput.actions["ScrollUp"].ReadValue<float>();
        float scrollDownValue = playerInput.actions["ScrollDown"].ReadValue<float>();

        if (scrollUpValue > 0.5f)
        {
            // Scroll up logic
            Debug.Log("Scroll Up");
        }
        else if (scrollDownValue > 0.5f)
        {
            // Scroll down logic
            Debug.Log("Scroll Down");
        }
    }
}
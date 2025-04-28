using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeControllerScript : MonoBehaviour
{
    private InputAction swipeAction;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    [SerializeField] private float swipeThreshold = 50f; 
    private PlayerRunnerScript playerRunnerScript;

    private void Awake()
    {
        // Carrega o Input Action Asset
        var inputActionAsset = Resources.Load<InputActionAsset>("PlayerInput");
        swipeAction = inputActionAsset.FindAction("Swipe");
        playerRunnerScript = this.GetComponent<PlayerRunnerScript>();
    }

    private void OnEnable()
    {
        swipeAction.Enable();
    }

    private void OnDisable()
    {
        swipeAction.Disable();
    }

    private void Update()
    {
        //codigo para pegar o touch inicial e final do usuario e transformar em Vector2
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (!isSwiping)
            {
                startTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                isSwiping = true;
            }
        }
        else
        {
            if (isSwiping)
            {
                endTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    DetectSwipeDirection(swipeDelta);
                }

                isSwiping = false;
            }
        }
    }

    private void DetectSwipeDirection(Vector2 swipeDelta)
    {
        //usa Vector2 para indicar em qual direçao jogador arrastou na tela
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                //Debug.Log("Swiped Right");
                playerRunnerScript.ChangeLane(-1.5f);
            }
            else
            {
                //Debug.Log("Swiped Left");
                playerRunnerScript.ChangeLane(1.5f);
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                //Debug.Log("Swiped Up");
                playerRunnerScript.Jump();
            }
            else
            {
                //Debug.Log("Swiped Down");
                playerRunnerScript.Slide();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button_Script : MonoBehaviour
{
    [SerializeField] private Moving_Platform _movingPlatform;
    [SerializeField] private float _delayBetweenMovement = 2f;

    private bool _isPressed = false;

    public UnityEvent OnButtonPress;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PressButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ReleaseButton();
        }
    }

    private void PressButton()
    {
        if(! _isPressed)
        {
            _isPressed = true;
            _movingPlatform.MoveToNextWaypoint();
            OnButtonPress.Invoke();
            Invoke("MoveBack", _delayBetweenMovement);
        }
    }

    private void ReleaseButton()
    {
        _isPressed = false;
    }

    private void MoveBack()
    {
        _movingPlatform.MoveBackToStart();
        _isPressed = false;
    }
}

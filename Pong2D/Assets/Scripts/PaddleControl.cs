using UnityEngine;
using UnityEngine.Events;

public class PaddleControl : MonoBehaviour
{
    [SerializeField]
    UnityEvent onGetLifePowerUp;

    Rigidbody2D thisRigidbody;
    Vector2 swipeDeltaPos;

#region MonoBehavior

    void Awake()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
#if !UNITY_EDITOR
        // Touch controls
        // Mirror any swiping movements on the lower part of the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && IsInputOnLowerPartOfScreen(touch.position))
            {
                swipeDeltaPos += touch.deltaPosition;
            }
        }
#endif
    }

    void FixedUpdate()
    {
        Vector3 newPos = transform.position;
#if UNITY_EDITOR
        // Editor / PC controls
        // Follow the mouse x position if it is on the lower part of the screen
        if (IsInputOnLowerPartOfScreen(Input.mousePosition))
        {
            newPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
#else
        // Touch controls      
        newPos.x += ConvertTouchDeltaToWorldSpace(swipeDeltaPos).x;
        swipeDeltaPos = Vector2.zero;
#endif
        thisRigidbody.MovePosition(newPos); // Note: Behavior may work differently in 3D
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            PowerUpType powerUpType = other.GetComponent<PowerUp>().Type;
            ProcessPowerUp(powerUpType);
        }
    }

#endregion // MonoBehaviour

#region Helper Methods

    void ProcessPowerUp(PowerUpType powerUpType)
    {
        switch(powerUpType)
        {
            case PowerUpType.AddLife:
                onGetLifePowerUp.Invoke();
                break;
            default:
                break;
        }
    }

    bool IsInputOnLowerPartOfScreen(Vector2 inputPos)
    {
        // Return true if input is within the lower 40% of the screen
        Vector2 viewportPos = Camera.main.ScreenToViewportPoint(inputPos);
        return viewportPos.y <= 0.4f;
    }

    Vector2 ConvertTouchDeltaToWorldSpace(Vector2 touchDeltaPos)
    {
        // Return true if input is within the lower 40% of the screen
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(swipeDeltaPos); 
        Vector2 worldPos0 = Camera.main.ScreenToWorldPoint(Vector2.zero); 
        return worldPos - worldPos0;
    }

#endregion // Helper Methods

}

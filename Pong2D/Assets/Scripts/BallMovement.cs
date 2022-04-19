using UnityEngine;
using UnityEngine.Events;

public class BallMovement : MonoBehaviour
{
    [SerializeField] [Range(5f, 20f)]
    float speed = 10f;
    [SerializeField]
    Transform parentWhenInactive;
    [SerializeField]
    UnityEvent onBallDropped;

    Rigidbody2D thisRigidbody;
    bool isActive = false;          // Set to true when ball is moving, false when it is still coasting on top of the paddle
    bool isInputStarted = false;    // Set to true when the user touches the lower half of the screen

#region MonoBehavior

    void Awake()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (parentWhenInactive == null)
        {
            parentWhenInactive = transform.parent;
        }
    }

    void Start()
    {
        Deactivate();
    }

    void Update()
    {
        // If ball is not yet active, check for user input
        // TODO: Consider if input code should be centralized somewhere else instead
        if (!isActive)
        {
            // Activate the ball on mouse press and release on the lower half of the screen
#if UNITY_EDITOR  
            // Editor / PC controls
            if (Input.GetMouseButtonDown(0))
            {
                isInputStarted = IsInputOnLowerPartOfScreen(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isInputStarted && IsInputOnLowerPartOfScreen(Input.mousePosition))
                {
                    Activate();
                }
                isInputStarted = false;
            }
#else
            // Touch controls
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    isInputStarted = IsInputOnLowerPartOfScreen(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (isInputStarted && IsInputOnLowerPartOfScreen(touch.position))
                    {
                        Activate();
                    }
                    isInputStarted = false;
                }
            }
#endif
        }
    }

    void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        // Ball can naturally gain/lose speed when getting stuck between other rigidbodies
        // Prevent this by regularly correcting the speed to a constant value
        thisRigidbody.velocity = thisRigidbody.velocity.normalized * speed;

        // TODO: Adjust movement angle to avoid getting stuck perpetually bouncing horizontally/verticlaly
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onBallDropped.Invoke();
            Deactivate();
        }
    }

#endregion // MonoBehaviour

#region  Helper Methods

    void Activate()
    {
        transform.SetParent(null);
        thisRigidbody.simulated = true;
        isActive = true;
        // Start moving the ball by assigning an upward velocity
        this.thisRigidbody.velocity = Vector2.up * speed;
    }

    void Deactivate()
    {
        thisRigidbody.velocity = Vector2.zero;
        thisRigidbody.angularVelocity = 0f;
        thisRigidbody.simulated = false;
        transform.position = parentWhenInactive.position;
        transform.SetParent(parentWhenInactive);
        isActive = false;
    }

    bool IsInputOnLowerPartOfScreen(Vector2 inputPos)
    {
        // Return true if input is within the lower 40% of the screen
        Vector2 viewportPos = Camera.main.ScreenToViewportPoint(inputPos);
        return viewportPos.y <= 0.4f;
    }

#endregion // Helper Methods

}

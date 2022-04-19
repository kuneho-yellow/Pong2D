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
    bool startMovementFlag = false;

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
        // If ball is not yet active, check user input
        // TODO: Consider if input code should be centralized somewhere else instead
        if (!isActive)
        {
            // Editor / PC controls
            // Activate the ball on mouse release on the lower half of the screen
            // TODO: Implement the "lower half" part
            if (Input.GetMouseButtonDown(0))
            {
                isInputStarted = true;
            }
            if (Input.GetMouseButtonUp(0) && isInputStarted)
            {
                isInputStarted = false;
                Activate();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        // Start moving the ball by assigning an upward velocity
        if (startMovementFlag)
        {
            this.thisRigidbody.velocity = Vector2.up * speed;
            startMovementFlag = false;
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
        startMovementFlag = true;
        isActive = true;
    }

    void Deactivate()
    {
        thisRigidbody.velocity = Vector2.zero;
        thisRigidbody.angularVelocity = 0f;
        thisRigidbody.simulated = false;
        transform.position = parentWhenInactive.position;
        transform.SetParent(parentWhenInactive);
        startMovementFlag = false;
        isActive = false;
    }

#endregion // Helper Methods

}

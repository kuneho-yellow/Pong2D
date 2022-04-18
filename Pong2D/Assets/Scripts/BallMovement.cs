using UnityEngine;
using UnityEngine.Events;

public class BallMovement : MonoBehaviour
{
    [SerializeField] [Range(5f, 20f)]
    float startingSpeed = 10f;
    [SerializeField]
    Transform parentWhenInactive;
    [SerializeField]
    UnityEvent onBallDropped;

    Rigidbody2D thisRigidbody;
    bool isActive = false;          // Set to true when ball is moving, false when it is still coasting on top of the paddle
    bool isInputStarted = false;    // Set to true when the user touches the lower half of the screen
    bool addForceFlag = false;

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

    // Update is called once per frame
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
        // Add a force to make the ball move
        if (addForceFlag)
        {
            float forceToAdd = startingSpeed * thisRigidbody.mass / Time.fixedDeltaTime;
            thisRigidbody.AddForce(Vector2.up * forceToAdd); // TODO: Control the angle through some mechanic
            addForceFlag = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onBallDropped.Invoke();
            Deactivate();
        }
    }

    void Activate()
    {
        transform.SetParent(null);
        thisRigidbody.simulated = true;
        addForceFlag = true;
        isActive = true;
    }

    void Deactivate()
    {
        thisRigidbody.velocity = Vector2.zero;
        thisRigidbody.angularVelocity = 0f;
        thisRigidbody.simulated = false;
        transform.position = parentWhenInactive.position;
        transform.SetParent(parentWhenInactive);
        addForceFlag = false;
        isActive = false;
    }

#endregion // MonoBehaviour

#region  Helper Methods

#endregion // Helper Methods

}

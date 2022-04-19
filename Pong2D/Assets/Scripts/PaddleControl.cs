using UnityEngine;
using UnityEngine.Events;

public class PaddleControl : MonoBehaviour
{
    [SerializeField]
    UnityEvent onGetLifePowerUp;

    Rigidbody2D thisRigidbody;

#region MonoBehavior

    void Awake()
    {
        thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Editor / PC controls
        // Follow the mouse x position
        Vector3 newPos = transform.position;
        newPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
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

#endregion // Helper Methods

}

using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    private Rigidbody2D thisRigidbody;

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
        // TODO: PowerUps
    }

#endregion // MonoBehaviour
}

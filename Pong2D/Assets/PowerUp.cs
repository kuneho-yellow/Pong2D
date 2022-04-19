using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType Type { get; private set; }
    [SerializeField]
    PowerUpType type = PowerUpType.AddLife;
    [SerializeField] [Range(1, 20)]
    float dropSpeed = 5;

    Rigidbody2D thisRigidbody;

    void Awake()
    {
        Type = type;
        thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move downwards at constant speed
        this.transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Move downwards at constant speed
        thisRigidbody.MovePosition(Vector2.down * dropSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Deactivate when touching the paddle of ground
        if (other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}


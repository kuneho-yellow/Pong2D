using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    [SerializeField] [Range(1, 3)]
    int startingHP = 1;
    [SerializeField]
    Color[] hitPointColors = new Color[3]; // TODO: Better Inspector GUI
    [SerializeField]
    UnityEvent onBrickDestroyed;
    [SerializeField]
    PowerUp powerUp;

    int curHP = 0;
    SpriteRenderer thisSpriteRenderer;

#region MonoBehavior

    void Awake()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ResetHP();
        ResetPowerUp();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ReduceHP();
        }
    }

#endregion // MonoBehaviour

#region Helper Methods

    void Deactivate()
    {
        curHP = 0;
        gameObject.SetActive(false);
        onBrickDestroyed.Invoke();
    }

    void ResetHP()
    {
        curHP = startingHP;
        UpdateColor();
    }

    void ResetPowerUp()
    {
        if (powerUp != null)
        {
            powerUp.gameObject.SetActive(false);
            powerUp.transform.position = transform.position;
        }
    }

    void ReduceHP()
    {
        curHP--;
        UpdateColor();
        if (curHP <= 0)
        {
            ReleasePowerUp();
            Deactivate();
        }
    }

    void ReleasePowerUp()
    {
        if (powerUp != null)
        {
            powerUp.transform.position = transform.position;
            powerUp.gameObject.SetActive(true);
        }
    }

    void UpdateColor()
    {
        int colorIndex = Mathf.Clamp(0, curHP - 1, hitPointColors.Length - 1);
        thisSpriteRenderer.color = hitPointColors[colorIndex];
    }

#endregion // Helper Methods

}

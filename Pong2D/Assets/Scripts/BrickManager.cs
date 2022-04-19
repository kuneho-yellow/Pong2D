using UnityEngine;
using UnityEngine.Events;

public class BrickManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent onAllBricksDestroyed;

    int remainingBrickCount;

#region MonoBehavior

    void Start()
    {
        InitializeBrickCounter();
    }

#endregion // MonoBehavior

#region Brick Management

    public void OnBrickDestroyed()
    {
        remainingBrickCount--;
        if (remainingBrickCount == 0)
        {
            onAllBricksDestroyed.Invoke();
        }
    }

    void InitializeBrickCounter()
    {
        remainingBrickCount = remainingBrickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
        Brick[] allBricks = GetComponentsInChildren<Brick>(false);
        remainingBrickCount = allBricks.Length;
    }

#endregion // Brick Management

}

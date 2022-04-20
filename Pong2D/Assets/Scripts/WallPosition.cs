using UnityEngine;

public class WallPosition : MonoBehaviour
{
    [SerializeField]
    Transform wallParentL;
    [SerializeField]
    Transform wallParentR;
    [SerializeField] [Range(4, 8)]
    float minGameAreaWidth = 4;
    [SerializeField] [Range(4, 8)]
    float maxGameAreaWidth = 6;
    
    void Awake()
    {
        AdjustWalls();
    }

    void AdjustWalls()
    {
        // Adjust walls so that they are right at te screen edges
        // If screen is too wide, just allow the walls to show so that there isn't too much empty space
        Vector2 botLeft = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 topRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        float targetWidth = Mathf.Clamp(topRight.x - botLeft.x, minGameAreaWidth, maxGameAreaWidth);
        float halfWidth = targetWidth * 0.5f;
        float cameraXPos = Camera.main.transform.position.x;
        Vector3 leftWallPos = wallParentL.position;
        leftWallPos.x = cameraXPos - halfWidth;
        Vector3 rightWallPos = wallParentR.position;
        rightWallPos.x = cameraXPos + halfWidth;
        wallParentL.position = leftWallPos;
        wallParentR.position = rightWallPos;
    }
}

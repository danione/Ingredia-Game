using UnityEngine;

[System.Serializable]
public class Boundaries
{
    public float xLeftBoundary;
    public float xRightBoundary;
    public float yTopBoundary;
    public float yBottomBoundary;

    public static bool IsOutsideBoundaries(Vector3 position, Boundaries boundaries)
    {
        if (boundaries == null) return true;

        // Check if 
        if (position.x >= boundaries.xLeftBoundary || position.x <= boundaries.xRightBoundary) return true;

        if (position.y >= boundaries.yTopBoundary || position.y <= boundaries.yBottomBoundary) return true;

        return false;
    }
}

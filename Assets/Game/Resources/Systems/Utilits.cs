using UnityEngine;

public static class Utilits
{
    public static Vector3 FindRandomPositionXY(float minX, float minY, float maxX, float maxY)
        => new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

}

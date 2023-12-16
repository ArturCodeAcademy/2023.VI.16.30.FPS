using UnityEngine;

public static class ShootingHelper
{
    public static Vector3 SpreadDirection(Vector3 direction, float maxSpreadAngle, float minSpreadAngle = 0)
    {
        float spreadAngle = Random.Range(minSpreadAngle, maxSpreadAngle);
        Vector3 spreadDirection = Quaternion.AngleAxis(spreadAngle, Vector3.up) * direction;
        return spreadDirection;
    }
}

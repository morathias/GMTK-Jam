using UnityEngine;

namespace DefaultNamespace
{
    public static class Extensions
    {
        public static Vector3 ProjectUp(this Vector3 v3)
        {
            return Vector3.ProjectOnPlane(v3, Vector3.up);
        }
    }
}
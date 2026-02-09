using UnityEngine;

namespace Quantum
{
    public static class UnityVector3Extensions
    {
        public static Vector3 SetX(this Vector3 v, float x) => new(x, v.y, v.z);

        public static Vector3 SetY(this Vector3 v, float y) => new(v.x, y, v.z);

        public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);
    }
}

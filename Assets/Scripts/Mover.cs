using System.Collections.Generic;
using UnityEngine;

namespace System.Linq.Move
{
    public static class Mover
    {
        public static void Move(this List<Vector3> from, List<Vector3> to, Vector3 item)
        {
            from.Remove(item);
            to.Add(item);
        }
    }
}
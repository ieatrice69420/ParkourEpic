using System.Collections.Generic;

namespace UnityEngine.AI
{
    public class BotClass : MonoBehaviour
    {
        #region Enums

        /// <summary>
        /// These states show what triggers the navmeshagent touch
        /// </summary>
        public enum TriggerState
        {
            Jump = 0,
            WallJump = 1,
            WallRun = 2
        }

        /// <summary>
        /// These states affect the way the AI is moving, but do not control direction of movement.
        /// </summary>
        public enum MoveState
        {
            Jumping = 0,
            WallRunning = 1,
            Still = 2,
            Rolling = 3,
            Ziplining = 4,
            Roping = 5
        }

        /// <summary>
        /// These states change the direction of movement.
        /// </summary>
        public enum PathFindState
        {
            Wandering = 0,
            Following = 1,
            Objective = 2
        }

        /// <summary>
        /// These states affect the shooting of the AI
        /// </summary>
        public enum ShootState
        {
            Idle = 0,
            Spraying = 1,
            Precise = 2
        }

        #endregion

        public virtual void ShareVelocity(Vector3 inputVelocity, out Vector3 outputVelocity)
        {
            outputVelocity = inputVelocity;
        }

        public virtual GameObject FindClosest(IEnumerable<Transform> gameObjects)
        {
            float lastDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (Transform g in gameObjects)
                if (Vector3.SqrMagnitude(transform.position - g.position) < lastDistance)
                {
                    lastDistance = Vector3.SqrMagnitude(transform.position - g.position);
                    closest = g;
                }

            return closest.gameObject;
        }

        public virtual GameObject FindClosest(IEnumerable<Transform> gameObjects, GameObject origin)
        {
            float lastDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (Transform g in gameObjects)
                if (Vector3.SqrMagnitude(g.transform.position - origin.transform.position) < lastDistance)
                {
                    lastDistance = Vector3.SqrMagnitude(origin.transform.position - g.position);
                    closest = g;
                }

            return closest.gameObject;
        }
    }
}

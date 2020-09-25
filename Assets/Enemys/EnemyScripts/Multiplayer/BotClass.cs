using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual ClosestObject FindClosest(IEnumerable<Transform> gameObjects)
        {
            if (gameObjects.Count<Transform>() > 0)
            {
                float lastDistance = Mathf.Infinity;
                Transform closest = null;

                foreach (Transform g in gameObjects)
                    if (Vector3.SqrMagnitude(transform.position - g.position) < lastDistance)
                    {
                        lastDistance = Vector3.SqrMagnitude(transform.position - g.position);
                        closest = g;
                    }

                return new ClosestObject(closest.gameObject, Vector3.SqrMagnitude(closest.position - transform.position));
            }
            else return new ClosestObject(null, Single.NaN);
        }

        public virtual ClosestVector FindClosest(IEnumerable<Vector3> gameObjects)
        {
            float lastDistance = Mathf.Infinity;
            Vector3 closest = Vector3.zero;

            foreach (Vector3 g in gameObjects)
                if (Vector3.SqrMagnitude(transform.position - g) < lastDistance)
                {
                    lastDistance = Vector3.SqrMagnitude(transform.position - g);
                    closest = g;
                }

            if (closest == Vector3.zero) throw new ArgumentOutOfRangeException();
            else return new ClosestVector(closest, Vector3.SqrMagnitude(closest - transform.position));
        }

        public virtual ClosestObject FindClosest(IEnumerable<Transform> gameObjects, GameObject origin)
        {
            float lastDistance = Mathf.Infinity;
            Transform closest = null;

            foreach (Transform g in gameObjects)
                if (Vector3.SqrMagnitude(g.transform.position - origin.transform.position) < lastDistance)
                {
                    lastDistance = Vector3.SqrMagnitude(origin.transform.position - g.position);
                    closest = g;
                }

            return new ClosestObject(closest.gameObject, Vector3.SqrMagnitude(closest.position - transform.position));
        }

        public virtual Transform[] PlayersInSight(float fieldOfView)
        {
            MultiplayerPlayerManager manager = MultiplayerPlayerManager.instance;
            List<Transform> players = new List<Transform>();

            foreach (Transform player in manager.players)
            {
                Vector3 playerDir = player.position - transform.position;
                float angle = Vector3.Angle(playerDir, transform.forward);

                if (angle < fieldOfView && !Physics.Raycast(transform.position, player.transform.position - transform.position, LayerMask.NameToLayer("Ground")))
                    players.Add(player);
            }

            return players.ToArray<Transform>();
        }

        public virtual Transform[] PlayersInSight(float fieldOfView, Transform ignoredTransform)
        {
            MultiplayerPlayerManager manager = MultiplayerPlayerManager.instance;
            List<Transform> players = new List<Transform>();

            foreach (Transform player in manager.players)
            {
                Vector3 playerDir = player.position - transform.position;
                float angle = Vector3.Angle(playerDir, transform.forward);

                if (angle < fieldOfView && /* !Physics.Raycast(transform.position, player.transform.position - transform.position, LayerMask.NameToLayer("Ground")) && */ player != ignoredTransform)
                    players.Add(player);
            }

            if (players.ToArray<Transform>().Length > 0) Debug.Log(players.ToArray<Transform>()[0]);

            return players.ToArray<Transform>();
        }
    }

    public struct ClosestObject
    {
        public GameObject closest;
        public float sqrDistance;

        public ClosestObject(GameObject closest, float sqrDistance)
        {
            this.closest = closest;
            this.sqrDistance = sqrDistance;
        }
    }

    public struct ClosestVector
    {
        public Vector3 closest;
        public float sqrDistance;

        public ClosestVector(Vector3 closest, float sqrDistance)
        {
            this.closest = closest;
            this.sqrDistance = sqrDistance;
        }
    }
}
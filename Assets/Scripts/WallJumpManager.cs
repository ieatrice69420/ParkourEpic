﻿using System.Linq;

namespace UnityEngine.AI
{
    public class WallJumpManager : MonoBehaviour
    {
        public static WallJumpManager instance;
        public Transform[] wallJumpTriggers;

        private void Awake() => instance = this;

        private void Start()
        {
            if (wallJumpTriggers.Contains<Transform>(null))
                for (var i = 0; i < wallJumpTriggers.Length; i++)
                    wallJumpTriggers[i] = transform.GetChild(i);
        }
    }
}
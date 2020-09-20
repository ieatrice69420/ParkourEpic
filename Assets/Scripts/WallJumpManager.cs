using System.Linq;

namespace UnityEngine.AI
{
    public class WallJumpManager : MonoBehaviour
    {
        public static WallJumpManager instance;
        [SerializeField]
        public Transform[] WallJumpTriggers { get; private set; }

        private void Awake() => instance = this;

        private void Start()
        {
            if (WallJumpTriggers.Contains<Transform>(null))
                for (var i = 0; i < WallJumpTriggers.Length; i++)
                    WallJumpTriggers[i] = transform.GetChild(i);
        }
    }
}
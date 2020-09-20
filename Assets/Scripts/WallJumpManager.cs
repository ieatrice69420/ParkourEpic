namespace UnityEngine.AI
{
    public class WallJumpManager : MonoBehaviour
    {
        public static WallJumpManager instance;
        [SerializeField]
        public Transform[] WallJumpTriggers { get; private set; }
    }
}
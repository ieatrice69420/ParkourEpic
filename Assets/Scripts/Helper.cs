namespace Helper
{
    public static class Mathf
    {
        public static float Clamp(float value, float min, float max)
        {
            if (max > min) return UnityEngine.Mathf.Clamp(value, min, max);
            else return UnityEngine.Mathf.Clamp(value, max, min);
        }
    }
}

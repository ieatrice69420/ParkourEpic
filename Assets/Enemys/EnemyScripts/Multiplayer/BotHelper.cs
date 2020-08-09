using UnityEngine;
using System.Threading.Tasks;

namespace BotHelper
{
    public static class BotHelper
    {
        public static GameObject Instantiate(GameObject bot)
        {
            GameObject instantiatedBot = GameObject.Instantiate(bot);
            return instantiatedBot;
        }

        public static GameObject Instantiate(GameObject bot, Vector3 position, Quaternion rotation)
        {
            GameObject instantiatedBot = GameObject.Instantiate(bot, position, rotation);
            return instantiatedBot;
        }

        public static GameObject Instantiate(GameObject bot, float delay)
        {
            Task.Delay((int)(delay * 1000));
            GameObject instantiatedBot = GameObject.Instantiate(bot);
            return instantiatedBot;
        }

        public static GameObject Instantiate(GameObject bot, float delay, Vector3 position, Quaternion rotation)
        {
            Task.Delay((int)(delay * 1000));
            GameObject instantiatedBot = GameObject.Instantiate(bot, position, rotation);
            return instantiatedBot;
        }
    }
}
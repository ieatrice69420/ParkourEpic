using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Saving
{
	public static class SaveSystem
	{
		public static void SavePlayer(Health healthScript, WeaponManager weaponScript)
		{
			Debug.Log("adfasfs");
			BinaryFormatter formatter = new BinaryFormatter();

			string path = Application.persistentDataPath + "/player.rice";
			Debug.Log(path);
			FileStream stream = new FileStream(path, FileMode.Create);

			PlayerData data = new PlayerData(healthScript, weaponScript);

			formatter.Serialize(stream, data);
			stream.Close();
			Debug.Log("afasf");
		}

		public static PlayerData LoadPlayer()
		{
			string path = Application.persistentDataPath + "/player.rice";
			if (File.Exists(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);

				PlayerData data = formatter.Deserialize(stream) as PlayerData;
				stream.Close();

				return data;
			}
			else
			{
				Debug.LogError("Save file not found in" + path);
				return null;
			}
		}
	}
}
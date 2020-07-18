using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Saving
{
	public static class SavePlayerData
	{
		public static void SavePlayer(Health healthScript, WeaponManager weaponScript)
		{
			BinaryFormatter formatter = new BinaryFormatter();

			string path = Application.persistentDataPath + "/player.rice";
			Debug.Log(path);
			FileStream stream = new FileStream(path, FileMode.Create);

			PlayerData data = new PlayerData(healthScript, weaponScript);

			formatter.Serialize(stream, data);
			stream.Close();
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

	public static class SaveKeyData
	{
		public static void SaveKeys(Keys keys)
		{
			BinaryFormatter formatter = new BinaryFormatter();

			string path = Application.persistentDataPath + "/keys.rice";
			Debug.Log(path);
			FileStream stream = new FileStream(path, FileMode.Create);

			KeyData data = new KeyData(keys);

			formatter.Serialize(stream, data);
			stream.Close();
		}

		public static KeyData LoadKeys()
		{
			string path = Application.persistentDataPath + "/keys.rice";
			if (File.Exists(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);

				KeyData data = formatter.Deserialize(stream) as KeyData;
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
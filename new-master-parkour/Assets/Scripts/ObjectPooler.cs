using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	public static ObjectPooler instance;

    void Awake() => instance = this;

    public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnBulletHole(string tag, Vector3 position, Vector3 normal)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag" + tag + "doesnt exist!");
			return null;
		}

		GameObject objToSpawn = poolDictionary[tag].Dequeue();

		objToSpawn.SetActive(true);
		objToSpawn.transform.forward = normal * -1f;
		objToSpawn.transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 89f));
		objToSpawn.transform.position = position - objToSpawn.transform.forward * .0001f;

		poolDictionary[tag].Enqueue(objToSpawn);

		return objToSpawn;
	}
}
using UnityEngine;
using UnityEngine.AI;

public class JumpPath : MonoBehaviour
{
	[SerializeField]
	OffMeshLink offMeshLink;

	void Update()
	{
		Debug.Log(offMeshLink.occupied);
	}
}

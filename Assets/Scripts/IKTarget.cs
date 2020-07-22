using UnityEngine;

public class IKTarget : MonoBehaviour
{
	[HideInInspector]
	public Vector3 defaultPos;
	
	void Start() => defaultPos = transform.localPosition;
	
	public void ResetPos() => transform.localPosition = defaultPos;
	
	public void SetPosition(Vector3 localPos) => transform.localPosition = localPos;
}
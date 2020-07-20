using UnityEngine;

public class MenuCamera : MonoBehaviour
{
	[SerializeField]
	float rotateSpeed = 7f;

    void Update() => transform.Rotate(transform.up * Time.deltaTime * rotateSpeed, Space.Self);
}
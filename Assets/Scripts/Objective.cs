using UnityEngine;

public class Objective : MonoBehaviour
{
	public static Objective instance;

    void Awake() => instance = this;
}
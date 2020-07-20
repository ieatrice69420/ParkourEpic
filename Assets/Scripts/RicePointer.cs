using UnityEngine;
using TMPro;

public class RicePointer : MonoBehaviour
{
    public Transform rice, pointer;
    [SerializeField]
    TextMeshProUGUI distance;

    void Update()
    {
        distance.text = "distance : " + (System.Math.Sqrt((double)((rice.position.x - transform.position.x) + (rice.position.y - transform.position.y) + (rice.position.z - transform.position.z)))).ToString("F0") + "m";
	}
}
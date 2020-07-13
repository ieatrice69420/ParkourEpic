using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcepush : MonoBehaviour
{
    public float pushAmount;
    public float pushRadious;
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("g");
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadious);
            foreach (Collider pushobject in colliders)
                if (other.tag == "Player")
                {
                    Debug.Log("d");
                    Rigidbody pushbody = pushobject.GetComponent<Rigidbody>();
                    pushbody.AddForce(transform.position * pushAmount);
                }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    Movement move;
    [SerializeField]
    float speed = 10f, duration = 10f;

    void OnTriggerEnter(Collider other)
    {
        move = other.gameObject.GetComponent<Movement>();
        if (move != null)
        {
            move.knockBackDir = other.transform.position - transform.position;
            move.knockBackSpeed = speed;
        }

        if (!other.CompareTag("Cannon"))
        {
            Destroy(gameObject);
        }
    }
}

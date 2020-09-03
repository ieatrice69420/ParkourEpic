using UnityEngine;
using System.Collections;

public class Zipline : MonoBehaviour
{
    [SerializeField]
    ZiplineCarrier ziplineCarrier;
    [Header("Zipline")]
    public Transform carrier;
    public Transform start;
    public Transform end;

    public void Move(float speed, Vector3 hitPoint)
    {
        Vector3 toOther = hitPoint - carrier.position;

        // This gets called if its the player is infront of it
        if (Vector3.Dot(carrier.forward, toOther) > 0)
        {
            Debug.Log("The other transform is in front of me!");
            ziplineCarrier.velocity = carrier.right * speed;
        }
        else
        {
            ziplineCarrier.velocity = carrier.right * -1f * speed;
        }

        StartCoroutine(StartZipLine());
    }

    public void LookAtStart()
    {
        start.LookAt(carrier);
        start.localRotation = Quaternion.Euler(0f, start.localRotation.y, 0f);
    }

    public void LookAtEnd()
    {
        end.LookAt(carrier);
        end.localRotation = Quaternion.Euler(0f, end.localRotation.y, 0f);
    }

    public void FixCarrierRotation()
    {
        carrier.LookAt(end);
    }

    IEnumerator StartZipLine()
    {
        yield return new WaitForSeconds(1f);
        ziplineCarrier.canBeStopped = true;
    }
}
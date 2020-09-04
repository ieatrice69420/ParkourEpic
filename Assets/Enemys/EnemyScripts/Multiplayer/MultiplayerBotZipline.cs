using UnityEngine;

public class MultiplayerBotRopeSwing : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Zipline"))
        {
            Zipline zipline = hit.transform.parent.gameObject.GetComponent<Zipline>();
            zipLineCarrier = hit.transform;
            zipline?.Move(zipLineSpeed, hit.point);
            isZipLining = true;
            instance = this;
        }
    }
}
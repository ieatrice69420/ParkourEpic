using UnityEngine;

public class mouselooking : MonoBehaviour
{
    public float mouseSensetivity = 100f;
    public Movement playerbody;
    float Xrotation = 0;

    void Update() => Look();

    void Look()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        if (!playerbody.isClimbing) playerbody.transform.Rotate(Vector3.up * MouseX);
        else transform.Rotate(Vector3.up * MouseX);

        Xrotation -= MouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
    }
}
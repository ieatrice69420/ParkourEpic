using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselooking : MonoBehaviour
{
    public float mouseSensetivity = 100f, recoilSlowTimer, currentRecoil, decaySpeed;
    public Movement playerbody;
    float Xrotation = 0, beforeRecoil;

    void Update() => Look();

    void Look()
    {
        {
            float MouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            if (!playerbody.isClimbing) playerbody.transform.Rotate(Vector3.up * MouseX);
            else transform.Rotate(Vector3.up * MouseX);

            Xrotation -= MouseY;
            Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

            if (Xrotation - currentRecoil < -90f) transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
            else transform.localRotation = Quaternion.Euler(Xrotation - currentRecoil, 0f, 0f);
        }
    }

    public void StartRecoil(float max, float timerIncrease)
    {
        currentRecoil += max;
        recoilSlowTimer += timerIncrease;
    }

    public void Recoil(float slowSpeed)
    {
        if (currentRecoil > 0f)
        {
            currentRecoil -= slowSpeed * Time.deltaTime;
            Xrotation -= slowSpeed * Time.deltaTime;
            Debug.Log(currentRecoil);
        }
        if (currentRecoil < 0f) currentRecoil = 0f;
    }
}
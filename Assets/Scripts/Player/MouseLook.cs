using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerRoot, lookAroundRoot;
    [SerializeField] bool invert = false;
    [SerializeField] bool canUnlock = true;
    [SerializeField] float sensitivity = 5f;
    [SerializeField] int smoothSteps = 10;
    [SerializeField] float smoothWeight = 0.4f;
    [SerializeField] float rollAngle = 10f;
    [SerializeField] float rollSpeed = 3f;
    [SerializeField] Vector2 lookLimits = new Vector2(-90f, 90f);
    Vector2 lookAngles;
    Vector2 currentMouseLook;
    Vector2 smoothMove;
    float currentRollAngle;
    int lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        cursorLockToggle();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void cursorLockToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensitivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, lookLimits.x, lookLimits.y);

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw("Mouse X") * rollAngle, rollSpeed * Time.deltaTime);

        lookAroundRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}

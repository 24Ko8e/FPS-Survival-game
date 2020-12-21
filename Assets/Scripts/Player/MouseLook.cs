using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerRoot, lookAroundRoot;
    [SerializeField] bool invert;
    [SerializeField] bool canUnlock = true;
    [SerializeField] float sensitivity = 5f;
    [SerializeField] int smoothSteps = 10;
    [SerializeField] float smoothWeight = 0.4f;
    [SerializeField] float rollAngle = 10f;
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

    }
}

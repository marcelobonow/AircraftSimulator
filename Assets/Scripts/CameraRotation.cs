using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{

    [SerializeField] private float deadZone = 10f;
    [SerializeField, Range(20f, 500f)] private float sensibility = 1;

    private Vector2 initialPosition;
    private bool swiping;
    private bool clicked;

    private void Update()
    {
        var overUI = EventSystem.current.IsPointerOverGameObject();
        if (Input.GetMouseButtonDown(0) && !overUI)
        {
            initialPosition = Input.mousePosition;
            clicked = true;
            swiping = false;
        }

        if (Input.GetMouseButton(0) && clicked)
        {
            var swipeDistance = (Vector2)Input.mousePosition - initialPosition;
            if (swipeDistance.magnitude > deadZone)
            {
                swiping = true;
            }

            if (swiping)
            {
                var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * -1f);
                RotateCamera(mouseMovement, sensibility);
            }
        }
    }

    public void RotateCamera(Vector3 mouseMovement, float sensibility)
    {
        var currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += mouseMovement.x * sensibility * Time.deltaTime;
        currentRotation.x += mouseMovement.y * sensibility * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

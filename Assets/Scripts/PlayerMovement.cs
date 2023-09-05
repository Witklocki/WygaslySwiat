using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public float movementSpeed = 5.0f;

    private void Update()
    {

        float horizontal = moveJoystick.Horizontal;
        float vertical = moveJoystick.Vertical;


        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }
    

}

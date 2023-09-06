using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public FixedJoystick moveJoystick;
    public float movementSpeed = 5.0f;
    private bool isRight;
    private bool isLeft;
    private bool isUp;
    private bool isDown;
    private void Update()
    {

        float horizontal = moveJoystick.Horizontal;
        float vertical = moveJoystick.Vertical;
        isRight = (horizontal > 0) && (Mathf.Abs(horizontal) > Mathf.Abs(vertical));
        isLeft = (horizontal < 0) && (Mathf.Abs(horizontal) > Mathf.Abs(vertical));
        isUp = (vertical > 0) && (Mathf.Abs(vertical) > Mathf.Abs(horizontal));
        isDown = (vertical < 0) && (Mathf.Abs(vertical) > Mathf.Abs(horizontal));
        animator.SetBool("isRight", isRight);
        animator.SetBool("isLeft", isLeft);
        animator.SetBool("isUp", isUp);
        animator.SetBool("isDown", isDown);

        animator.SetFloat("SpeedH", Mathf.Abs(horizontal));
        animator.SetFloat("Speed", Mathf.Abs(vertical));

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }
}

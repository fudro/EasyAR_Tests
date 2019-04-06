/**************************************
 This script allows the attached AR gameobject to react to screen touches by turning or moving towards the touch point.
 ************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookController : MonoBehaviour
{
    enum TurnMode {Quick, Smooth, Move};
    int mode = (int)TurnMode.Move;
    Vector3 touchPoint;     
    Vector3 lookDirection;      
    Quaternion rotationAngle;   //Difference between the gamobject's current facing direction and the direction to look
    float turnSpeed = 5.0f;
    float moveSpeed = 10.0f;
    bool turning = false;       //Flag to indicate if the gameobject has completed its turn or is still turning

    void Start()
    {
        //Not Used.
    }

    void Update()
    {
        if (Input.GetMouseButton(0))    //If the left mousebutton is clicked (or the screen is touched on mobile)
        {
            SetTouchPoint();        
        }
        if (turning)    //Continue turning while this flag is true
        {
        switch(mode)    //Select the type of turn reaction
            {
                case (int)TurnMode.Smooth:
                    smoothLook();
                    break;
                case (int)TurnMode.Move:
                    smoothLook();
                    moveVirtualTarget();
                    break;
                default:
                    this.transform.LookAt(touchPoint);
                    break;
            }
        }
    }

    void SetTouchPoint()    //Find the in-world point of the touch.
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            touchPoint = hit.point;
            turning = true;
        }
    }

    void smoothLook()
    {
        lookDirection = new Vector3(touchPoint.x - transform.position.x, transform.position.y, touchPoint.z - transform.position.z);    //The X and Z axes form the ground plane where the gameobject rests. Y-axis is the upward direction (axis of rotation).
        rotationAngle = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, turnSpeed * Time.deltaTime);   //Use a Slerp to smoothly turn through the rotation angle to end facing the direction of the touch.
    }

    void moveVirtualTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, touchPoint, moveSpeed * Time.deltaTime);   //Smoothly move the gamobject towards the touch point.
        if (transform.position == touchPoint)
        {
            turning = false;
        }
        
    }
}

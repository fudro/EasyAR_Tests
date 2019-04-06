using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	Ray GenerateMouseRay() 
	{
		//Get two points through orthographic projection of the screen touch position to the near and far clipping planes of the main camera.
		Vector3 mouseTouchFar = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);								
		Vector3 mouseTouchNear = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

		//Convert the points to world coordinates
		Vector3 mouseWorldFar = Camera.main.ScreenToWorldPoint(mouseTouchFar);
		Vector3 mouseWorldNear = Camera.main.ScreenToWorldPoint (mouseTouchNear);

		Ray mouseRay = new Ray (mouseWorldNear, mouseWorldFar - mouseWorldNear);
		return mouseRay;
	}

    // Update is called once per frame
    void Update()	
    {
		if (Input.GetMouseButton(0))
		{
			Ray mouseRay = GenerateMouseRay ();
			RaycastHit hit;

			if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit))
			{
                //Add code for touches here
			}
		}
    }
}

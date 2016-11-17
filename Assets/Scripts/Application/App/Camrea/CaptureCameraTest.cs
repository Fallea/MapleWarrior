using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaptureCameraTest : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

    // Use this for initialization
    void Start () {
        //CaptureCamera.Capture();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool grab = true;
    void OnPostRender()
    {
        if (grab)
        {
            //CaptureCamera.Capture(new Rect(0, 0, Screen.width, Screen.height));
            grab = false;
        }
    }

}

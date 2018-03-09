using UnityEngine;
using System.Collections;
using Net;

public class SocketClientTest : MonoBehaviour {

    SocketClient sc;

	// Use this for initialization
	void Start () {
        sc = new SocketClient();
        sc.Connect("2121", 1000);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

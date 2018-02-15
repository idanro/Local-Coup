using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        offset = (player.transform.position - transform.position) / 10f;
        transform.position += new Vector3 (offset.x, offset.y, 0);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}

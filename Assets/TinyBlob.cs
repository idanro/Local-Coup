using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBlob : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    Animator anim;
    float velocity;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        offset = (player.transform.position - transform.position + new Vector3(-0.6f,0.9f,0f)) / 50f;
        transform.position += new Vector3(offset.x, offset.y, 0);
        velocity = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(offset.x, 2)) + Mathf.Abs(Mathf.Pow(offset.y, 2)));
        print("Velocity: " + velocity);
        anim.SetFloat("Speed", velocity);
    }
}

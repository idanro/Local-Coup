using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBlob : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    [SerializeField] float eightPatternStrength = 10f; //control the size of the eight figure, unused for now
    [SerializeField] Vector3 eightFigure; //displays the eight figure in inspector
            

    Animator anim;
    float velocity;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {

        eightFigure = new Vector3(
                Mathf.Cos(Time.time),
                Mathf.Sin(2f * (Time.time)) / 2f,
                0f); //the eight figure formula
        offset = (player.transform.position - transform.position + eightFigure + new Vector3(0f, 1f, 0f)) / 50f;
            
        transform.position += new Vector3(offset.x, offset.y, 0);
        velocity = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(offset.x, 2)) + Mathf.Abs(Mathf.Pow(offset.y, 2)));
        print("Velocity: " + velocity);
        anim.SetFloat("Speed", velocity);
    }
}

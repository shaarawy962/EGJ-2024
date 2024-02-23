using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float length, startpos;

    public GameObject camera;
    public Camera cam;

    [SerializeField] private float parallax;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (camera.transform.position.x * parallax);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}

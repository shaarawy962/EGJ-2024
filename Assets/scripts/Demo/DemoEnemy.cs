using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnemy : Enemy
{
    public Transform MyTransform;

    void Start()
    {
        MyTransform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        MyTransform.Translate(new Vector3(1, 0, 0), Space.Self);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformmovement : MonoBehaviour {

    public float speed,
                      tilt;
    public float tgX;
    private Vector3 target;

    private void Start()
    {
        target = new Vector3(tgX, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

        if (transform.position == target && target.x != 300.15f)
        {
            target.x = 300.15f;
        }
        else if (transform.position == target && target.x == 300.15f)
        {
            target.x = 306.45f;
        }
    }
}

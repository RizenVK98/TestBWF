﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    public float speed;

	private void Update () {
        transform.Rotate(Vector3.up,Time.deltaTime*speed);
	}
}

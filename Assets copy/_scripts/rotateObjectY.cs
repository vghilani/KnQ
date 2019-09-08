using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObjectY : MonoBehaviour {

    void Update () {
	gameObject.transform.Rotate(Vector3.down * 60 * Time.deltaTime);
    }
}

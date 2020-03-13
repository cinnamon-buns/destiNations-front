using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPosition : MonoBehaviour
{
    public GameObject globe;
    public void originalPosition() {
        globe = GameObject.Find("/earth/Icosphere");
        globe.transform.rotation = Quaternion.Euler(-120f, -50f, 0f);
        // globe.transform.position = new Vector3(10f, 10f, 10f);
    }

}

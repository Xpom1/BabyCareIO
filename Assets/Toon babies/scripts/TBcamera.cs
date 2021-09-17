using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBcamera : MonoBehaviour {

    public Transform target;  
       	
	void Update ()
    {
        transform.position = target.position + new Vector3(0f, 1f, 2f);      
        transform.LookAt(target.position + new Vector3(0f, 0.5f, 0f));
    }
}

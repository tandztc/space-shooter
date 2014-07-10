using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //print("the object destroyed: " + other.transform.position.ToString());
        Destroy(other.gameObject);
    }
	
}

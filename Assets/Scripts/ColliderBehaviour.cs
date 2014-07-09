using UnityEngine;
using System.Collections;

public class ColliderBehaviour : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        Destroy(gameObject);
	
	}
}

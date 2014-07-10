using UnityEngine;
using System.Collections;

public class ColliderBehaviour : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
	
	}
}

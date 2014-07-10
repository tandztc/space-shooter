using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {

        Destroy(other.gameObject);
    }

}

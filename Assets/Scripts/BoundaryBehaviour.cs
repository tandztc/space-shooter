using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

    //public GameObject enemyExplosion;
    void OnTriggerExit(Collider other)
    {

        Destroy(other.gameObject);
    }

}

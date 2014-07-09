using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

    //public GameObject enemyExplosion;
    void OnTriggerExit(Collider other)
    {
        //print("the object destroyed: " + other.transform.position.ToString());
        Destroy(other.gameObject);
    }

}

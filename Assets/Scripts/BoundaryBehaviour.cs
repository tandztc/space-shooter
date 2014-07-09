using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

    //public GameObject enemyExplosion;
    void OnTriggerExit(Collider other)
    {
        //print("the object destroyed: " + other.transform.ToString());
        //Instantiate(enemyExplosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
        Destroy(other.gameObject);
    }

}

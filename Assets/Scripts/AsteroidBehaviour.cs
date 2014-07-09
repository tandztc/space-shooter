using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour {

    public float tumble;
    public GameObject explosion;
    public int shotScore;
    public int luckyScore;
    public int launchAngle;

    private GameController gameController;

    void Start()
    {
        rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
        transform.rotation = Quaternion.Euler(0, Random.Range(-launchAngle, launchAngle), 0);

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }

        if (other.tag == "Player")
        {
            gameController.AddScore(shotScore);
        }
        else
        {
            return;
            //gameController.AddScore(luckyScore);
        }

        Instantiate(explosion, transform.position, transform.rotation);

        //陨石只负责销毁自己
        //Destroy(other.gameObject);
        Destroy(gameObject);
    }
 
}

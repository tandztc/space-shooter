using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {

    public float speed;
    public Boundary boundary;

    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject playerExplosion;
    public int origPowerLevel;

    private GameController gameController;
    private float nextFire;
    private int powerLevel;

    void Start()
    {
        powerLevel = (origPowerLevel > 0) ? origPowerLevel : 0;

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
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //print(moveHorizontal);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );

    }

    void PlayFire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        for (int i = 1; i < powerLevel; i++)
        {
            Vector3 extraShotPositionBase = shotSpawn.position - shotSpawn.forward;
            Instantiate(shot, extraShotPositionBase + Vector3.left * 0.5f * i, shotSpawn.rotation);
            Instantiate(shot, extraShotPositionBase - Vector3.left * 0.5f * i, shotSpawn.rotation);
        }
        audio.Play();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            PlayFire();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            powerLevel += (powerLevel >= 10) ? 0 : 1;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            powerLevel -= (powerLevel <= 1) ? 0 : 1; ;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            powerLevel += (int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"));
            powerLevel = (powerLevel > 10) ? 10 : powerLevel;
            powerLevel = (powerLevel < 1) ? 1 : powerLevel;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.Gameover();
        }

    }
}

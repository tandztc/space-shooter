using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public Boundary evadeBoundary;
    public GameObject enemyExplosion;
    public int shotScore;
    public int luckyScore;
    public float chargeSpeed, evadeSpeed;
    public float chargeTimeMin, chargeTimeMax;
    public float evadeIntervalMin, evadeIntervalMax;
    

    private GameController gameController;
    private Vector3 evadeTo;
    private float evadeTime;
    private Transform playerTransform;
    private WeaponController weapon;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject playerTmp = GameObject.Find("Player");
        if (playerTmp != null)
        {
            playerTransform = playerTmp.transform;
        }

        weapon = GetComponent<WeaponController>();
        if (weapon == null)
        {
            Debug.Log("No weapon for " + gameObject.name);
        }

        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        //冲锋进场
        rigidbody.velocity = transform.forward * chargeSpeed;
        yield return new WaitForSeconds(Random.Range(chargeTimeMin, chargeTimeMax));

        while (true)
        {
            //冲锋和每次闪避后，速度设为0，打一枪，休息一段时间
            rigidbody.velocity = Vector3.zero;
            Attack();
            yield return new WaitForSeconds(Random.Range(evadeIntervalMin, evadeIntervalMax));

            //生成新的闪避位置
            evadeTo = new Vector3(
                Random.Range(evadeBoundary.xMin, evadeBoundary.xMax), 
                0.0f, 
                Random.Range(evadeBoundary.zMin, evadeBoundary.zMax)
                );

            //闪避时间等于距离除以绝对速度
            evadeTime = Vector3.Distance(evadeTo, rigidbody.transform.position) / evadeSpeed;
            //各分量速度等于距离除以闪避时间
            rigidbody.velocity = (evadeTo - rigidbody.transform.position) / evadeTime;
            //print("Starting envade at " + Time.time + "      envade time = " + evadeTime);
            yield return new WaitForSeconds(evadeTime);
            //print("envading end at " + Time.time);

        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        }
        else
        {
            //weapon.StartAutoFire();
            transform.rotation = Quaternion.identity;
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        if (other.tag == "Player")
        {
            gameController.AddScore(shotScore);
            var x = rigidbody.rotation;
            var y = gameObject.transform.rotation;
            var z = transform.localEulerAngles;
        }
        else
        {
            return;
            //gameController.AddScore(luckyScore);
        }

        Instantiate(enemyExplosion, transform.position, transform.rotation);
        
        Destroy(gameObject);
        
    }

    void Attack()
    {
        weapon.Fire();
        audio.Play();
    }

}

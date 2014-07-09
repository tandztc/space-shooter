using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float defaultFireRate;

    private bool m_bAuto;
    private float m_fireRate;
	void Start () {
        m_fireRate = defaultFireRate;
        m_bAuto = false;
	}

    IEnumerator AutoFire()
    {
        while (m_bAuto)
        {
            Fire();
            yield return new WaitForSeconds(m_fireRate);
        }
        
    }
	
    public void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    public void StartAutoFire(float fireRate)
    {
        if (m_bAuto)
        {
            return;
        }
        m_fireRate = fireRate;
        m_bAuto = true;
        StartCoroutine(AutoFire());
    }

    public void StartAutoFire()
    {
        if (m_bAuto)
        {
            return;
        }
        m_bAuto = true;
        StartCoroutine(AutoFire());
    }

    public void StopAutoFire()
    {
        m_bAuto = false;
    }
}

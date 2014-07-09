using UnityEngine;
using System.Collections;

public class FlyingPose : MonoBehaviour {

    public float tilt;

    private float realTilt;
    void FixedUpdate()
    {
        if (gameObject.transform.rotation.y > 0.0f)
        {
            realTilt = rigidbody.velocity.x * tilt;
        }
        else
        {
            realTilt = -rigidbody.velocity.x * tilt;
        }

        //rigidbody.transform.Rotate(rigidbody.velocity.x * Vector3.forward * tilt);
        //gameObject.transform.rotation.SetLookRotation(Vector3.back);
        //print(rigidbody.velocity.x * -tilt);
        //rigidbody.rotation *= Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);

        rigidbody.rotation = Quaternion.Euler(
            gameObject.transform.rotation.eulerAngles.x,
            gameObject.transform.rotation.eulerAngles.y,
            realTilt);


    }
}

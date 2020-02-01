using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjTurret : MonoBehaviour
{
    [SerializeField]
    public GameObject projectile;

    public float shotSpeed;
    public GameObject shooter;
    public GameObject target;
    Vector3 shooterPosition;
    Vector3 shooterVelocity;
    bool hasFired = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 shooterPosition = shooter.transform.position;
        Vector3 shooterVelocity = shooter.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;
        shooter = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && hasFired == false)
        {
            Vector3 interceptPoint = getTargetPosition(other, shooterPosition, shooterVelocity);
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = interceptPoint.normalized*shotSpeed;
            hasFired = true; 
        }
    }

    public Vector3 getTargetPosition(Collider other, Vector3 shooterPosition, Vector3 shooterVelocity)
    {
        Vector3 targetPosition = other.transform.position;
        Vector3 targetVelocity = other.GetComponent<Rigidbody>() ? other.GetComponent<Rigidbody>().velocity : Vector3.zero;
        Vector3 interceptPoint = FirstOrderIntercept(shooterPosition, shooterVelocity, shotSpeed, targetPosition, targetVelocity);
        return interceptPoint;
    }


    public static Vector3 FirstOrderIntercept(Vector3 shooterPosition, Vector3 shooterVelocity, float shotSpeed, Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime(shotSpeed, targetRelativePosition, targetRelativeVelocity);
        return targetPosition + t * (targetRelativeVelocity);   
    }
    
    public static float FirstOrderInterceptTime(float shotSpeed, Vector3 targetRelativePosition, Vector3 targetRelativeVelocity)
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if(velocitySquared < 0.001f)
        {
            return 0f;
        }

        float a = velocitySquared - shotSpeed * shotSpeed;
        if(Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude / (2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition));
            return Mathf.Max(t, 0f); //uses greatest between t or 0 so we don't shoot at a past position
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;

        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        {
            float t1 = (-b + Mathf.Sqrt(determinant) / 2f * a);
            float t2 = (-b - Mathf.Sqrt(determinant) / 2f * a);

            if (t1 > 0f)
            {
                if (t2 > 0f)
                {
                    return Mathf.Min(t1, t2);
                }
                else return t1;
            }
            else return Mathf.Max(t2, 0f);
        }
        else if (determinant < 0f)
        {
            return 0f;
        }
        else return Mathf.Max(-b / (2f * a), 0f);
    }
}

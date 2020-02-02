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
    float timer = 0.0f;

    [SerializeField]
    private int turretLevel;

    // Start is called before the first frame update
    void Start()
    {
        turretLevel = 1;
        shooter = gameObject;
        shooterPosition = shooter.transform.position;
        shooterVelocity = shooter.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
        }
        else
        {
            hasFired = false;
        }
    }

    public int returnLevel()
    {
        return turretLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && hasFired == false && turretLevel == 1)
        {
            attackFunc(other);
            timer = 3.0f;
        }
        else if(other.tag == "Enemy" && hasFired == false && turretLevel == 2)
        {
            attackFunc(other);
            timer = 2.0f;
        }
        else if(other.tag == "Enemy" && hasFired == false && turretLevel == 3)
        {
            attackFunc(other);
            timer = 1.0f; 
        }
    }

    private void attackFunc(Collider other)
    {
        target = other.gameObject;
        Vector3 interceptPoint = getTargetPosition(target, shooterPosition, shooterVelocity);
        GameObject bullet = Instantiate(projectile, shooterPosition, gameObject.transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().setLevel(turretLevel);
        bullet.GetComponent<Rigidbody>().velocity = interceptPoint.normalized * shotSpeed;
        hasFired = true;
    }

    public Vector3 getTargetPosition(GameObject target, Vector3 shooterPosition, Vector3 shooterVelocity)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>() ? target.GetComponent<Rigidbody>().velocity : Vector3.zero;
        Vector3 whereTheTargetWillBe = FirstOrderIntercept(shooterPosition, shooterVelocity, shotSpeed, targetPosition, targetVelocity);
        return whereTheTargetWillBe;
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

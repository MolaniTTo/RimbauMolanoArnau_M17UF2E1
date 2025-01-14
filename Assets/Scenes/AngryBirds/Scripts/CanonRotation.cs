using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        var direction = mousePos - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, _minRotation.z, _maxRotation.z);
        transform.rotation = Quaternion.Euler(0, 0, angle + offset);

        if (Input.GetMouseButton(0))
        {
            
            ProjectileSpeed += Time.deltaTime * 4;
            Debug.Log(ProjectileSpeed);
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * ProjectileSpeed;
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}

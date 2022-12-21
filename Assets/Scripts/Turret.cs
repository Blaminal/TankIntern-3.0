using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;

    // [Header("Bullet ")]
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    private GameObject tank;
    [SerializeField]
    private Transform projectileDirection;
    [SerializeField]
    private ObjectPool bulletPool;
    [SerializeField]
    private int bulletPoolCount = 2;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private bool hasShot = false;
    /*private ObjectPool bulletPool;
    [SerializeField]
    private int bulletPoolCount = 10;
    */

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        //bulletPool = GetComponent<ObjectPool>();
    }

    
    public void OnShoot(InputAction.CallbackContext context)
    {
        hasShot = context.action.triggered;
        
    }

    

    private void Update()
    {
        if (hasShot)
        {
            //GameObject bullet = Instantiate(bulletPrefab, projectileDirection.position, projectileDirection.rotation);
            //bullet.SetActive(true);
            
            // Debug.Log(bulletPool);

            GameObject b = bulletPool.GetObject();
            if (b != null) {
                b.transform.position = projectileDirection.transform.position;
                b.transform.rotation = projectileDirection.transform.rotation;      
                b.SetActive(true);
                b.GetComponent<BulletController>().Initialize(bulletPool, tank);               
            }
            // Debug.Log(b.GetComponent<BulletController>() == null);
            hasShot = false;
        }
    }
}

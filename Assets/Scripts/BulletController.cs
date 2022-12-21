using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private int hitScore = 10;

    private ObjectPool objectPool;

    private Rigidbody2D rb2d;
    private GameObject tankOwner;

    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(3f);
        objectPool.ReturnObject(gameObject);
    }

    
    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        //rb2d.velocity = transform.position * speed * Time.deltaTime * 100f;
        
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     objectPool.ReturnObject(gameObject);

    // }

    private void OnCollisionEnter2D(Collision2D other) { 
        tankOwner.GetComponent<TankMover>().ChangeScoreOfPlayer(hitScore);
        objectPool.ReturnObject(gameObject);
    }

    public void Initialize(ObjectPool objectPool, GameObject tank)
    {
        
        this.objectPool = objectPool;
        tankOwner = tank;
        StartCoroutine(DestroyBulletAfterTime());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), tank.GetComponent<Collider2D>());

    }

}

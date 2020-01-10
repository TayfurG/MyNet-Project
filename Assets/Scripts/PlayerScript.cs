using System.Collections;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;

    [NonSerialized]
    public bool isTripleShootActivated = false;
    [NonSerialized]
    public float fireSpeed = 200;
    [NonSerialized]
    public float spawnRate = 0.5f;
    [NonSerialized]
    public int shootAmount = 1;

    private Vector2 firePoint = Vector2.zero;

    private float nextTimeToSpawn = 0;

    [NonSerialized]
    public int abilityCount = 0;

    void OnEnable()
    {
        SetFirePoint();
    }

    private void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {

            nextTimeToSpawn = Time.time + 1f / spawnRate;                  
            
                SpawnAndShootBullet(transform.up.normalized);
      
            if (shootAmount > 1)
            {           
                StartCoroutine(BulletShoot( (20 / (fireSpeed) / spawnRate), transform.up.normalized));
            }

            if(isTripleShootActivated)
            {
                //transform.up +- transform.right rotates 45 degree..
                SpawnAndShootBullet((transform.up + transform.right).normalized);
                SpawnAndShootBullet((transform.up - transform.right).normalized);

                if (shootAmount > 1)
                {
                    StartCoroutine(BulletShoot((20 / (fireSpeed) / spawnRate), (transform.up + transform.right).normalized));
                    StartCoroutine(BulletShoot((20 / (fireSpeed) / spawnRate), (transform.up - transform.right).normalized));
                }
            }
        }
    }
    public IEnumerator BulletShoot(float timeToWait, Vector2 direction)
    {
        yield return new WaitForSeconds(timeToWait);
        SpawnAndShootBullet(direction);
        
    }

    void SpawnAndShootBullet(Vector2 direction)
    {
        
        GameObject bulletPre = Instantiate(bullet, firePoint, Quaternion.identity);
        bulletPre.GetComponent<Rigidbody2D>().AddForce(direction * fireSpeed);
        Destroy(bulletPre, 2f);
    }

    public void SpawnCopy(bool isTripleShootActivated, int shootAmount, float fireSpeed, float fireFrequency)
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.value, UnityEngine.Random.value));

        GameObject newPlayer = Instantiate(transform.gameObject, randomPositionOnScreen, Quaternion.identity);
        PlayerScript newPlayerScript = newPlayer.GetComponent<PlayerScript>();
        if (this.isTripleShootActivated != false)
            newPlayerScript.isTripleShootActivated = true;
        newPlayerScript.shootAmount = this.shootAmount;
        newPlayerScript.fireSpeed = this.fireSpeed;
        newPlayerScript.spawnRate = this.spawnRate;
    }

    private void SetFirePoint()
    {
        float playerSpriteHeightSize = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        float bulletSpriteHeightSize = bullet.transform.localScale.y;
        firePoint = new Vector2(transform.position.x,
                                transform.position.y + playerSpriteHeightSize / 2 + bulletSpriteHeightSize / 2);
    }

}

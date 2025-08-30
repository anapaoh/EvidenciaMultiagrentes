using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragonBoss : MonoBehaviour
{
    [Header("Prefabs de balas/mounstruos")]
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    [Header("Configuración de disparo")]
    public float patternDuration = 10f; 
    public float timeBetweenPatterns = 1f;


    private void Start()
    {
        StartCoroutine(ShootPatterns());
    }

    private IEnumerator ShootPatterns()
    {
        yield return StartCoroutine(PatternRadial());
        yield return new WaitForSeconds(timeBetweenPatterns);

        yield return StartCoroutine(PatternRandomBurst());
        yield return new WaitForSeconds(timeBetweenPatterns);

        yield return StartCoroutine(PatternSimpleCircle());
        yield return new WaitForSeconds(timeBetweenPatterns);
    }

    private IEnumerator PatternRadial()
    {
        float elapsed = 0f;
        float fireRate = 0.5f;
        int bulletsPerShot = 12;

        while (elapsed < patternDuration)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                float angle = i * 360f / bulletsPerShot;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Vector3 spawnPos = transform.position + transform.forward * 1f;

                GameObject bullet = Instantiate(bulletPrefab1, spawnPos, rotation);
                bullet.GetComponent<Bullet>().Initialize(rotation * Vector3.forward, 10f);
            }

            elapsed += fireRate;
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator PatternRandomBurst()
    {
        float elapsed = 0f;
        float fireRate = 0.2f;

        while (elapsed < patternDuration)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;

            for (int i = -1; i <= 1; i++)
            {
                Quaternion rotation = Quaternion.LookRotation(randomDirection) * Quaternion.Euler(0, i * 10f, 0);
                Vector3 spawnPos = transform.position + transform.forward * 1f;

                GameObject bullet = Instantiate(bulletPrefab2, spawnPos, rotation);
                bullet.GetComponent<Bullet>().Initialize(rotation * Vector3.forward, 15f);
            }

            elapsed += fireRate;
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator PatternSimpleCircle()
    {
        float elapsed = 0f;
        float fireRate = 3f;
        int bulletsInCircle = 72;

        Vector3[] offsets = new Vector3[]
        {
            new Vector3(20f, 0, 0),
            new Vector3(-20f, 0, 0),
            new Vector3(0, 0, 20f),
            new Vector3(0, 0, -20f)
        };

        while (elapsed < patternDuration)
        {
            foreach (Vector3 offset in offsets)
            {
                Vector3 spawnPos = transform.position + offset;

                for (int i = 0; i < bulletsInCircle; i++)
                {
                    float angle = i * (360f / bulletsInCircle);
                    Quaternion rotation = Quaternion.Euler(0, angle, 0);

                    GameObject bullet = Instantiate(bulletPrefab3, spawnPos, rotation);
                    bullet.GetComponent<Bullet>().Initialize(rotation * Vector3.forward, 20f);
                }
            }

            elapsed += fireRate;
            yield return new WaitForSeconds(fireRate);
        }
    }
}

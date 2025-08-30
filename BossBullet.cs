using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    private bool isInitialized = false;

    public void Initialize(Vector3 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
        isInitialized = true;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        
        if (GetComponent<Collider>() == null)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 0.2f;
            collider.isTrigger = true;
        }
        
        BulletCounter.Instance.BulletCreated();
        
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        if (!isInitialized) return;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInitialized) return;
        
        if (other.CompareTag("Boss") || other.CompareTag("Terrain")) 
        {
            return;
        }

        if (string.IsNullOrEmpty(other.tag) || other.CompareTag("Untagged"))
        {
            return;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInitialized) return;
        OnTriggerEnter(collision.collider);
    }
    
    private void OnDestroy()
    {
        if (isInitialized)
        {
            BulletCounter.Instance.BulletDestroyed();
        }
    }
}

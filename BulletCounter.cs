using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public static BulletCounter Instance;

    [Header("UI References")]
    public GameObject bulletCountTextObject; 
    private TMP_Text bulletCountText;

    private int currentBulletCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        if (bulletCountTextObject == null)
        {
            bulletCountTextObject = GameObject.Find("BulletCounterText");
            if (bulletCountTextObject == null)
            {
                Debug.LogError("No se encontró BulletCounterText");
                return;
            }
        }

        bulletCountText = bulletCountTextObject.GetComponent<TMP_Text>();
        if (bulletCountText == null)
        {
            Debug.LogError("El objeto no tiene componente TMP_Text");
            return;
        }
    }

    void Start()
    {
        Debug.Log("BulletCounter iniciado");
        UpdateBulletCountText();
    }

    public void BulletCreated()
    {
        currentBulletCount++;
        UpdateBulletCountText();
    }

    public void BulletDestroyed()
    {
        currentBulletCount--;
        UpdateBulletCountText();
    }

    private void UpdateBulletCountText()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = $"Balas: {currentBulletCount}";

        }
    }
}

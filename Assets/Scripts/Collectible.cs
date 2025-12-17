using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleData data;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ScoreManager.instance != null && data != null)
        {
            ScoreManager.instance.AddScore(data.scoreValue);
        }
        AudioManager.instance.PlayCollect();

        Destroy(gameObject);
    }
}

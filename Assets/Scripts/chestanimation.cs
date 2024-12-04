using UnityEngine;
using System.Collections;
using DG.Tweening;

public class chestanimation : MonoBehaviour
{
    private bool isOpened = false;

    public Sprite closechest, Openchest;

    public SpriteRenderer spriterendrer;

    public int coinsToAdd = 10;

    public GameObject coinPrefab;  // Reference to the coin prefab

    public Transform coinSpawnPoint;  // Point where coins will spawn

    public RectTransform coinTextUI;  // Reference to the coin text UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened && other.gameObject.CompareTag("Player"))
            return;
        {
            isOpened = true;
            spriterendrer.sprite = Openchest;
            PlayerMovements player = other.gameObject.GetComponent<PlayerMovements>();
            if (player != null)
            {
                player.AddCoins(coinsToAdd);
                StartCoroutine(SpawnAndAnimateCoins(coinsToAdd, transform.position));
            }
            FindFirstObjectByType<UIManager>().Levelcomplete();
        }
    }
    private void Start()
    {
        spriterendrer.sprite = closechest;
    }

    private IEnumerator SpawnAndAnimateCoins(int numberOfCoins, Vector3 startPosition)
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            GameObject coin = Instantiate(coinPrefab, startPosition, Quaternion.identity);
            RectTransform coinRectTransform = coin.GetComponent<RectTransform>();
            Vector3 endPosition = coinTextUI.position;

            // Animate the coin to the coin text UI
            yield return StartCoroutine(MoveCoin(coinRectTransform, startPosition, endPosition, 0.5f));

            Destroy(coin);  // Destroy the coin after the animation
        }
    }

    private IEnumerator MoveCoin(RectTransform coin, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            coin.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        coin.position = end;
    }

}
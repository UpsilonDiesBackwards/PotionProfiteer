using System.Net.Http.Headers;
using UnityEngine;

public class FlockController : MonoBehaviour {
    public GameObject bird;

    public int minBirds = 3;
    public int maxBirds = 7;

    public float areaRadius = 1f;

    private Vector3 _origLocalScale;

    void Awake() {
        SpawnBirds();

        _origLocalScale = transform.localScale;
    }

    void SpawnBirds() {
        int numBirds = Random.Range(minBirds, maxBirds + 1);
        for (int i = 0; i < numBirds; i++) {
            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * areaRadius;
            GameObject newBird = Instantiate(bird, randomPos, Quaternion.identity, transform);

        if (Random.value <= 0.5f) {
            // Flip the bird on the X axis
            Vector3 newScale = newBird.transform.localScale;
            newScale.x *= -1f;
            newBird.transform.localScale = newScale;
        }
        }
    }
}

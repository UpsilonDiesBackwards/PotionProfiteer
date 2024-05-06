using System.Net.Http.Headers;
using UnityEngine;

public class FlockController : MonoBehaviour {
    public GameObject Creature;

    public int min = 3;
    public int max = 7;

    public float areaRadius = 1f;

    private Vector3 _origLocalScale;

    public bool shouldRandomlyFlip = true;

    void Awake() {
        SpawnCreature();

        _origLocalScale = transform.localScale;
    }

    void SpawnCreature() {
        int numBirds = Random.Range(min, max + 1);
        for (int i = 0; i < numBirds; i++) {
            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * areaRadius;
            GameObject newCreature = Instantiate(Creature, randomPos, Quaternion.identity, transform);


            if (shouldRandomlyFlip) {
                if (Random.value <= 0.5f) {
                    Vector3 newScale = newCreature.transform.localScale;
                    newScale.x *= -1f;
                    newCreature.transform.localScale = newScale;
                }
            }
        }
    }
}

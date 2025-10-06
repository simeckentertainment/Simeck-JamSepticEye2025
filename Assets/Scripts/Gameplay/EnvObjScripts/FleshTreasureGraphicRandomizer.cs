using UnityEngine;

public class FleshTreasureGraphicRandomizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] fleshSprites;
    void Start()
    {
        sr.sprite = fleshSprites[Random.Range(0, fleshSprites.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

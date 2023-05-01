using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersHandler : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;
    public int layerDiff = 2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            playerSpriteRenderer.sortingOrder = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            playerSpriteRenderer.sortingOrder = -7;
        }
    }
}

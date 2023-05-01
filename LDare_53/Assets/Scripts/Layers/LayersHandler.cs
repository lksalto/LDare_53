using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersHandler : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;

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
            playerSpriteRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
        }
    }
}

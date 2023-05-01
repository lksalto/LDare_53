using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersHandler : MonoBehaviour
{
    SpriteRenderer playerSpriteRenderer;
    GameObject[] shadows;
    GameObject shadowV;
    GameObject shadowH;
    SpriteRenderer playerShadowRenderer;
    public int layerDiff = 2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            playerSpriteRenderer.sortingOrder = 10;
            shadowV = other.GetComponent<PlayerMov3D>().verShadow;
            shadowH = other.GetComponent<PlayerMov3D>().horShadow;
            shadowV.GetComponent<SpriteRenderer>().sortingOrder = 10;
            shadowH.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            playerSpriteRenderer.sortingOrder = -7;
            shadowV.GetComponent<SpriteRenderer>().sortingOrder = -7;
            shadowH.GetComponent<SpriteRenderer>().sortingOrder = -7;
        }
    }
}

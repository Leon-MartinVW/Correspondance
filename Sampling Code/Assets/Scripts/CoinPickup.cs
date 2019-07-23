using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int pointsForCoinPickup = 100;
    //dont forget to add soundeffects on collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        //pickupsound
        Destroy(gameObject);
    }
}

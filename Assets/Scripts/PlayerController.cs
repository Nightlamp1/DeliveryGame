using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerHealth = 3;
    public GameObject[] playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            playerHealth -= 1;
            Debug.Log("Player Health Remaining: " + playerHealth);
        }

        if(collision.tag == "Envelope")
        {
            Debug.Log("You picked up an envelope");
            addEnvelopeToInventory(collision.gameObject);
        }

        if(collision.tag == "Mailbox")
        {
            Debug.Log("you scored!!");
            string mailboxColor = collision.gameObject.GetComponent<Mailbox>().mailboxColor;
            deliverMailToMailbox(mailboxColor);

        }
    }

    private void Start()
    {
        playerInventory = new GameObject[3];
        Debug.Log(playerInventory[0]);
    }

    void addEnvelopeToInventory(GameObject newEnvelope)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if(playerInventory[i] == null)
            {
                playerInventory[i] = newEnvelope;
                newEnvelope.SetActive(false);
                return;
            }
        }
        Debug.Log("Inventory is full");
    }

    void deliverMailToMailbox(string color)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i].GetComponent<Envelope>().envelopeColor == color)
            {
                Destroy(playerInventory[i]);
                playerInventory[i] = null;
                GameController.playerScore += 10;
                Debug.Log(GameController.playerScore);
                return;
            }
        }
    }
}

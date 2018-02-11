using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int playerHealth = 3;
    public GameObject[] playerInventory;
    public Text playerHealthText;
    public GameObject emptyInventorySlot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            playerHealth -= 1;
            playerHealthText.text = "Player Health: " + playerHealth;
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
        for(int i = 0; i < playerInventory.Length; i++)
        {
            playerInventory[i] = emptyInventorySlot;
        }
        Debug.Log(playerInventory[0]);
    }

    void addEnvelopeToInventory(GameObject newEnvelope)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if(playerInventory[i] == emptyInventorySlot)
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
                playerInventory[i] = emptyInventorySlot;
                GameController.playerScore += 10;
                ItemSpawnController.envelopeCount -= 1;
            }
        }
    }
}

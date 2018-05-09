using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int playerHealth = 3;
    public GameObject[] playerInventory;
    public Text playerHealthText;
    public GameObject emptyInventorySlot;
    public Image[] inventorySlots = new Image[3];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            playerHealth -= 1;
            playerHealthText.text = "Player Health: " + playerHealth;
            if(playerHealth == 0)
            {
                gameOver();
            }
        }

        if(collision.tag == "Envelope")
        {
            addEnvelopeToInventory(collision.gameObject);
        }

        if(collision.tag == "Mailbox")
        {
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
            inventorySlots[i].enabled = false;
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
                inventorySlots[i].enabled = true;
                inventorySlots[i].sprite = newEnvelope.GetComponent<SpriteRenderer>().sprite;
                newEnvelope.SetActive(false);
                return;
            }
        }
    }

    void deliverMailToMailbox(string color)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i].GetComponent<Envelope>().envelopeColor == color)
            {
                Destroy(playerInventory[i]);
                playerInventory[i] = emptyInventorySlot;
                inventorySlots[i].enabled = false;
                GameController.playerScore += 10;
                ItemSpawnController.envelopeCount -= 1;
            }
        }
    }

    void gameOver()
    {
        Application.LoadLevel("MainMenu");
    }

}

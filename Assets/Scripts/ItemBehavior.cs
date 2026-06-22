using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameBehavior GameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(collision.gameObject);
            Debug.Log("Item collected");
            GameManager.Items += 1;
            GameManager.PrintLootReport();
        }
    }
    void Start()
    {
        GameManager=GameObject.Find("Game_Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

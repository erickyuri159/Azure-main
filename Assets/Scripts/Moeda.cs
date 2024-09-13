using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3)
        {
          Player player = collider.gameObject.GetComponent<Player>();
            player.CJ.GanhaMoedas(1);
            Destroy(gameObject);
        }
    }
}

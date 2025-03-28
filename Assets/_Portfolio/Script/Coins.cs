using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioClip _CoinSound;

    private PlayerController Player;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.Coins++;
            if(Player != null)
            {
                Player.Sound();
            }
            Destroy(this.gameObject);
        }
    }
}

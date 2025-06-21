using UnityEngine;

public class camera : MonoBehaviour
{
    GameObject Player;
    Vector3 posicaocamera;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        posicaocamera = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        this.transform.position = posicaocamera + offset;
    }
}

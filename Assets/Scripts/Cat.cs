using Unity.VisualScripting;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject globalVolume;
    Game game;
    public AudioSource[] popSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = globalVolume.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.missed < game.missThreshold)
        {
            float horizontal = Input.GetAxis("Mouse X");
            Vector3 current = transform.position;
            transform.position = new Vector3(current.x + horizontal, current.y, current.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "bubble")
        {
            popSounds[Random.Range(0, 3)].Play();
            game.score += 1;
            GameObject.Destroy(collider.gameObject);
        }
    }
}

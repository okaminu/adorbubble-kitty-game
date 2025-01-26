using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameObject globalVolume;
    Game game;

    float moveDelta = 0.04f;
    float moveThreshold = 4;
    Vector3 originalPosition;
    bool toLeft = false;
    bool toRight = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = globalVolume.GetComponent<Game>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!toLeft && !toRight)
        {
            toLeft = true;
        }

        if (toLeft && game.missed < game.missThreshold)
        {
            transform.position = new Vector3(transform.position.x - moveDelta, transform.position.y, transform.position.z);
            if ((originalPosition.x - transform.position.x) >= moveThreshold)
            {
                toLeft = false;
                toRight = true;
            }
        }

        if (toRight && game.missed < game.missThreshold)
        {
            transform.position = new Vector3(transform.position.x + moveDelta, transform.position.y, transform.position.z);
            if ((transform.position.x - originalPosition.x) >= moveThreshold)
            {
                toLeft = true;
                toRight = false;
            }
        }

    }
}

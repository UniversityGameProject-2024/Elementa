using UnityEngine;

public class BackgroundMoveWithPlayer : MonoBehaviour
{

    private GameObject cammera;
    [SerializeField] private float parallaxEffect;
    private float x_Position;
    void Start()
    {
        cammera = GameObject.Find("Main Camera");

        x_Position = transform.position.x;
    }

    void Update()
    {
        float moveDist = cammera.transform.position.x * parallaxEffect;

        transform.position = new Vector2(x_Position + moveDist, transform.position.y);
    }
}

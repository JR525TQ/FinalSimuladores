using UnityEngine;

public class ControlCamara : MonoBehaviour
{

    public float speed = 20.0f;
    public float borde = 15.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.mousePosition.y >= Screen.height - borde)
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= borde)
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - borde)
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= borde)
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}

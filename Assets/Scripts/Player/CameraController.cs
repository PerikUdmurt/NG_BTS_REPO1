using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera m_camera;
    public Controller player;
    public float coof;
    public float motionSpeed;
    public void Awake()
    {
        m_camera = GetComponent<Camera>();
    }
    private void Update()
    {
        CameraMove();
    }
    private void CameraMove()
    {
        Vector3 screenCenter = new Vector3 (Screen.width*0.5f, Screen.height*0.5f, 0);
        Vector2 destinationVector2 = player.transform.position + (Input.mousePosition - screenCenter)*coof;
        Vector3 destinationPosition = Vector2.Lerp(m_camera.transform.position, destinationVector2, motionSpeed);
        m_camera.transform.position = new Vector3(destinationPosition.x, destinationPosition.y, m_camera.transform.localPosition.z);
    }
}

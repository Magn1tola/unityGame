using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    private GameObject[] backgrounds;
    private float[] backgroundSpeed;
    private Vector3 cameraStartPosition;
    private Transform cameraTransform;
    private float distance;

    private float farthestBackground;
    private Material[] materials;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraStartPosition = cameraTransform.position;

        var backgroundCount = transform.childCount;
        materials = new Material[backgroundCount];
        backgroundSpeed = new float[backgroundCount];
        backgrounds = new GameObject[backgroundCount];

        for (var i = 0; i < backgroundCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backgroundCount);
    }

    private void LateUpdate()
    {
        distance = cameraTransform.position.x - cameraStartPosition.x;

        for (var i = 0; i < backgrounds.Length; i++)
        {
            var speed = backgroundSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }

    private void BackSpeedCalculate(int backCount)
    {
        for (var i = 0; i < backCount; i++)
            if (backgrounds[i].transform.position.z - cameraTransform.position.z > farthestBackground)
                farthestBackground = backgrounds[i].transform.position.z - cameraTransform.position.z;

        for (var i = 0; i < backCount; i++)
            backgroundSpeed[i] = 1 - (backgrounds[i].transform.position.z - cameraTransform.position.z) / farthestBackground;
    }
}
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private static readonly int MainTexShader = Shader.PropertyToID("_MainTex");

    [SerializeField] private float parallaxSpeed;

    [SerializeField] private GameObject[] backgroundLayers;

    private float[] _backgroundsSpeed;
    private Vector3 _cameraStartPosition;

    private Transform _cameraTransform;

    private Material[] _layersMaterial;

    private void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
        _cameraStartPosition = _cameraTransform.position;

        var backgroundCount = backgroundLayers.Length;

        _layersMaterial = new Material[backgroundCount];
        _backgroundsSpeed = new float[backgroundCount];
        var farthestBackground = 0f;
        for (var i = 0; i < backgroundCount; i++)
        {
            var backgroundLayer = backgroundLayers[i];
            _layersMaterial[i] = backgroundLayer.GetComponent<Renderer>().material;

            var diffZ = backgroundLayer.transform.position.z - _cameraTransform.position.z;
            if (diffZ > farthestBackground) farthestBackground = diffZ;

            _backgroundsSpeed[i] =
                1 - (backgroundLayer.transform.position.z - _cameraTransform.position.z) / farthestBackground;
        }
    }

    private void LateUpdate()
    {
        var distance = _cameraTransform.position.x - _cameraStartPosition.x;

        for (var i = 0; i < backgroundLayers.Length; i++)
        {
            var speed = _backgroundsSpeed[i] * parallaxSpeed;
            _layersMaterial[i].SetTextureOffset(MainTexShader, new Vector2(distance, 0) * speed);
        }
    }
}
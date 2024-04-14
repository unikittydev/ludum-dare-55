using UnityEngine;

public class PostProcessHandler : MonoBehaviour
{
    private static readonly int THRESHOLD = Shader.PropertyToID("_Threshold");
    private static readonly int VIEWPORT_CENTER = Shader.PropertyToID("_ViewportCenter");
    
    [SerializeField] private Material _ppMaterial;

    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    private void Awake()
    {
        Vector3 position = _target != null ? _camera.WorldToViewportPoint(_target.position) : Vector3.one * .5f;
        
        _ppMaterial.SetVector(VIEWPORT_CENTER, position);
    }

    private void OnDestroy()
    {
        _ppMaterial.SetFloat(THRESHOLD, 0f);
    }
}

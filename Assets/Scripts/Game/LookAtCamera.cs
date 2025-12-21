using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (gameObject.activeSelf == false) return;
        if (_canvas == null || _mainCamera == null) return;
        Vector3 dir = _canvas.transform.position - _mainCamera.transform.position;
        dir.x = 0f;
        if (dir.sqrMagnitude < 0.001f) return;
        _canvas.transform.rotation = Quaternion.LookRotation(dir);
    }
}

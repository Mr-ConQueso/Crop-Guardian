using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private float moveSpeed = 1.0f;
    
    // ---- / Serialized Variables / ---- //
    private Transform _playerTransform;
    
    /*
    private void Awake()
    {
        Material mat = GetComponent<Material>();
        mat.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
    }
    */

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}

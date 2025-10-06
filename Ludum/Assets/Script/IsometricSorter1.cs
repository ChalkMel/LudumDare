using UnityEngine;

public class IsometricSorter1 : MonoBehaviour
{
    public Transform pivotPoint; // Точка, за которую "цепляется" объект (обычно низ)
    public bool affectsSorting = true;

    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();

        // Если pivot point не задан, используем нижнюю точку коллайдера
        if (pivotPoint == null && objectCollider != null)
        {
            CreatePivotPoint();
        }
        else if (pivotPoint == null)
        {
            pivotPoint = transform;
        }
    }

    void Update()
    {
        if (affectsSorting)
        {
            UpdateSortingBasedOnPivot();
        }
    }

    void CreatePivotPoint()
    {
        GameObject pivotObj = new GameObject("PivotPoint");
        pivotObj.transform.SetParent(transform);

        // Устанавливаем pivot point в нижнюю часть коллайдера
        Bounds bounds = objectCollider.bounds;
        pivotObj.transform.localPosition = new Vector3(0, -bounds.extents.y, 0);

        pivotPoint = pivotObj.transform;
    }

    void UpdateSortingBasedOnPivot()
    {
        // Сортируем based on pivot point (нижней точки), а не центра объекта
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-pivotPoint.position.y * 100);
    }

    // Вызывается когда другой объект входит в триггер
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdatePlayerSorting(other.transform);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdatePlayerSorting(other.transform);
        }
    }

    void UpdatePlayerSorting(Transform player)
    {
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
        if (playerRenderer == null) return;

        float playerY = player.position.y;
        float pivotY = pivotPoint.position.y;

        // Если игрок ниже pivot point объекта, он должен быть ПОД объектом
        if (playerY < pivotY)
        {
            playerRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        }
        else
        {
            playerRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
        }
    }
}

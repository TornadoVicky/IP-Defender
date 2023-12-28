using UnityEngine;
using UnityEngine.UI;

public class ContinuousPrefabPlacement : MonoBehaviour
{
    public GameObject prefabToPlace;
    public int initialSortingOrder = 1;
    public float sortingOrderIncrement = 1f;
    public Image imageToSample;
    public RectTransform noSpawnZone1; // Reference to the first RectTransform
    public RectTransform noSpawnZone2; // Reference to the second RectTransform

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (prefabToPlace == null)
        {
            Debug.LogError("Prefab not assigned to the script.");
        }

        if (imageToSample == null)
        {
            Debug.LogError("Image not assigned to the script.");
        }

        if (noSpawnZone1 == null || noSpawnZone2 == null)
        {
            Debug.LogError("NoSpawnZone RectTransforms not assigned to the script.");
        }
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -mainCamera.transform.position.z;

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Convert mouse position to local coordinates of the first RectTransform
            Vector2 localMousePos1;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(noSpawnZone1, Input.mousePosition, mainCamera, out localMousePos1);

            // Convert mouse position to local coordinates of the second RectTransform
            Vector2 localMousePos2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(noSpawnZone2, Input.mousePosition, mainCamera, out localMousePos2);

            // Check if the local position is inside the bounds of either RectTransform
            if (noSpawnZone1.rect.Contains(localMousePos1) || noSpawnZone2.rect.Contains(localMousePos2))
            {
                // Mouse is inside either RectTransform, do not spawn the prefab
                return;
            }

            // Instantiate the prefab at the mouse position
            GameObject newPrefab = Instantiate(prefabToPlace, worldPosition, Quaternion.identity);

            // Get color from the image and set it to the prefab
            Image image = imageToSample.GetComponent<Image>();
            if (image != null)
            {
                SpriteRenderer prefabRenderer = newPrefab.GetComponent<SpriteRenderer>();
                if (prefabRenderer != null)
                {
                    prefabRenderer.color = image.color;
                }
            }

            // Increase sorting order for proper rendering order
            SpriteRenderer newPrefabRenderer = newPrefab.GetComponent<SpriteRenderer>();
            if (newPrefabRenderer != null)
            {
                newPrefabRenderer.sortingOrder = initialSortingOrder;
                //initialSortingOrder++;
            }
        }
    }
}

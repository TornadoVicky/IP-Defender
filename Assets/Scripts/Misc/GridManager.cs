using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX = 5;
    public int gridSizeY = 5;
    public float tileSize = 1.0f;
    public GameObject squarePrefab;
    private bool[,] occupiedTiles;

    public int numOccupiedTiles = 1;
    private Vector2 touchStart;

    void Start()
    {
        CreateGrid();
        occupiedTiles = new bool[gridSizeX, gridSizeY];
    }

    void CreateGrid()
    {
        Vector3 parentPosition = transform.position;
        float startX = parentPosition.x - (gridSizeX - 1) * tileSize / 2;
        float startY = parentPosition.y - (gridSizeY - 1) * tileSize / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 tilePosition = new Vector3(startX + x * tileSize, startY + y * tileSize, parentPosition.z);
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchEnd = Camera.main.ScreenToWorldPoint(touch.position);
                float distance = Vector2.Distance(touchStart, touch.position);

                if (distance < 20f) // Adjust this threshold as needed
                {
                    int gridX = Mathf.FloorToInt((touchEnd.x - transform.position.x) / tileSize + gridSizeX / 2);
                    int gridY = Mathf.FloorToInt((touchEnd.y - transform.position.y) / tileSize + gridSizeY / 2);

                    if (gridX >= 0 && gridX < gridSizeX && gridY >= 0 && gridY < gridSizeY)
                    {
                        if (CanOccupyTiles(gridX, gridY))
                        {
                            for (int i = gridX; i < gridX + numOccupiedTiles && i < gridSizeX; i++)
                            {
                                for (int j = gridY; j < gridY + numOccupiedTiles && j < gridSizeY; j++)
                                {
                                    float centerX = transform.position.x + (i - gridSizeX / 2) * tileSize + tileSize / 2;
                                    float centerY = transform.position.y + (j - gridSizeY / 2) * tileSize + tileSize / 2;

                                    // Instantiate the square at the clicked tile center
                                    Instantiate(squarePrefab, new Vector3(centerX, centerY, transform.position.z), Quaternion.identity);

                                    // Mark the tile as occupied
                                    occupiedTiles[i, j] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    bool CanOccupyTiles(int startGridX, int startGridY)
    {
        int endGridX = startGridX + numOccupiedTiles;
        int endGridY = startGridY + numOccupiedTiles;

        if (endGridX > gridSizeX || endGridY > gridSizeY)
        {
            return false; // Not enough tiles available
        }

        for (int i = startGridX; i < endGridX; i++)
        {
            for (int j = startGridY; j < endGridY; j++)
            {
                if (occupiedTiles[i, j])
                {
                    return false; // Tile is already occupied
                }
            }
        }

        return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 parentPosition = transform.position;
        float startX = parentPosition.x - (gridSizeX - 1) * tileSize / 2;
        float startY = parentPosition.y - (gridSizeY - 1) * tileSize / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Shift the Gizmo position to the right by half a tileSize
                Vector3 center = new Vector3(startX + x * tileSize + tileSize / 2 + tileSize / 2, startY + y * tileSize + tileSize / 2, parentPosition.z);
                Gizmos.DrawWireCube(center, new Vector3(tileSize, tileSize, 0));
            }
        }
    }
}

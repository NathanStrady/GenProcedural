using UnityEngine;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetGridPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetGridPosition(x, y), GetGridPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetGridPosition(x, y), GetGridPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetGridPosition(0, height), GetGridPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetGridPosition(width, 0), GetGridPosition(width, height), Color.white, 100f);
    }
    
    #region WorldPositionConverter
    // Equivalent to CellToWorld Position
    private Vector3 GetGridPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    // Equivalent to WorldToCell Position
    private void GetWorldPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    
    #endregion
    
    #region GetValue
    public TGridObject GetValue(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"GETVALUE : Out of range ({x}, {y})");
            return default(TGridObject);
        }
        
        return gridArray[x, y];
    }

    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetWorldPosition(worldPosition, out x, out y);
        
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"GETVALUE : Out of range ({x}, {y})");
            return default(TGridObject);
        }
        
        return gridArray[x, y];
    }
    
    #endregion
    
    #region SetValue
    public bool SetValue(int x, int y, TGridObject value)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"SETVALUE : Out of range ({x}, {y})");
            return false;
        }

        gridArray[x, y] = value;
        debugTextArray[x, y].text = gridArray[x, y].ToString();
        return true;
    }
    
    public bool SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetWorldPosition(worldPosition, out x, out y);
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"SETVALUE : Out of range ({x}, {y})");
            return false;
        }
        SetValue(x, y, value);
        return true;
    }
    #endregion
}

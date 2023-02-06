using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    
    [Header("Map settings")]
    [Tooltip("Height and width of the map.")][SerializeField] private Vector2Int Size;
    [Range(0, 100)][SerializeField] private float zoomX, zoomY;
    private float[,] Map;

    [Header("Externals element")] 
    [SerializeField] private GameObject WhiteCell; 
    [SerializeField] private GameObject BlackCell;
    [SerializeField] private Transform GridTransform;
    [SerializeField] private GridLayoutGroup _gridLayout;

    private List<GameObject> cells = new List<GameObject>();

    private void Start() {
        Map = new float[Size.x, Size.y];
    }

    void Update() {
        _gridLayout.constraintCount = Size.y;
        GenerateMap();
        DisplayMap();
    }

    private void GenerateMap() {
        for (int i = 0; i < Size.x; i++) {
            for (int j = 0; j < Size.y; j++) {
                Map[i, j] = Mathf.PerlinNoise(i * zoomX, j * zoomY);
                Map[i, j] = Mathf.SmoothStep(0, 0.4999f, 0);
                Map[i, j] = Mathf.SmoothStep(0.5f, 1, 1);
            }
        }
    }

    private void DisplayMap() {
        foreach (var VARIABLE in cells) {
            Destroy(VARIABLE);
        }
        foreach (float cell in Map) {
            if (cell < 0.5f) {
                GameObject instance = Instantiate(BlackCell, GridTransform);
                cells.Add(instance);
            }
            else {
                GameObject instantiate = Instantiate(WhiteCell, GridTransform);
                cells.Add(instantiate);
            }
        }
    }
}
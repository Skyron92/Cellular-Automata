using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    
    [Header("Map settings")]
    [Tooltip("Height and width of the map.")][SerializeField] private Vector2Int Size;
    [Range(0, 1)][SerializeField] private float _fillPercent;
    [Range(0, 100)][SerializeField] private float _zoom;
    private float[,] Map;
    private List<float[,]> maps = new List<float[,]>();

    [Header("Externals element")] 
    [SerializeField] private GameObject WhiteCell; 
    [SerializeField] private GameObject BlackCell;
    [SerializeField] private Transform GridTransform;
    [SerializeField] private GridLayoutGroup _gridLayout;

    private List<GameObject> cells = new List<GameObject>();

    void Update() {
        if (Size.x <= 0) Size.x = 1;
        if (Size.y <= 0) Size.y = 1;
        if (_zoom <= 0) _zoom = 0.01f;
        _gridLayout.constraintCount = Size.y;
        if(Input.GetButtonDown("Jump")) NewMap();
    }

    private void GenerateMap() {
        for (int i = 0; i < Size.x; i++) {
            for (int j = 0; j < Size.y; j++) {
                float sampleX = j / _zoom;
                float sampleY = i / _zoom;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                Map[i, j] = perlinValue;
                if (Map[i, j] > _fillPercent) Map[i, j] = 1;
                else Map[i, j] = 0; 
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

    public void NewMap() {
        Map = new float[Size.x, Size.y];
        GenerateMap();
        DisplayMap();
    }
}

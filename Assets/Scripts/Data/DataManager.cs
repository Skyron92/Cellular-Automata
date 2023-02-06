using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    [Header("Map settings")] [Tooltip("Height and width of the map.")] [SerializeField]
    private Vector2Int Size;

    [Range(0, 1)] [SerializeField] private float _fillPercent;
    [Range(0, 0.5f)] [SerializeField] private float _zoom;
    public static Cell[,] Map;

    [Header("Externals element")] [SerializeField]
    private GameObject WhiteCell;

    [SerializeField] private GameObject BlackCell;
    [SerializeField] private Transform GridTransform;
    [SerializeField] private GridLayoutGroup _gridLayout;

    private List<GameObject> cells = new List<GameObject>();

    void Update() {
        if (Size.x <= 0) Size.x = 1;
        if (Size.y <= 0) Size.y = 1;
        if (_zoom <= 0) _zoom = 0.01f;
        _gridLayout.constraintCount = Size.y;
        if (Input.GetButtonDown("Jump")) NewMap();
        if(Input.GetButtonDown("Submit")) GenerateNoise();
        if (Input.GetButtonDown("Fire1")) IterateMap(Map);
    }

    private void GenerateFirstMap() {
        for (int i = 0; i < Size.x; i++) {
            for (int j = 0; j < Size.y; j++) {
                float sampleX = j * _zoom;
                float sampleY = i * _zoom;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                Map[i, j].State = perlinValue;
                if (Map[i, j].State > _fillPercent) Map[i, j].State = 1;
                else Map[i, j].State = 0;
            }
        }

        Debug.Log("Noise is generated !");
    }

    private void AttributeCellsType() {
        foreach (Cell cell in Map) {
            if (cell.State == 0) Map[cell.X, cell.Y] = new Rock();
            if (cell.State == 1) Map[cell.X, cell.Y] = new Grass();
        }

        Debug.Log("Cells are born !");
    }

    private Cell[,] GetNextMap(Cell[,] currentMap) {
        Cell[,] NextMap = new Cell[Size.x, Size.y];
        foreach (Cell cell in currentMap) {
            if (cell.NextState() == 0) NextMap[cell.X, cell.Y] = new Rock();
            if (cell.NextState() == 1) NextMap[cell.X, cell.Y] = new Grass();
        }

        /*if (NextMap == currentMap) return NextMap;
        else {
            GetNextMap(NextMap);
        }*/
        Debug.Log("Next Map is generated !!");
        return NextMap;
    }

    private void DisplayMap(Cell[,] currentMap) {
        foreach (var VARIABLE in cells) {
            Destroy(VARIABLE);
        }

        foreach (Cell cell in currentMap) {
            if (cell.State < 0.5f) {
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
        Map = new Cell[Size.x, Size.y];
        for (int i = 0; i < Size.x; i++) {
            for (int j = 0; j < Size.y; j++) {
                Map[i, j] = new Rock();
            }
        }

        GenerateFirstMap();
        AttributeCellsType();
        Map = GetNextMap(Map);
        DisplayMap(Map);
    }

    public void IterateMap(Cell[,] currentMap) {
        if (currentMap == null) {
            Debug.Log("Can't iterate a null Map");
            return;
        }
        currentMap = GetNextMap(currentMap);
        DisplayMap(currentMap);
    }

    public void GenerateNoise() {
        float[,] noise = new float[Size.x, Size.y];
        for (int i = 0; i < Size.x; i++) {
            for (int j = 0; j < Size.y; j++) {
                float sampleX = i * _zoom;
                float sampleY = j * _zoom;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noise[i, j] = perlinValue;
                if (noise[i, j] > _fillPercent) noise[i, j] = 1;
                else noise[i, j] = 0;
            }
        }
        foreach (float value in noise) {
            if (value < _fillPercent) {
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


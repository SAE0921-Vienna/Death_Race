using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nicrom.PM;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spaceShip;
    public GameObject spaceShipsParentMesh;

    public FlexibleColorPicker flexibleColorpicker;

    public List<Palette> paletteInColorManager;
    private List<CellData> cellstorage;

    public List<Color32> color;
    public Color32 chosencolor;
    public Color32 previouscolor;

    private GameObject child;
    private GameObject childsChild;

    private Transform parent;
    private int siblingIndex;

    private void Awake()
    {
        for (int i = 0; i < spaceShipsParentMesh.transform.childCount; i++)
        {
            if (spaceShipsParentMesh.transform.GetChild(i).gameObject.activeSelf == true)
            {
                spaceShip = spaceShipsParentMesh.transform.GetChild(i).gameObject;
            }
        }

        paletteInColorManager = spaceShip.GetComponent<PaletteModifier>().palettesList;

        cellstorage = spaceShip.GetComponent<PaletteModifier>().palettesList[0].cellsList;

        int y = cellstorage.Count;

        for (int i = 0; i < y; i++)
        {
            color.Add(cellstorage[i].currentCellColor);

            child = transform.GetChild(i).gameObject;
            childsChild = child.transform.GetChild(0).gameObject;

            childsChild.GetComponent<Image>().color = color[i];
        }
    }

    private void Update()
    {
        if (parent != null)
        {
            parent.GetChild(0).GetComponent<Image>().color = chosencolor;
        }
    }

    public void ColorPressed(Button button)
    {
        flexibleColorpicker.gameObject.SetActive(true);

        parent = button.transform.parent.transform;

        flexibleColorpicker.color = parent.GetChild(0).GetComponent<Image>().color;
        siblingIndex = parent.GetSiblingIndex();

        chosencolor = flexibleColorpicker.color;
    }

    public void OnColorChange()
    {
        chosencolor = flexibleColorpicker.color;
    }

    public void ApplyColor()
    {
        if (cellstorage[siblingIndex].currentCellColor.Equals(chosencolor)) return;
        previouscolor = cellstorage[siblingIndex].currentCellColor;
        cellstorage[siblingIndex].currentCellColor = chosencolor;

    }

    //public void ResetColor()
    //{
    //    cellstorage[siblingIndex].previousCellColor = previouscolor;
    //    chosencolor = cellstorage[siblingIndex].previousCellColor;
    //    cellstorage[siblingIndex].currentCellColor = chosencolor;
    //}
}

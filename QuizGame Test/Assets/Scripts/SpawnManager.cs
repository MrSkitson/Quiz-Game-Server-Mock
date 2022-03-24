
using UnityEngine;
using UnityEngine.UI;
public class SpawnManager : MonoBehaviour
{
    public GameObject[] ColoredPrefabs; // цветные префабы
 
    public GameObject[] SpawnPositions; // позиции спавна префабов
    public Button[] ColoredButtons;     // цветные кнопки
 
 
 
    private GameObject[] _existingObjects;
 
    private void Start()
    {
        _existingObjects = new GameObject[ColoredPrefabs.Length];
 
        for (int i = 0; i < ColoredButtons.Length; i++)
        {
            int index = i;
            ColoredButtons[i].onClick.AddListener(() => ButtonClick(index));
        }
    }
 
    public void ButtonClick(int index)
    {
 
        if (_existingObjects[index] == null)
        {
            _existingObjects[index] = Instantiate(ColoredPrefabs[index], SpawnPositions[index].transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(_existingObjects[index]);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Screen 
{
    private ICollection<Object> _BtnList = null;

    public Screen()
    {
        _BtnList = new List<Object>();
    }

    public ICollection<Object> BtnList => _BtnList;
}


public class ScreenHash 
{
    private List<Screen> _screens = null;
    public ScreenHash()
    {
        _screens = new List<Screen>();
    }
    public List<Screen> Screens => _screens;
}


public class PanelManager : MonoBehaviour
{
      //List objects to create panlels (5 elements) 
    public List<GameObject> ListPanel = new List<GameObject>();
    //Place to Instantiate Panel
    public Transform root2;
    //place to Cash  index created Panels
    private ScreenHash _screensCash;
    //index created panel with null passibility
    public int? CurrentSceentIndex { get; set; }

    private void Start()
    { 
        _screensCash = new ScreenHash();

    }

    public void CreatePanel()
    {
        Clear();
        //_currentSceentIndex = _currentSceentIndex == null ? 0 : _currentSceentIndex;
        var currentScreen = new Screen();

        
        for (int i = 0; i < 5; i++)
        {

            int index = Random.Range(0, ListPanel.Count);
            currentScreen.BtnList.Add(ListPanel[index]);
            
        }
        _screensCash.Screens.Add(currentScreen);
        CurrentSceentIndex = _screensCash.Screens.Count() - 1;
        DisplayScreen();
    }

    


    //Method to desplay have in  out - created panel with current index from Cash method
    public void DisplayScreen()
    {
        Clear();
        foreach (var item in (_screensCash.Screens[(int)CurrentSceentIndex]).BtnList)
        {
            Instantiate(item, root2);
        }
    }

    //Method for clearering panel loop for childCount 
    void Clear()
    {
        for (int i = 0; i < root2.childCount; i++)
        {
            Destroy(root2.GetChild(i).gameObject);
        }
    }

    //Method for Next Button in Canvas, get from clicked on Next Button
    public void NextButtonClicked()
    {
        if (CurrentSceentIndex == null)
        {
            CreatePanel();
            return;
        }
        CurrentSceentIndex++;
        if (CurrentSceentIndex == _screensCash.Screens.Count()) { 

            CreatePanel();
        }

        else { DisplayScreen(); }
    }

    //Method for Previous Button in Canvas, get from clicked on Previous Button. 
    public void PreviusButtonClicked()
    {
        if(CurrentSceentIndex == null || CurrentSceentIndex == 0)
        {
            return;
        }else
        {
            CurrentSceentIndex--;
            DisplayScreen();
        }

    }
    
}

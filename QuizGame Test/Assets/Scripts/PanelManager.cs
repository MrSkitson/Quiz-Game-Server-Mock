using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : Singleton<PanelManager>
{
        //Thw list of all the panels aviarible
    //public List<PanelModel> Panels;

    

    //this is going to hold all of our Instances
    private List<panelInstanceModel> _listInstances =new List<panelInstanceModel>();
    //Pool of panels
    private ObjectPool _objectPool;

    private void Start()
    {
        //Cache the object pool
        _objectPool = ObjectPool.Instance;
    }

    public void ShowPanel(string panelId) 
    {
        GameObject panelInstance = _objectPool.GetObjectfromPool(panelId);

        if(panelInstance != null)
        {
            //Create a new Instance
         //var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);
        //Add new panel to the queue
        _listInstances.Add( new panelInstanceModel{
             PanelId = panelId,
             PanelInstance = panelInstance
         });
        }
        else
        {
            Debug.LogWarning( $"trying to use panelId = {panelId} , but this is not in Panels");
        }
    }

    public void HideLastPanel(string panelId)
    {   //Make sure we do have panel showing
        if(AnyPanelShowing())
        {
            //Get the last panel showing
            var lastPanel = _listInstances[_listInstances.Count - 1];
            _listInstances.Remove(lastPanel);
            //Destroy that Instance
            _objectPool.PoolObject(lastPanel.PanelInstance);
        }

    }
    //Return if any panel is showing
    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }
    //returns how many panels we have in queue
    public int GetAmountPanelsInQueue()
    {
        return _listInstances.Count;
    }
}

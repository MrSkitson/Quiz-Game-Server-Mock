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

    public void ShowPanel(string panelId, PanelShowBehaviour behavior = PanelShowBehaviour.KEEP_PREVIOUS) 
    {
        GameObject panelInstance = _objectPool.GetObjectfromPool(panelId);

        if(panelInstance != null)
        {
            if(behavior == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInQueue() > 0)
            {
               var lastPanel = GetLastPanel(); 
                if(lastPanel != null)
                {
                    lastPanel.PanelInstance.SetActive(false);
                }
           }

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
            var lastPanel = GetLastPanel();

            _listInstances.Remove(lastPanel);
            //Destroy that Instance
            _objectPool.PoolObject(lastPanel.PanelInstance);

            if(GetAmountPanelsInQueue() > 0)
            {
                lastPanel =GetLastPanel();
                if(lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }

    }

     panelInstanceModel GetLastPanel()
    {
        return _listInstances[_listInstances.Count - 1];
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

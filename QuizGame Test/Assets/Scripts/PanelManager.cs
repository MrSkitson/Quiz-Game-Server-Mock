using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : Singleton<PanelManager>
{
        //Thw list of all the panels aviarible
    public List<PanelModel> Panels;

    //this is going to hold all of our Instances
    private Queue<panelInstanceModel> _queue =new Queue<panelInstanceModel>();

    public void ShowPanel(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(panel => panel.PanelId == panelId);
        if(panelModel != null)
        {
            //Create a new Instance
         var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);
        //Add new panel to the queue
         _queue.Enqueue( new panelInstanceModel{
             PanelId = panelId,
             PanelInstance = newInstancePanel
         });
        }
        else
        {
            Debug.LogWarning( $"trying to use panelId = {panelId} , but this is not in Panels");
        }
    }

    public void HideLastPanel()
    {   //Make sure we do have panel showing
        if(AnyPanelShowing())
        {
            //Get the last panel showing
            var lastPanel = _queue.Dequeue();
            //Destroy that Instance
            Destroy(lastPanel.PanelInstance);
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
        return _queue.Count;
    }
}

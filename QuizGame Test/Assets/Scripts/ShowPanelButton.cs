using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    // the id of the panel we want to show
    public string PanelId;
    //cashed panel manager
    private PanelManager _panelManager;

    private void Start()
    {
        //cache this
        _panelManager = PanelManager.Instance;
    }

    public void DoShowPanel()
    {
        PanelManager.Instance.ShowPanel(PanelId);
    }
}

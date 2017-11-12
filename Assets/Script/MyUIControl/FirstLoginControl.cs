using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class FirstLoginControl : MyUIControl
{
    private FirstLoginGroup unityUI;

    public FirstLoginGroup UnityUI {
        get {
            if (unityUI == null)
                unityUI = myRootUI as FirstLoginGroup;
            return unityUI; }
    }

    public override void Start()
    {
        LoadRootUI("FirstLoginChooser", UIFactory.CanvasType.AlwaysRefresh);

        UnityUI.Hide();
    }

    public override void Update()
    {
    }
}

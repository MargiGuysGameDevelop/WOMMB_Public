using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class LoginUIControl : MyUIControl
{

    private LoginUIGroup myUIGroup;

    public LoginUIGroup UnityUI {
        get {
            if (myUIGroup == null) {
                myUIGroup = myRootUI as LoginUIGroup;
            }
            return myUIGroup;
        }
    }

    public override void Start()
    {
        LoadRootUI("LoginUI", UIFactory.CanvasType.UsuallyRefresh);
    }

    public override void Update()
    {
        
    }
}

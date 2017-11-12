using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIGroup : MyUnityUIGroup {

    public MyUnityUI Btn_Login {
        get { return members[0]; }
    }

    public MyUnityUI Btn_AboutUs {
        get { return members[1]; }
    }

    public MyUnityUI Btn_Exit {
        get { return members[2]; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ConnectionSystem : MySystem
{
    public ConnectionType CONNECTTYPE = ConnectionType.Photon;

    public ConnectionSystem() {
        switch (CONNECTTYPE)
        {
            case ConnectionType.Photon:
                break;
            case ConnectionType.None:
                break;
            default:
                break;
        }
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TestUIControl : MyUIControl
{
    public override void Start()
    {
        LoadRootUI("TestUI", UIFactory.CanvasType.UsuallyRefresh);
    }

    public override void Update()
    {
        
    }
}

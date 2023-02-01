using Godot;
using System;

public partial class ScrollCenter : ScrollContainer
{
    public override void _Ready()
    {
        GetHScrollBar().Changed += Center;
    }

    public void Center()
    {
        ScrollHorizontal = (int)(((Control)GetChild(0)).Size.x - Size.x) / 2;
    }
}

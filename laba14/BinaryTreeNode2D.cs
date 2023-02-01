using Godot;
using System;

public partial class BinaryTreeNode2D : Control
{
	private Label _label;
	
	public override void _Ready()
	{
		_label = (Label) GetNode("./Circle/Label");
	}
	
	public void SetText(int value) => _label.Text = $"{value}";
}

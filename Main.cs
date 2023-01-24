using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Main : Control
{
	[Export]
	public BinaryTree Original { get; set; }
	[Export]
	public BinaryTree Modified { get; set; }
	[Export]
	public AcceptDialog Dialog { get; set; }
	
	public static Main Instance { get; internal set; }
	
	public override void _Ready()
	{
		Instance = this;
	}

	public static void ShowDialog(string s)
	{
		Instance.Dialog.DialogText = s;
		Instance.Dialog.Popup();
	}
}

using Godot;
using System;

public partial class LineEdit : Godot.LineEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
		
		TextSubmitted += OnTextSubmitted;
	}
	
	private void OnTextSubmitted(string text)
	{
		try
		{
			Main.Instance.Original.Parse(text);
			Main.Instance.Original.Render();
			if (!Main.Instance.Original.IsValid())
				throw new Exception("Исходное дерево не является бинарным деревом поиска!");

			Main.Instance.Modified.CopyFrom(Main.Instance.Original);
			Process(Main.Instance.Modified.Root);
			Tools.WriteNodesToFile(Main.Instance.Modified.Root);
			Main.Instance.Modified.Render();
			if (!Main.Instance.Modified.IsValid())
				throw new Exception("Обработанное дерево не является бинарным деревом поиска!");
		}
		catch (Exception e)
		{
			Main.ShowDialog($"{e}");
		}
	}
	
	private void OnButtonPressed()
		=> OnTextSubmitted(Text);

	private static void Process(BinaryTreeNode<int> root)
	{
		if (root is null)
			return;

		var maxPrimeLeaf = MaxPrimeLeaf(root);
		if (maxPrimeLeaf == int.MinValue)
			throw new Exception("Не существует простых листьев");

		ReplacePrimeWith(root, maxPrimeLeaf);
	}

	private static int MaxPrimeLeaf(BinaryTreeNode<int> root)
	{
		if (root is null)
			return int.MinValue;

		if (root.IsLeaf())
		{
			return Tools.IsPrime(root.Value) ? root.Value : int.MinValue;
		}

		return Math.Max(MaxPrimeLeaf(root.Left), MaxPrimeLeaf(root.Right));
	}

	private static void ReplacePrimeWith(BinaryTreeNode<int> root, int value)
	{
		if (root is null)
			return;
		
		if (Tools.IsPrime(root.Value))
			root.Value = value;
		
		ReplacePrimeWith(root.Left, value);
		ReplacePrimeWith(root.Right, value);
	}
}

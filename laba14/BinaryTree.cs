using Godot;
using System;
using System.Linq;

public partial class BinaryTree : Control
{
	[Export]
	public PackedScene BTNodeTemplate { get; set; }
	
	[Export]
	public GradientTexture1D LineGradient { get; set; }
	
	public BinaryTreeNode<int> Root { get; internal set; } = null;
	public int Depth { get; internal set; } = 0;
	
	const float _width = 64 + 16;
	const float _height = 64 + 16;

	public void Parse(string input)
	{
		var values = input.Split(" ").Select(int.Parse).ToList();
		Root = null;
		
		if (!values.Any() || values[0] == 0)
			return;
		
		Root = new BinaryTreeNode<int>(values[0]);
		Depth = 1;
		for (var i = 1; i < values.Count && values[i] != 0; i++)
			Depth = Math.Max(Root.Add(values[i]), Depth);
		
		GD.Print($"Done. Depth: {Depth}");
		GD.Print($"{Root}");
	}

	public void CopyFrom(BinaryTree other)
	{
		Root = other.Root.DeepCopy();
		Depth = other.Depth;
	}
	
	public void Render()
	{
		foreach (var child in GetChildren())
			child.QueueFree();
		
		var width = _width * (float) Math.Pow(2, Depth - 1);
		CustomMinimumSize = new Vector2(width * 2, Depth * _height);
		Render(Root, this, width, width);
	}

	private void MakeLine(Control btn, float x)
	{
		var line = new Line2D() { ZIndex = -1, TextureMode = Line2D.LineTextureMode.Stretch, Texture = LineGradient };
		line.Position = new Vector2(0, 0);
		line.AddPoint(new Vector2(0, 0));
		line.AddPoint(new Vector2(x, _height));
		btn.AddChild(line);
	}

	private void Render(BinaryTreeNode<int> node, Node sceneNode, float x, float width)
	{
		if (node is null)
			return;
		
		var btn = BTNodeTemplate.Instantiate() as BinaryTreeNode2D;
		sceneNode.AddChild(btn);
		btn.SetText(node.Value);
		btn.Position = new Vector2(x, _height);

		width /= 2;
		
		if (node.Left is not null)
		{
			MakeLine(btn, -width);
			Render(node.Left, btn, -width, width);
		}

		if (node.Right is not null)
		{
			MakeLine(btn, width);
			Render(node.Right, btn, width, width);
		}
	}

	public bool IsValid() => IsValid(Root, int.MinValue, int.MaxValue);

	private bool IsValid(BinaryTreeNode<int> node, int rangeMin, int rangeMax)
	{
		if (node is null)
			return true;

		if (node.Value < rangeMin || node.Value > rangeMax)
			return false;

		return IsValid(node.Left, rangeMin, node.Value - 1) && IsValid(node.Right, node.Value + 1, rangeMax);
	}
}

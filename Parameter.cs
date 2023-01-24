using Godot;
using System;

public partial class Parameter : LineEdit
{
	private void Calculate()
	{
		Text = $"Исходное: {CalculateParameter(Main.Instance.Original.Root)}, Обработанное: {CalculateParameter(Main.Instance.Modified.Root)}";
	}

	private int CalculateParameter(BinaryTreeNode<int> node, int depth = 1, bool isLeft = false)
	{
		if (node is null)
			return 0;
		
		var result = 0;

		if (isLeft && depth % 2 == 1)
			result += node.Value;

		depth++;
		return result + CalculateParameter(node.Left, depth, true) + CalculateParameter(node.Right, depth, false);
	}
}

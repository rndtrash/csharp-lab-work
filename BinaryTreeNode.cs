using System;
using System.Collections.Generic;

public class BinaryTreeNode<T> : IComparable<T> where T : IComparable<T>
{
	public T Value { get; set; }
	
	public BinaryTreeNode<T> Left { get; set; } = null;
	public BinaryTreeNode<T> Right { get; set; } = null;
	
	public BinaryTreeNode(T value) => Value = value;
	
	public int CompareTo(T other) => Value.CompareTo(other);
	public int CompareTo(BinaryTreeNode<T> other) => Value.CompareTo(other.Value);
	
	public int Add(T v, int depth = 1)
	{
		var compare = v.CompareTo(Value);
		depth++;
		switch (compare)
		{
			case < 0 when Left is null:
				Left = new BinaryTreeNode<T>(v);
				return depth;
			case < 0:
				return Left.Add(v, depth);
			case > 0 when Right is null:
				Right = new BinaryTreeNode<T>(v);
				return depth;
			case > 0:
				return Right.Add(v, depth);
			// v == Value
			default:
				throw new Exception($"Число {v} уже есть в дереве!");
		}
	}

	public bool IsLeaf() => Left is null && Right is null;
	
	public override string ToString()
		=> $"{Value} ({Left} {Right})";

	public BinaryTreeNode<T> DeepCopy() => new(Value) {Left = Left?.DeepCopy(), Right = Right?.DeepCopy()};

	public void ToList(ref List<T> list)
	{
		list.Add(Value);
		Left?.ToList(ref list);
		Right?.ToList(ref list);
	}
}

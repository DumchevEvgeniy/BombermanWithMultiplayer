using System;

public class RelatedNode : DirectedNode {
    public Cell PreviousNode { get; set; }

    public RelatedNode(Int32 indexRow, Int32 indexColumn) : base(indexRow, indexColumn) { }
    public RelatedNode(Cell node) : this(node.IndexRow, node.IndexColumn) { }
}
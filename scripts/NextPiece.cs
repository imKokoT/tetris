using Godot;
using Pieces;
using System;

public partial class NextPiece : TileMapLayer
{
    Piece _prev;

    public override void _Process(double delta)
    {
        var gridData = GameData.Instance.GridData;

        if (gridData.NextPiece != _prev)
        {
            _prev = gridData.NextPiece;
            if (gridData.NextPiece is I) GD.Print("I");
            else if (gridData.NextPiece is L) GD.Print("L");
            else if (gridData.NextPiece is J) GD.Print("J");
            else if (gridData.NextPiece is S) GD.Print("S");
            else if (gridData.NextPiece is Z) GD.Print("Z");
            else if (gridData.NextPiece is T) GD.Print("T");
            else if (gridData.NextPiece is O) GD.Print("O");
        }
    }

    private void Show(Block[,] data)
    {

    }
}

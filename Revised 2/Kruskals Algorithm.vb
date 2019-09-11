Module Kruskals_Algorithm
    Function Kruskals(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim CellSet As New Dictionary(Of Cell, Integer)
        Dim AvailableCells As New List(Of Cell)
        Dim SetNumber As Integer = 1
        Dim Returnpath As New List(Of Node)
        Dim EdgeWeights As New Dictionary(Of Cell, Integer)
        Dim R As New Random
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                'Assigning each available cell a unique set
                CellSet(New Cell(x, y)) = SetNumber
                SetNumber += 1
                AvailableCells.Add(New Cell(x, y))
                'Assigning edge weights
                If x < Limits(2) - 2 And x + 2 <> Limits(2) - 1 Then
                    EdgeWeights(New Cell(x + 2, y)) = R.Next(0, 99)
                End If
                If y < Limits(3) - 1 Then
                    EdgeWeights(New Cell(x, y + 1)) = R.Next(0, 99)
                End If
            Next
        Next
        SetBoth(ConsoleColor.White)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While EdgeWeights.Count > 0
            If ExitCase() Then Return Nothing
            'find the edge with the lowest weight
            Dim HighestWeightEdge As Cell = EdgeWeights.Keys(R.Next(0, EdgeWeights.Count))
            'For Each cell In EdgeWeights
            '    If EdgeWeights(HighestWeightCell) < EdgeWeights(cell.Key) Then HighestWeightCell = cell.Key
            'Next
            'TempLowest is now the key with the lowest value in the EdgeWeights dictionary
            Dim WallCell As Cell = HighestWeightEdge
            'need to find the two adjacent cells
            Dim AdjacentCells As New List(Of Cell)
            If AvailableCells.Contains(New Cell(WallCell.X, WallCell.Y - 1)) Then AdjacentCells.Add(New Cell(WallCell.X, WallCell.Y - 1))
            If AvailableCells.Contains(New Cell(WallCell.X + 2, WallCell.Y)) Then AdjacentCells.Add(New Cell(WallCell.X + 2, WallCell.Y))
            If AvailableCells.Contains(New Cell(WallCell.X, WallCell.Y + 1)) Then AdjacentCells.Add(New Cell(WallCell.X, WallCell.Y + 1))
            If AvailableCells.Contains(New Cell(WallCell.X - 2, WallCell.Y)) Then AdjacentCells.Add(New Cell(WallCell.X - 2, WallCell.Y))
            If CellSet(AdjacentCells(1)) <> CellSet(AdjacentCells(0)) Then
                If ShowMazeGeneration Then WallCell.Print("██")
                Returnpath.Add(New Node(WallCell.X, WallCell.Y))
                Dim SetNumToBeChanged As Integer = CellSet(AdjacentCells(1))
                Dim CellsToBeChanged As New List(Of Cell)
                For Each thing In CellSet
                    If thing.Value = SetNumToBeChanged Then CellsToBeChanged.Add(thing.Key)
                Next
                For Each thing In CellsToBeChanged
                    CellSet(thing) = CellSet(AdjacentCells(0))
                Next
                For Each cell In AdjacentCells
                    If ShowMazeGeneration Then cell.Print("██")
                    If Not Returnpath.Contains(New Node(cell.X, cell.Y)) Then Returnpath.Add(New Node(cell.X, cell.Y))
                Next
            End If
            EdgeWeights.Remove(WallCell)
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(Returnpath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop + 5
        AddStartAndEnd(Returnpath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Returnpath
    End Function
End Module

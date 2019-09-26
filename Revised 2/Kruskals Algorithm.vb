Module KruskalsAlgorithm
    Function Kruskals(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim cellSet As New Dictionary(Of Cell, Integer)
        Dim availableCells As New List(Of Cell)
        Dim setNumber = 1
        Dim returnpath As New List(Of Node)
        Dim edgeWeights As New Dictionary(Of Cell, Integer)
        Dim r As New Random
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                'Assigning each available cell a unique set
                cellSet(New Cell(x, y)) = setNumber
                setNumber += 1
                availableCells.Add(New Cell(x, y))
                'Assigning edge weights
                If x < limits(2) - 2 And x + 2 <> limits(2) - 1 Then
                    edgeWeights(New Cell(x + 2, y)) = r.Next(0, 99)
                End If
                If y < limits(3) - 1 Then
                    edgeWeights(New Cell(x, y + 1)) = r.Next(0, 99)
                End If
            Next
        Next
        SetBoth(ConsoleColor.White)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While edgeWeights.Count > 0
            If ExitCase() Then Return Nothing
            'find the edge with the lowest weight
            Dim highestWeightEdge As Cell = edgeWeights.Keys(r.Next(0, edgeWeights.Count))
            'For Each cell In EdgeWeights
            '    If EdgeWeights(HighestWeightCell) < EdgeWeights(cell.Key) Then HighestWeightCell = cell.Key
            'Next
            'TempLowest is now the key with the lowest value in the EdgeWeights dictionary
            Dim wallCell As Cell = highestWeightEdge
            'need to find the two adjacent cells
            Dim adjacentCells As New List(Of Cell)
            If availableCells.Contains(New Cell(wallCell.X, wallCell.Y - 1)) Then adjacentCells.Add(New Cell(wallCell.X, wallCell.Y - 1))
            If availableCells.Contains(New Cell(wallCell.X + 2, wallCell.Y)) Then adjacentCells.Add(New Cell(wallCell.X + 2, wallCell.Y))
            If availableCells.Contains(New Cell(wallCell.X, wallCell.Y + 1)) Then adjacentCells.Add(New Cell(wallCell.X, wallCell.Y + 1))
            If availableCells.Contains(New Cell(wallCell.X - 2, wallCell.Y)) Then adjacentCells.Add(New Cell(wallCell.X - 2, wallCell.Y))
            If cellSet(adjacentCells(1)) <> cellSet(adjacentCells(0)) Then
                If showMazeGeneration Then wallCell.Print("██")
                returnpath.Add(New Node(wallCell.X, wallCell.Y))
                Dim setNumToBeChanged As Integer = cellSet(adjacentCells(1))
                Dim cellsToBeChanged As New List(Of Cell)
                For Each thing In cellSet
                    If thing.Value = setNumToBeChanged Then cellsToBeChanged.Add(thing.Key)
                Next
                For Each thing In cellsToBeChanged
                    cellSet(thing) = cellSet(adjacentCells(0))
                Next
                For Each cell In adjacentCells
                    If showMazeGeneration Then cell.Print("██")
                    If Not returnpath.Contains(New Node(cell.X, cell.Y)) Then returnpath.Add(New Node(cell.X, cell.Y))
                Next
            End If
            edgeWeights.Remove(wallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(returnpath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop + 5
        AddStartAndEnd(returnpath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return returnpath
    End Function
End Module

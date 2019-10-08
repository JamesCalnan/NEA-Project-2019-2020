Module KruskalsAlgorithm
    Function Kruskals(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour as consolecolor, backGroundColour as consolecolor, rule as String)
        dim tempLimits() = {limits(0),limits(1),limits(2),limits(3)}
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,tempLimits)
        Dim cellSet As New Dictionary(Of Cell, Integer)
        Dim availableCells As New List(Of Cell)
        Dim setNumber = 1
        Dim returnpath As New List(Of Node)
        Dim edges As New List(Of Cell)
        Dim edgeWeights As New PriorityQueue(Of Node) 'Dictionary(Of Cell, Integer)
        Dim r As New Random
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                'Assigning each available cell a unique set
                cellSet(New Cell(x, y)) = setNumber
                setNumber += 1
                availableCells.Add(New Cell(x, y))
                'Assigning edge weights
                If x < limits(2) - 2 And x + 2 <> limits(2) - 1 Then
                    'edgeWeights(New Cell(x + 2, y)) = r.Next(0, 99)
                    edges.Add(New Cell(x + 2, y))
                    edgeWeights.Enqueue(New Node(x + 2, y), r.Next(0, 99))
                End If
                If y < limits(3) - 1 Then
                    'edgeWeights(New Cell(x, y + 1)) = r.Next(0, 99)
                    edges.Add(New Cell(x, y + 1))
                    edgeWeights.Enqueue(New Node(x, y + 1), r.Next(0, 99))
                End If
            Next
        Next
        SetBoth(pathColour)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While Not edgeWeights.IsEmpty() Or edges.Count > 0 'edgeWeights.Count > 0
            If ExitCase() Then Return Nothing
            'find the edge with the lowest weight
             Dim highestWeightEdge As Cell
            If rule = "simplified" Then
                If edges.Count = 0 Then Exit While
                highestWeightEdge = edges(r.Next(edges.Count)) 'edgeWeights.Keys(r.Next(0, edgeWeights.Count))
            Else
                Dim tempCell As Cell = edgeWeights.ExtractMin().ToCell() 'edgeWeights.Keys(0)
                'For Each cell In From cell1 In edgeWeights Where edgeWeights(tempCell) < edgeWeights(cell1.Key)
                '    tempCell = cell.Key
                'Next
                highestWeightEdge = tempCell
            End If
            'TempLowest is now the key with the lowest value in the EdgeWeights dictionary
            Dim wallCell As Cell = highestWeightEdge
            Dim adjacentCells As New List(Of Cell)
            If availableCells.Contains(New Cell(wallCell.X, wallCell.Y - 1)) Then adjacentCells.Add(New Cell(wallCell.X, wallCell.Y - 1))
            If availableCells.Contains(New Cell(wallCell.X + 2, wallCell.Y)) Then adjacentCells.Add(New Cell(wallCell.X + 2, wallCell.Y))
            If availableCells.Contains(New Cell(wallCell.X, wallCell.Y + 1)) Then adjacentCells.Add(New Cell(wallCell.X, wallCell.Y + 1))
            If availableCells.Contains(New Cell(wallCell.X - 2, wallCell.Y)) Then adjacentCells.Add(New Cell(wallCell.X - 2, wallCell.Y))
            If cellSet(adjacentCells(1)) <> cellSet(adjacentCells(0)) Then
                If showMazeGeneration Then wallCell.Print("██")
                returnpath.Add(New Node(wallCell.X, wallCell.Y))
                Dim setNumToBeChanged As Integer = cellSet(adjacentCells(1))
                Dim cellsToBeChanged As List(Of Cell) = (From thing In cellSet Where thing.Value = setNumToBeChanged Select thing.Key).ToList()
                For Each thing In cellsToBeChanged
                    cellSet(thing) = cellSet(adjacentCells(0))
                Next
                For Each cell In adjacentCells
                    If showMazeGeneration Then cell.Print("██")
                    If Not returnpath.Contains(New Node(cell.X, cell.Y)) Then returnpath.Add(New Node(cell.X, cell.Y))
                Next
            End If
            If rule = "simplified" Then edges.Remove(wallCell)
            'edgeWeights.Remove(wallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnpath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop + 5
        AddStartAndEnd(returnpath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return returnpath
    End Function
End Module

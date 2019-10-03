Module BoruvkasAlgorithm

    'video: https://www.youtube.com/watch?v=czcf73b0Ga0


    Function BoruvkasAlgorithm(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor, rule As String)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim T As New Dictionary(Of Cell, Integer)
        Dim availableCells As New List(Of Cell)
        Dim setNumber = 1
        Dim returnPath As New List(Of Node)
        Dim edgeWeights As New Dictionary(Of Cell, Integer)
        Dim r As New Random
        SetBoth(ConsoleColor.White)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                'Assigning each available cell a unique set
                T(New Cell(x, y)) = setNumber
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
        Dim iteration As Integer = 0
        If rule = "shuffle" Then ShuffleCells(availableCells)
        Console.BackgroundColor = ConsoleColor.Black
        Console.SetCursorPosition(0, 0)
        Console.Write($"iteration: {iteration}")
        While UniqueComponentNumber(T)
            Dim cellEdge As New Dictionary(Of Cell, Cell)
            Dim edgesUsed As New List(Of Cell)
            For Each Vertex In availableCells
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    Vertex.Print("XX")
                    Threading.Thread.Sleep(delay \ 2)
                End If
                If Not returnPath.Contains(Vertex.ToNode()) Then returnPath.Add(Vertex.ToNode())
                Dim chosenEdge = FindLowestEdge(Vertex, edgeWeights, availableCells, T, returnPath, showMazeGeneration, delay)
                If IsNothing(chosenEdge) Then Continue For
                edgesUsed.Add(chosenEdge)
                cellEdge(Vertex) = chosenEdge
                Dim adjacentCells As New List(Of Cell)
                If availableCells.Contains(New Cell(chosenEdge.X, chosenEdge.Y - 1)) Then adjacentCells.Add(New Cell(chosenEdge.X, chosenEdge.Y - 1))
                If availableCells.Contains(New Cell(chosenEdge.X + 2, chosenEdge.Y)) Then adjacentCells.Add(New Cell(chosenEdge.X + 2, chosenEdge.Y))
                If availableCells.Contains(New Cell(chosenEdge.X, chosenEdge.Y + 1)) Then adjacentCells.Add(New Cell(chosenEdge.X, chosenEdge.Y + 1))
                If availableCells.Contains(New Cell(chosenEdge.X - 2, chosenEdge.Y)) Then adjacentCells.Add(New Cell(chosenEdge.X - 2, chosenEdge.Y))
                If T(adjacentCells(1)) <> T(adjacentCells(0)) Then
                    Dim setNumToBeChanged As Integer = T(adjacentCells(1))
                    Dim cellsToBeChanged As List(Of Cell) = (From thing In T Where thing.Value = setNumToBeChanged Select thing.Key).ToList()
                    For Each thing In cellsToBeChanged
                        T(thing) = T(adjacentCells(0))
                    Next
                End If
            Next
            For Each edge In edgesUsed
                edgeWeights.Remove(edge)
            Next
            Console.BackgroundColor = ConsoleColor.Black
            Console.SetCursorPosition(0, 0)
            iteration += 1
            Console.Write($"iteration: {iteration}")
        End While
        AddStartAndEnd(returnPath, limits, pathColour)
        Return returnPath
    End Function
    Sub ShuffleCells(ByRef list As List(Of Cell))
        Dim returnList As New List(Of Cell)
        Dim r As New Random
        Do
            Dim temp = list(r.Next(list.Count))
            list.Remove(temp)
            returnList.Add(temp)
        Loop Until list.Count = 0
        list = returnList
    End Sub
    Function sameCellSet(availableCells As List(Of Cell), chosenEdge As Cell, T As Dictionary(Of Cell, Integer))
        Dim adjacentCells As New List(Of Cell)
        If availableCells.Contains(New Cell(chosenEdge.X, chosenEdge.Y - 1)) Then adjacentCells.Add(New Cell(chosenEdge.X, chosenEdge.Y - 1))
        If availableCells.Contains(New Cell(chosenEdge.X + 2, chosenEdge.Y)) Then adjacentCells.Add(New Cell(chosenEdge.X + 2, chosenEdge.Y))
        If availableCells.Contains(New Cell(chosenEdge.X, chosenEdge.Y + 1)) Then adjacentCells.Add(New Cell(chosenEdge.X, chosenEdge.Y + 1))
        If availableCells.Contains(New Cell(chosenEdge.X - 2, chosenEdge.Y)) Then adjacentCells.Add(New Cell(chosenEdge.X - 2, chosenEdge.Y))
        If T(adjacentCells(1)) = T(adjacentCells(0)) Then
            Return True
        End If
        Return False
    End Function

    Function FindLowestEdge(ByVal centreCell As Cell, ByVal edgeWeights As Dictionary(Of Cell, Integer), availableCells As List(Of Cell), T As Dictionary(Of Cell, Integer), ByRef returnPath As List(Of Node), showMazeGeneration As Boolean, delayMS As Integer) As Cell
        Dim left As New Cell(centreCell.X - 2, centreCell.Y)
        Dim up As New Cell(centreCell.X, centreCell.Y - 1)
        Dim right As New Cell(centreCell.X + 2, centreCell.Y)
        Dim down As New Cell(centreCell.X, centreCell.Y + 1)
        Dim neighbours As New List(Of Cell)
        If edgeWeights.ContainsKey(left) Then neighbours.Add(left)
        If edgeWeights.ContainsKey(up) Then neighbours.Add(up)
        If edgeWeights.ContainsKey(right) Then neighbours.Add(right)
        If edgeWeights.ContainsKey(down) Then neighbours.Add(down)
        If neighbours.Count = 0 Then Return Nothing
        Dim lowestEdge As Cell = neighbours(0)
        Dim changed As Boolean = False
        For Each edge In neighbours
            If Not sameCellSet(availableCells, edge, T) Then
                If edgeWeights(edge) <= edgeWeights(lowestEdge) Then
                    lowestEdge = edge
                    changed = True
                End If
            Else
            End If
        Next
        If Not changed Then Return Nothing
        If showMazeGeneration Then
            lowestEdge.Print("XX")
            Threading.Thread.Sleep(delayMS \ 2)
        End If
        If Not returnPath.Contains(lowestEdge.ToNode()) Then returnPath.Add(lowestEdge.ToNode())
        Return lowestEdge
    End Function
    Function UniqueComponentNumber(ByVal cellSets As Dictionary(Of Cell, Integer))
        For i = 1 To cellSets.Values.Count - 2
            If cellSets.Values(i) <> cellSets.Values(i - 1) Then Return True
        Next
        Return False
    End Function
End Module

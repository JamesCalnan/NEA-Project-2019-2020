Module Reverse_Delete_Algorithm
    Function reverseDeleteAlgorithm(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor, searchingAlgorithm As String)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim T As New Dictionary(Of Cell, Integer)
        Dim availableCells, availableEdges As New List(Of Cell)
        Dim returnPath As New List(Of Node)
        Dim edgeWeights As New Dictionary(Of Cell, Integer)
        Dim r As New Random
        SetBoth(pathColour)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                'Assigning each available cell a unique set
                availableCells.Add(New Cell(x, y))
                returnPath.Add(New Node(x, y))
                If showMazeGeneration Then
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                End If
                'Assigning edge weights
                If x < limits(2) - 2 And x + 2 <> limits(2) - 1 Then
                    edgeWeights(New Cell(x + 2, y)) = r.Next(0, 99)
                    availableEdges.Add(New Cell(x + 2, y))
                    returnPath.Add(New Node(x + 2, y))
                    If showMazeGeneration Then
                        Console.SetCursorPosition(x + 2, y)
                        Console.Write("XX")
                    End If
                End If
                If y < limits(3) - 1 Then
                    edgeWeights(New Cell(x, y + 1)) = r.Next(0, 99)
                    returnPath.Add(New Node(x, y + 1))
                    If showMazeGeneration Then
                        Console.SetCursorPosition(x, y + 1)
                        Console.Write("XX")
                    End If
                    availableEdges.Add(New Cell(x, y + 1))
                End If
            Next
        Next
        SetBoth(backGroundColour)
        While availableEdges.Count > 0
            If ExitCase() Then Return Nothing
            Dim highestWeightEdge = FindHighestEdgeWeight(edgeWeights, availableEdges)
            Dim adjacentCells As New List(Of Cell)
            If availableCells.Contains(New Cell(highestWeightEdge.X, highestWeightEdge.Y - 1)) Then adjacentCells.Add(New Cell(highestWeightEdge.X, highestWeightEdge.Y - 1))
            If availableCells.Contains(New Cell(highestWeightEdge.X + 2, highestWeightEdge.Y)) Then adjacentCells.Add(New Cell(highestWeightEdge.X + 2, highestWeightEdge.Y))
            If availableCells.Contains(New Cell(highestWeightEdge.X, highestWeightEdge.Y + 1)) Then adjacentCells.Add(New Cell(highestWeightEdge.X, highestWeightEdge.Y + 1))
            If availableCells.Contains(New Cell(highestWeightEdge.X - 2, highestWeightEdge.Y)) Then adjacentCells.Add(New Cell(highestWeightEdge.X - 2, highestWeightEdge.Y))
            If adjacentCells.Count = 0 Then Exit While
            Dim v1 = adjacentCells(0)
            Dim v2 = adjacentCells(1)
            availableEdges.Remove(highestWeightEdge)
            Dim connectedVertices As Boolean = False
            If searchingAlgorithm = "bfs" Then
                connectedVertices = connectedVerticesBfs(availableEdges, availableCells, v1, v2)
            ElseIf searchingAlgorithm = "dfs" Then
                connectedVertices = connectedVerticesDfs(availableEdges, availableCells, v1, v2)
            ElseIf searchingAlgorithm = "bestfs" Then
                connectedVertices = connectedVerticesBestfs(availableEdges, availableCells, v1, v2)
            End If
            If connectedVertices Then
                returnPath.Remove(highestWeightEdge.ToNode)
                If showMazeGeneration Then
                    highestWeightEdge.Print("XX")
                    Threading.Thread.Sleep(delay)
                End If
            End If
        End While
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnPath, limits(2), limits(3))
        End If
        AddStartAndEnd(returnPath, limits, pathColour)
        Return returnPath
    End Function
    Function connectedVerticesBestfs(edgeList As List(Of Cell), vertexList As List(Of Cell), v1 As Cell, v2 As Cell)
        Dim discovered As New Dictionary(Of Cell, Boolean)
        Dim q As New PriorityQueue(Of Node)
        Dim g As New List(Of Cell)
        g.AddRange(edgeList)
        g.AddRange(vertexList)
        For Each node In g
            discovered(node) = False
        Next
        q.Enqueue(v1.ToNode())
        discovered(v1) = True
        While Not q.IsEmpty()
            Dim v = q.ExtractMin().ToCell()
            If v.Equals(v2) Then Return True
            For Each w As Cell In GetNeighboursCell(v, g)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w.ToNode(), H(w.ToNode(), v2.ToNode(), 1))
                End If
            Next
        End While
        Return False
    End Function
    Function connectedVerticesDfs(edgeList As List(Of Cell), vertexList As List(Of Cell), v1 As Cell, v2 As Cell)
        Dim visited As New Dictionary(Of Cell, Boolean)
        Dim g As New List(Of Cell)
        g.AddRange(edgeList)
        g.AddRange(vertexList)
        Dim s As New Stack(Of Cell)
        For Each v In g
            visited(v) = False
        Next
        s.Push(v1)
        While s.Count > 0
            Dim u = s.Pop()
            If u.Equals(v2) Then Return True
            If Not visited(u) Then
                visited(u) = True
                For Each w In GetNeighboursCell(u, g)
                    If Not visited(w) Then
                        s.Push(w)
                    End If
                Next
            End If
        End While
        Return False
    End Function
    Function connectedVerticesBfs(edgeList As List(Of Cell), vertexList As List(Of Cell), v1 As Cell, v2 As Cell)
        Dim discovered As New Dictionary(Of Cell, Boolean)
        Dim q As New Queue(Of Cell)
        Dim g As New List(Of Cell)
        g.AddRange(edgeList)
        g.AddRange(vertexList)
        For Each node In g
            discovered(node) = False
        Next
        q.Enqueue(v1)
        discovered(v1) = True
        While q.Count > 0
            Dim v = q.Dequeue()
            If v.Equals(v2) Then Return True
            For Each w As Cell In GetNeighboursCell(v, g)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w)
                End If
            Next
        End While
        Return False
    End Function
    Function FindHighestEdgeWeight(edgeWeights As Dictionary(Of Cell, Integer), availableEdges As List(Of Cell)) As Cell
        Dim returnEdge As Cell = availableEdges(0)
        For Each edge In From edge1 In availableEdges Where edgeWeights(edge1) >= edgeWeights(returnEdge)
            returnEdge = edge
        Next
        Return returnEdge
    End Function
End Module

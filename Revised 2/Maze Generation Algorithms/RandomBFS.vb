Module RandomBFS
    Function RandomisedBFS(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim fullMaze As New List(Of Node)
        Dim visited = InitialiseVisited(limits)
        Dim r As New Random
        Dim cameFrom As New Dictionary(Of Cell, Cell)
        For Each node In visited.Keys
            cameFrom(node) = Nothing
        Next
        Dim frontierSet As New List(Of Cell) From {
                PickRandomStartingCell(limits)
        }
        cameFrom(frontierSet(0)) = frontierSet(0)
        visited(frontierSet(0)) = True
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        SetBoth(pathColour)
        While frontierSet.Count > 0
            If ExitCase() Then Return Nothing
            Dim v = dequeueRandom(frontierSet, r)
            If IsNothing(v) Then Return Nothing
            Dim wall = MidPoint(v, cameFrom(v))
            fullMaze.Add(wall.tonode)
            fullMaze.Add(v.tonode)
            If showMazeGeneration Then
                wall.Print("XX")
                v.print("XX")
                Threading.Thread.Sleep(delay)
            End If
            For Each w In Neighbour(v, visited, limits)
                If Not visited(w) Then
                    visited(w) = True
                    frontierSet.Add(w)
                    cameFrom(w) = v
                End If
            Next
        End While
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(fullMaze, limits(2), limits(3))
        End If
        AddStartAndEnd(fullMaze, limits, pathColour)
        Return fullMaze
    End Function

    Function dequeueRandom(frontierSet As List(Of Cell), r As Random)
        If frontierSet.Count = 0 Then Return Nothing
        Dim temp = frontierSet(r.Next(frontierSet.Count))
        frontierSet.Remove(temp)
        Return temp
    End Function
End Module

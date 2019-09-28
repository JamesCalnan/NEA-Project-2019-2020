Module CustomAlgorithm
    Function Custom(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour as consolecolor, backGroundColour as consolecolor)
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,limits)
        Dim r As New Random
        Dim frontierSet, recentFrontierSet As New List(Of Cell)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim wallCell As Cell
        Dim index As Integer
        Dim returnablePath As New List(Of Node)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        visitedCells(currentCell) = True
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(pathColour)
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(currentCell, visitedCells, limits, True)
                If Not frontierSet.Contains(cell) Then frontierSet.Add(cell)
                recentFrontierSet.Add(cell)
            Next
            If recentFrontierSet.Count > 0 Then
                index = r.Next(0, recentFrontierSet.Count)
                currentCell = recentFrontierSet(index)
            Else
                If frontierSet.Count = 0 Then Exit While
                index = frontierSet.Count - 1 'R.Next(0, FrontierSet.Count)
                currentCell = frontierSet(index)
            End If
            Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
            Dim previousCell As Cell = PickAdjancentCell(currentCell, adjancencyList)
            wallCell = MidPoint(currentCell, previousCell)
            If showMazeGeneration Then
                SetBoth(pathColour)
                wallCell.Print("██")
                currentCell.Print("██")
            End If
            visitedCells(currentCell) = True
            frontierSet.Remove(currentCell)
            returnablePath.Add(New Node(wallCell.X, wallCell.Y))
            returnablePath.Add(New Node(currentCell.X, currentCell.Y))
            Threading.Thread.Sleep(delay)
            recentFrontierSet.Clear()
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module

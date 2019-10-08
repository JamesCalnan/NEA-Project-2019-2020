Module GrowingTreeAlgorithm
    Function GrowingTree(limits() As Integer, delay As Integer, cellSelectionMethod() As Integer, showMazeGeneration As Boolean, pathColour as consolecolor, backGroundColour as consolecolor)
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,limits)
        Dim r As New Random
        Dim frontierSet, recentFrontierSet As New List(Of Cell)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim previousCell As Cell = currentCell
        Dim wallCell As Cell
        Dim index As Integer
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim returnablePath As New List(Of Node)
        visitedCells(currentCell) = True
        If showMazeGeneration Then
            SetBoth(pathColour)
            currentCell.Print("██")
        End If
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(currentCell, visitedCells, limits)
                If Not frontierSet.Contains(cell) Then frontierSet.Add(cell)
                recentFrontierSet.Add(cell)
            Next
            Dim randomNumber As Integer
            If cellSelectionMethod(0) = 1 Then
                'Recursive Backtracker
                If recentFrontierSet.Count > 0 Then
                    index = r.Next(0, recentFrontierSet.Count)
                    currentCell = recentFrontierSet(index)
                Else
                    If frontierSet.Count = 0 Then Exit While
                    index = frontierSet.Count - 1
                    currentCell = frontierSet(index)
                    Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                    previousCell = PickAdjancentCell(currentCell, adjancencyList)
                End If
            ElseIf cellSelectionMethod(1) = 1 Then
                'Prim's
                If frontierSet.Count = 0 Then Exit While
                index = r.Next(0, frontierSet.Count)
                currentCell = frontierSet(index)
                Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                previousCell = PickAdjancentCell(currentCell, adjancencyList)
            ElseIf cellSelectionMethod(2) = 1 Or cellSelectionMethod(3) = 1 Or cellSelectionMethod(4) = 1 Then
                Dim chance As Integer
                If cellSelectionMethod(2) = 1 Then
                    '75/25 split
                    chance = 75
                ElseIf cellSelectionMethod(3) = 1 Then
                    '50/50 split
                    chance = 50
                ElseIf cellSelectionMethod(4) = 1 Then
                    '25/75 split
                    chance = 25
                End If
                randomNumber = r.Next(1, 101)
                If randomNumber < chance Then
                    'Newest
                    If recentFrontierSet.Count > 0 Then
                        index = r.Next(0, recentFrontierSet.Count)
                        currentCell = recentFrontierSet(index)
                    Else
                        If frontierSet.Count = 0 Then Exit While
                        index = r.Next(0, frontierSet.Count)
                        currentCell = frontierSet(index)
                        Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                        previousCell = PickAdjancentCell(currentCell, adjancencyList)
                    End If
                Else
                    'Random
                    If frontierSet.Count = 0 Then Exit While
                    index = r.Next(0, frontierSet.Count)
                    currentCell = frontierSet(index)
                    Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                    previousCell = PickAdjancentCell(currentCell, adjancencyList)
                End If
            ElseIf cellSelectionMethod(5) = 1 Then
                'Oldest
                If frontierSet.Count = 0 Then Exit While
                index = 0
                currentCell = frontierSet(index)
                Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                previousCell = PickAdjancentCell(currentCell, adjancencyList)
            ElseIf cellSelectionMethod(6) = 1 Then
                'Middle
                If frontierSet.Count = 0 Then Exit While
                index = frontierSet.Count / 2
                currentCell = frontierSet(index)
                Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                previousCell = PickAdjancentCell(currentCell, adjancencyList)
            ElseIf cellSelectionMethod(7) = 1 Then
                'Newest/Oldest, 50/50 split
                randomNumber = r.Next(1, 101)
                If randomNumber > 50 Then
                    'Newest
                    If recentFrontierSet.Count > 0 Then
                        index = r.Next(0, recentFrontierSet.Count)
                        currentCell = recentFrontierSet(index)
                    Else
                        If frontierSet.Count = 0 Then Exit While
                        index = r.Next(0, frontierSet.Count)
                        currentCell = frontierSet(index)
                        Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                        previousCell = PickAdjancentCell(currentCell, adjancencyList)
                    End If
                Else
                    'Oldest
                    If frontierSet.Count = 0 Then Exit While
                    index = 0 'R.Next(0, FrontierSet.Count)
                    currentCell = frontierSet(index)
                    Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                    previousCell = PickAdjancentCell(currentCell, adjancencyList)
                End If
            ElseIf cellSelectionMethod(8) = 1 Then
                'Oldest/Random, 50/50 split
                randomNumber = r.Next(1, 101)
                If randomNumber > 50 Then
                    'Oldest
                    If frontierSet.Count = 0 Then Exit While
                    index = 0
                    currentCell = frontierSet(index)
                    Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                    previousCell = PickAdjancentCell(currentCell, adjancencyList)
                Else
                    'Random
                    If frontierSet.Count = 0 Then Exit While
                    index = r.Next(0, frontierSet.Count)
                    currentCell = frontierSet(index)
                    Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
                    previousCell = PickAdjancentCell(currentCell, adjancencyList)
                End If
            End If
            wallCell = MidPoint(currentCell, previousCell)
            If showMazeGeneration Then
                SetBoth(pathColour)
                wallCell.Print("██")
                currentCell.Print("██")
            End If
            visitedCells(currentCell) = True
            frontierSet.Remove(currentCell)
            AddToPath(returnablePath, currentCell, wallCell)
            Threading.Thread.Sleep(delay)
            recentFrontierSet.Clear()
            previousCell = currentCell
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module

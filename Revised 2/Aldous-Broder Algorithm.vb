Module AldousBroder
    Function AldousBroder(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour as consolecolor, backGroundColour as consolecolor)
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,limits)
        Dim totalCellCount As Integer
        Dim r As New Random
        Dim recentCells As New List(Of Cell)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim prevCell As Cell = PickRandomStartingCell(limits)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim wallCell As Cell
        Dim fullMaze As New List(Of Node)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                totalCellCount += 1
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(pathcolour)
        visitedCells(currentCell) = True
        fullMaze.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        Dim usedCellCount = 1
        While usedCellCount <> totalCellCount
            If ExitCase() Then Return Nothing
            recentCells.Clear()
            recentCells = RanNeighbour(currentCell, limits)
            Dim index As Integer = r.Next(0, recentCells.Count)
            Dim temporaryCell As Cell = recentCells(index)
            If Not visitedCells(temporaryCell) Then
                visitedCells(New Cell(temporaryCell.X, temporaryCell.Y)) = True
                usedCellCount += 1
                wallCell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                fullMaze.Add(New Node(wallCell.X, wallCell.Y))
                fullMaze.Add(New Node(temporaryCell.X, temporaryCell.Y))
                If showMazeGeneration Then
                    SetBoth(pathcolour)
                    prevCell.Print("██")
                    wallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            Else
                currentCell = temporaryCell
                If showMazeGeneration Then
                    SetBoth(pathcolour)
                    prevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathcolour)
            PrintMazeHorizontally(fullMaze, limits(2), limits(3))
        Else
            SetBoth(pathcolour)
            prevCell.Print("██")
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(fullMaze, limits, pathcolour)
        Console.SetCursorPosition(0, ypos)
        Return fullMaze
    End Function
End Module

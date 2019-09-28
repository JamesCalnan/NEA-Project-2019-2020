Module RecursiveBacktracker
    Function RecursiveBacktracker(limits() As Integer, delay As Integer, showMazeGeneration As Boolean,  pathColour as Consolecolor,  backGroundColour as ConsoleColor)
        Dim r As New Random
        If backgroundcolour <> ConsoleColor.Black Then DrawBackground(backGroundColour,limits)
        SetBoth(pathColour)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim prevCell As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim stack As New Stack(Of Cell)
        Dim returnablePath As New List(Of Node)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        visitedCells(currentCell) = True
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(pathColour)
            End If
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits, True)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
                visitedCells(temporaryCell) = True
                stack.Push(New Cell(temporaryCell.X, temporaryCell.Y))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, temporaryCell, wallCell)
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    prevCell.Print("██")
                    wallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            ElseIf stack.Count > 1 Then
                currentCell = stack.Pop
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    prevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    currentCell.Print("██")
                    SetBoth(pathColour)
                    prevCell = currentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathcolour)
        'Unicursal(returnablePath)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
    Function RecursiveBacktrackerRecursively(cell As Cell, limits() As Integer, path As List(Of Node), ByRef visited As Dictionary(Of Cell, Boolean), ByRef cameFrom As Cell, r As Random, showMazeGeneration As Boolean, delay As Integer, pathColour as consolecolor)
        Dim recentCells As List(Of Cell) = Neighbour(cell, visited, limits, True)
        If recentCells.Count > 0 Then
            Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
            Dim wall As Cell = MidPoint(cameFrom, temporaryCell)
            If showMazeGeneration Then
                wall.Print("██")
                temporaryCell.Print("██")
                Threading.Thread.Sleep(delay) : End If
            AddToPath(path, temporaryCell, wall)
            cameFrom = temporaryCell
            visited(temporaryCell) = True
            RecursiveBacktrackerRecursively(temporaryCell, limits, path, visited, cameFrom, r, showMazeGeneration, delay,pathColour)
        Else
            Return Nothing
        End If
        cameFrom = cell
        RecursiveBacktrackerRecursively(cell, limits, path, visited, cameFrom, r, showMazeGeneration, delay,pathColour)
        Return path
    End Function
End Module
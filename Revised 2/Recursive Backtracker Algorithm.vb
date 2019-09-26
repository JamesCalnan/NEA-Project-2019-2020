Module RecursiveBacktracker
    Function RecursiveBacktracker(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim r As New Random
        Dim back = ConsoleColor.White
        If back <> ConsoleColor.White Then
            SetBoth(ConsoleColor.White)
            For y = limits(1) - 1 To limits(3) + 1
                For x = limits(0) + 1 To limits(2) + 1
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                Next
            Next
        End If
        SetBoth(back)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim prevCell As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim stack As New Stack(Of Cell)
        Dim returnablePath As New List(Of Node)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim bias() As Integer = {0, 1}
        visitedCells(currentCell) = True
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(back)
            End If
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits, True)
            'If RecentCells.Count = 0 Then RecentCells = RanNeighbour(CurrentCell, Limits)
            If recentCells.Count > 0 Then 'done
                Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
                visitedCells(temporaryCell) = True
                stack.Push(New Cell(temporaryCell.X, temporaryCell.Y))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, temporaryCell, wallCell)
                If showMazeGeneration Then
                    SetBoth(back)
                    prevCell.Print("██")
                    wallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            ElseIf stack.Count > 1 Then
                currentCell = stack.Pop
                If showMazeGeneration Then
                    SetBoth(back)
                    prevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    currentCell.Print("██")
                    SetBoth(back)
                    prevCell = currentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(back)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, 0)
        'Unicursal(returnablePath)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
    Function RecursiveBacktrackerRecursively(cell As Cell, limits() As Integer, path As List(Of Node), ByRef visited As Dictionary(Of Cell, Boolean), ByRef cameFrom As Cell, r As Random, showMazeGeneration As Boolean, delay As Integer)
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
            RecursiveBacktrackerRecursively(temporaryCell, limits, path, visited, cameFrom, r, showMazeGeneration, delay)
        Else
            Return Nothing
        End If
        cameFrom = cell
        RecursiveBacktrackerRecursively(cell, limits, path, visited, cameFrom, r, showMazeGeneration, delay)
        Return path
    End Function
End Module
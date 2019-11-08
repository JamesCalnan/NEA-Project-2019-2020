Module RecursiveBacktracker
    Function RecursiveBacktracker(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As Consolecolor, backGroundColour As ConsoleColor)
        Dim r As New Random
        If backgroundcolour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        SetBoth(pathColour)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim prevCell As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim stack As New Stack(Of Cell)
        Dim returnablePath As New List(Of Node)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        visitedCells(currentCell) = True
        stack.Push(currentCell)
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(pathColour)
            End If
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(recentCells.Count))
                visitedCells(temporaryCell) = True
                stack.Push(temporaryCell)
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
        AddStartAndEnd(returnablePath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function

    Function RecursiveBacktrackerNotUsingStack(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim r As New Random
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        SetBoth(pathColour)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim prevCell As Cell = currentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim cameFrom As New Dictionary(Of Cell, Cell)
        For Each cell In visitedCells.Keys
            cameFrom(cell) = Nothing
        Next
        Dim returnablePath As New List(Of Node)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        visitedCells(currentCell) = True
        cameFrom(currentCell) = currentCell
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(pathColour)
            End If
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(recentCells.Count))
                visitedCells(temporaryCell) = True
                cameFrom(temporaryCell) = currentCell
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
            ElseIf gridComplete(visitedCells) Then
                currentCell = cameFrom(currentCell)
                'cameFrom(currentCell) = Nothing
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
        Else
            SetBoth(pathColour)
            prevCell.Print("XX")
        End If
        AddStartAndEnd(returnablePath, limits, pathColour)
        Return returnablePath
    End Function
    Function gridComplete(dict As Dictionary(Of Cell, Boolean))
        Return dict.Any(Function(thing) Not thing.Value)
    End Function

    Function RecursiveBacktrackerRecursively(cell As Cell, limits() As Integer, path As List(Of Node), ByRef visited As Dictionary(Of Cell, Boolean), ByRef cameFrom As Cell, r As Random, showMazeGeneration As Boolean, delay As Integer, pathColour as consolecolor)
        Dim recentCells As List(Of Cell) = Neighbour(cell, visited, limits)
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
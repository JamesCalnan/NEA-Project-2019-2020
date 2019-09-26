Module SpiralBacktrackerAlgorithm
    Function SpiralBacktracker(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim r As New Random
        Dim currentCell As Cell = PickRandomStartingCell(limits)
        Dim prevCell As Cell = currentCell
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim stack As New Stack(Of Cell)
        Dim returnablePath As New List(Of Node)
        Dim recentCells As New List(Of Cell)
        Dim adding As Double = GetIntInputArrowKeys("Cell Reach: ", 100, 0, True)
        Dim currentCellReach As Double = 0
        Dim dir = "UP"
        SetBoth(ConsoleColor.White)
        visitedCells(currentCell) = True
        If showMazeGeneration Then currentCell.Print("██")
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(ConsoleColor.White)
            End If
            If Neighbour(currentCell, visitedCells, limits, False) Then 'done
                Dim tempCell As New Cell(-1, -1)
                Dim validNextCell = False
                Do
                    If dir = "UP" Then
                        tempCell.Update(currentCell.X, currentCell.Y - 2)
                        If visitedCells.ContainsKey(tempCell) AndAlso Not visitedCells(tempCell) And tempCell.WithinLimits(limits) Then
                            validNextCell = True
                        Else
                            dir = "RIGHT"
                        End If
                    ElseIf dir = "RIGHT" Then
                        tempCell.Update(currentCell.X + 4, currentCell.Y)
                        If visitedCells.ContainsKey(tempCell) AndAlso Not visitedCells(tempCell) And tempCell.WithinLimits(limits) Then
                            validNextCell = True
                        Else
                            dir = "DOWN"
                        End If
                    ElseIf dir = "DOWN" Then
                        tempCell.Update(currentCell.X, currentCell.Y + 2)
                        If visitedCells.ContainsKey(tempCell) AndAlso Not visitedCells(tempCell) And tempCell.WithinLimits(limits) Then
                            validNextCell = True
                        Else
                            dir = "LEFT"
                        End If
                    ElseIf dir = "LEFT" Then
                        tempCell.Update(currentCell.X - 4, currentCell.Y)
                        If visitedCells.ContainsKey(tempCell) AndAlso Not visitedCells(tempCell) And tempCell.WithinLimits(limits) Then
                            validNextCell = True
                        Else
                            dir = "UP"
                        End If
                    End If
                Loop Until validNextCell
                Dim temporaryCell As Cell = tempCell
                If Math.Floor(currentCellReach) = 0 Then
                    dir = "UP"
                ElseIf Math.Floor(currentCellReach) = 1 Then
                    dir = "RIGHT"
                ElseIf Math.Floor(currentCellReach) = 2 Then
                    dir = "DOWN"
                ElseIf Math.Floor(currentCellReach) = 3 Then
                    dir = "LEFT"
                End If
                currentCellReach += adding / 100
                If Math.Floor(currentCellReach) = 5 Then currentCellReach = 0
                visitedCells(temporaryCell) = True
                stack.Push(New Cell(temporaryCell.X, temporaryCell.Y))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, temporaryCell, wallCell)
                If showMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    prevCell.Print("██")
                    wallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            ElseIf stack.Count > 1 Then
                currentCell = stack.Pop 'CurrentCell.Pop(Stack)
                If showMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    prevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    currentCell.Print("██")
                    SetBoth(ConsoleColor.White)
                    prevCell = currentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module

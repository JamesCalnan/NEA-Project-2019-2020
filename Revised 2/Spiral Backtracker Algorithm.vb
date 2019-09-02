Module Spiral_Backtracker_Algorithm
    Function SpiralBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim r As New Random
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits)
        Dim PrevCell As Cell = CurrentCell
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim Stack As New Stack(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim RecentCells As New List(Of Cell)
        Dim Adding As Double = GetIntInputArrowKeys("Cell Reach: ", 100, 0, True)
        Dim CurrentCellReach As Double = 0
        Dim Dir As String = "UP"
        SetBoth(ConsoleColor.White)
        VisitedCells(CurrentCell) = True
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            If ShowMazeGeneration Then
                PrevCell.Print("██")
                SetBoth(ConsoleColor.White)
            End If
            If Neighbour(CurrentCell, VisitedCells, Limits, False) Then 'done
                Dim TempCell As New Cell(-1, -1)
                Dim ValidNextCell As Boolean = False
                Do
                    If Dir = "UP" Then
                        TempCell.Update(CurrentCell.X, CurrentCell.Y - 2)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "RIGHT"
                        End If
                    ElseIf Dir = "RIGHT" Then
                        TempCell.Update(CurrentCell.X + 4, CurrentCell.Y)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "DOWN"
                        End If
                    ElseIf Dir = "DOWN" Then
                        TempCell.Update(CurrentCell.X, CurrentCell.Y + 2)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "LEFT"
                        End If
                    ElseIf Dir = "LEFT" Then
                        TempCell.Update(CurrentCell.X - 4, CurrentCell.Y)
                        If VisitedCells.ContainsKey(TempCell) AndAlso Not VisitedCells(TempCell) And TempCell.WithinLimits(Limits) Then
                            ValidNextCell = True
                        Else
                            Dir = "UP"
                        End If
                    End If
                Loop Until ValidNextCell
                Dim TemporaryCell As Cell = TempCell
                If Math.Floor(CurrentCellReach) = 0 Then
                    Dir = "UP"
                ElseIf Math.Floor(CurrentCellReach) = 1 Then
                    Dir = "RIGHT"
                ElseIf Math.Floor(CurrentCellReach) = 2 Then
                    Dir = "DOWN"
                ElseIf Math.Floor(CurrentCellReach) = 3 Then
                    Dir = "LEFT"
                End If
                CurrentCellReach += Adding / 100
                If Math.Floor(CurrentCellReach) = 5 Then CurrentCellReach = 0
                VisitedCells(TemporaryCell) = True
                Stack.Push(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                AddToPath(ReturnablePath, TemporaryCell, WallCell)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = Stack.Pop 'CurrentCell.Pop(Stack)
                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    PrevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    CurrentCell.Print("██")
                    SetBoth(ConsoleColor.White)
                    PrevCell = CurrentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(ReturnablePath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
End Module

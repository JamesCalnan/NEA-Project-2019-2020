Module Houstons_Algorithm
    Function Houstons(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim Direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim VisitedList, RecentCells As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        VisitedList.Add(CurrentCell)
        Dim PrevCell As Cell = PickRandomStartingCell(Limits)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        Dim AvailableCells As New List(Of Cell)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                TotalCellCount += 1
                AvailableCells.Add(New Cell(x, y))
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim InitialiseWilsonCell As Boolean = True
        While 1
            If TotalCellCount / 3 > VisitedList.Count Then
                If ExitCase() Then Return Nothing
                RecentCells.Clear()
                For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                    RecentCells.Add(cell)
                Next
                Dim Index As Integer = R.Next(0, RecentCells.Count)
                Dim TemporaryCell As Cell = RecentCells(Index)
                Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
                If Not VisitedList.Contains(TemporaryCell) Then
                    VisitedList.Add(New Cell(TemporaryCell.X, TemporaryCell.Y))
                    AvailableCells.Remove(TemporaryCell)
                    WallCell = MidPoint(CurrentCell, TemporaryCell)
                    CurrentCell = TemporaryCell
                    AddToPath(ReturnablePath, WallCell, TemporaryCell)
                    If ShowMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        PrevCell.Print("██")
                        WallCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        TemporaryCell.Print("██")
                        PrevCell = CurrentCell
                    End If
                Else
                    CurrentCell = TemporaryCell
                    If ShowMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        PrevCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        TemporaryCell.Print("██")
                        PrevCell = CurrentCell
                    End If
                End If
                Threading.Thread.Sleep(Delay)
            Else
                If InitialiseWilsonCell Then
                    CurrentCell = PickRandomCell(AvailableCells, VisitedList, Limits)
                    If ShowMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        PrevCell.Print("██")
                    End If
                    InitialiseWilsonCell = False
                End If
                'wilsons
                If ExitCase() Then Return Nothing
                RecentCells.Clear()
                Dim TemporaryCell As Cell
                For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                    RecentCells.Add(cell)
                Next
                TemporaryCell = RecentCells(R.Next(0, RecentCells.Count))
                Dim dir As String = GetDirection(CurrentCell, TemporaryCell, directions, ShowMazeGeneration)
                If VisitedList.Contains(TemporaryCell) Then 'Unvisited cell?
                    SetBoth(ConsoleColor.White)
                    Dim NewList As New List(Of Cell)
                    Dim current As Cell = directions.Keys(0)
                    Dim cur As Cell = current
                    AvailableCells.Remove(current)
                    While 1
                        Dim prev111 As Cell = cur
                        cur = PickNextDir(prev111, directions, ShowMazeGeneration, Delay, ReturnablePath)
                        NewList.Add(cur)
                        AvailableCells.Remove(cur)
                        If VisitedList.Contains(cur) Then Exit While
                    End While
                    Dim newcell As Cell = MidPoint(NewList(0), directions.Keys(0))
                    If Not ReturnablePath.Contains(New Node(newcell.X, newcell.Y)) Then ReturnablePath.Add(New Node(newcell.X, newcell.Y))
                    If ShowMazeGeneration Then newcell.Print("██")
                    For i = 1 To NewList.Count - 1
                        Dim wall As Cell = MidPoint(NewList(i), NewList(i - 1))
                        If ShowMazeGeneration Then
                            NewList(i).Print("██")
                            wall.Print("██")
                            Threading.Thread.Sleep(Delay)
                        End If
                        AddToPath(ReturnablePath, NewList(i), wall)
                    Next
                    For Each value In NewList
                        If Not VisitedList.Contains(value) Then VisitedList.Add(value)
                    Next
                    If Not VisitedList.Contains(directions.Keys(0)) Then VisitedList.Add(directions.Keys(0))
                    NewList.Clear()
                    directions.Clear()
                    Threading.Thread.Sleep(Delay)
                    If ShowMazeGeneration Then
                        For Each thing In Direction
                            If VisitedList.Contains(thing.Key) Then
                                SetBoth(ConsoleColor.White)
                                thing.Key.Print("  ")
                            Else
                                SetBoth(ConsoleColor.Black)
                                thing.Key.Print("  ")
                            End If
                        Next
                    End If
                    Direction.Clear()
                    If TotalCellCount = VisitedList.Count Then Exit While
                    CurrentCell = PickRandomCell(AvailableCells, VisitedList, Limits)
                Else
                    CurrentCell = TemporaryCell
                    If Direction.ContainsKey(CurrentCell) Then
                        Direction(CurrentCell) = GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration)
                    Else
                        Direction.Add(CurrentCell, GetDirection(CurrentCell, PrevCell, directions, ShowMazeGeneration))
                    End If
                    SetBoth(ConsoleColor.White)
                    PrevCell = CurrentCell
                End If
            End If
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

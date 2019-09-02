Module Wilsons_Algorithm
    Function Wilsons(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim UST, RecentCells, availablepositions As New List(Of Cell)
        Dim CurrentCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As New Cell(Limits(0) + 3, Limits(1) + 2)
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                availablepositions.Add(New Cell(x, y))
            Next
        Next
        Dim CellCount As Integer = availablepositions.Count
        Dim StartingCell As Cell = PickRandomCell(availablepositions, UST, Limits)
        If ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            StartingCell.Print("██")
        End If
        UST.Add(StartingCell)
        ReturnablePath.Add(New Node(StartingCell.X, StartingCell.Y))
        Dim Direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While 1
            If ExitCase() Then Return Nothing
            RecentCells.Clear()
            Dim TemporaryCell As Cell
            For Each cell As Cell In RanNeighbour(CurrentCell, Limits)
                RecentCells.Add(cell)
            Next
            TemporaryCell = RecentCells(R.Next(0, RecentCells.Count))
            Dim dir As String = GetDirection(CurrentCell, TemporaryCell, directions, ShowMazeGeneration)
            If UST.Contains(TemporaryCell) Then 'Unvisited cell?
                Direction.Add(TemporaryCell, GetDirection(TemporaryCell, CurrentCell, directions, ShowMazeGeneration))
                SetBoth(ConsoleColor.White)
                Dim NewList As New List(Of Cell)
                Dim current As Cell = directions.Keys(0)
                Dim cur As Cell = current
                While 1
                    Dim prev111 As Cell = cur
                    cur = PickNextDir(prev111, directions, ShowMazeGeneration, Delay, ReturnablePath)
                    NewList.Add(cur)
                    availablepositions.Remove(cur)
                    If UST.Contains(cur) Then Exit While
                End While
                Dim newcell As Cell = MidPoint(NewList(0), directions.Keys(0))
                ReturnablePath.Add(New Node(newcell.X, newcell.Y))
                If ShowMazeGeneration Then newcell.Print("██")
                For i = 1 To NewList.Count - 1
                    If ShowMazeGeneration Then NewList(i).Print("██")
                    Dim wall As Cell = MidPoint(NewList(i), NewList(i - 1))
                    If ShowMazeGeneration Then wall.Print("██")
                    Dim tempNode As New Node(NewList(i).X, NewList(i).Y)
                    If Not ReturnablePath.Contains(tempNode) Then ReturnablePath.Add(New Node(NewList(i).X, NewList(i).Y))
                    tempNode.update(wall.X, wall.Y)
                    If Not ReturnablePath.Contains(tempNode) Then ReturnablePath.Add(New Node(wall.X, wall.Y))
                    Threading.Thread.Sleep(Delay)
                Next
                For Each value In NewList
                    If Not UST.Contains(value) Then UST.Add(value)
                Next
                If Not UST.Contains(directions.Keys(0)) Then UST.Add(directions.Keys(0))
                NewList.Clear()
                directions.Clear()
                Threading.Thread.Sleep(Delay)
                If ShowMazeGeneration Then
                    For Each thing In Direction
                        If UST.Contains(thing.Key) Then
                            SetBoth(ConsoleColor.White)
                            thing.Key.Print("  ")
                        Else
                            SetBoth(ConsoleColor.Black)
                            thing.Key.Print("  ")
                        End If
                    Next
                End If
                Direction.Clear()
                If CellCount = UST.Count Then Exit While
                CurrentCell = PickRandomCell(availablepositions, UST, Limits)
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

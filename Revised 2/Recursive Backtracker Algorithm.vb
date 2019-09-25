Module Recursive_Backtracker
    Function RecursiveBacktracker(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim r As New Random
        Dim back As ConsoleColor = ConsoleColor.White
        If back <> ConsoleColor.White Then
            SetBoth(ConsoleColor.White)
            For y = Limits(1) - 1 To Limits(3) + 1
                For x = Limits(0) + 1 To Limits(2) + 1
                    Console.SetCursorPosition(x, y)
                    Console.Write("XX")
                Next
            Next
        End If
        SetBoth(back)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As Cell = CurrentCell '(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim Stack As New Stack(Of Cell)
        Dim ReturnablePath As New List(Of Node)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim Bias() As Integer = {0, 1}
        VisitedCells(CurrentCell) = True
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If ShowMazeGeneration Then
                PrevCell.Print("██")
                SetBoth(back)
            End If
            Dim RecentCells As List(Of Cell) = Neighbour(CurrentCell, VisitedCells, Limits, True)
            'If RecentCells.Count = 0 Then RecentCells = RanNeighbour(CurrentCell, Limits)
            If RecentCells.Count > 0 Then 'done
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                VisitedCells(TemporaryCell) = True
                Stack.Push(New Cell(TemporaryCell.X, TemporaryCell.Y))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                AddToPath(ReturnablePath, TemporaryCell, WallCell)
                If ShowMazeGeneration Then
                    SetBoth(back)
                    PrevCell.Print("██")
                    WallCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    TemporaryCell.Print("██")
                    PrevCell = CurrentCell
                End If
            ElseIf Stack.Count > 1 Then
                CurrentCell = Stack.Pop
                If ShowMazeGeneration Then
                    SetBoth(back)
                    PrevCell.Print("██")
                    SetBoth(ConsoleColor.Blue)
                    CurrentCell.Print("██")
                    SetBoth(back)
                    PrevCell = CurrentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(Delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(back)
            PrintMazeHorizontally(ReturnablePath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Unicursal(ReturnablePath)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
    Function RecursiveBacktrackerRecursively(ByVal cell As Cell, ByVal limits() As Integer, ByVal path As List(Of Node), ByRef visited As Dictionary(Of Cell, Boolean), ByRef cameFrom As Cell, ByVal r As Random, ByVal ShowMazeGeneration As Boolean, ByVal Delay As Integer)
        Dim RecentCells As List(Of Cell) = Neighbour(cell, visited, limits, True)
        If RecentCells.Count > 0 Then
            Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
            Dim wall As Cell = MidPoint(cameFrom, TemporaryCell)
            If ShowMazeGeneration Then
                wall.Print("██")
                TemporaryCell.Print("██")
                Threading.Thread.Sleep(Delay) : End If
            AddToPath(path, TemporaryCell, wall)
            cameFrom = TemporaryCell
            visited(TemporaryCell) = True
            RecursiveBacktrackerRecursively(TemporaryCell, limits, path, visited, cameFrom, r, ShowMazeGeneration, Delay)
        Else
            Return Nothing
        End If
        cameFrom = cell
        RecursiveBacktrackerRecursively(cell, limits, path, visited, cameFrom, r, ShowMazeGeneration, Delay)
        Return path
    End Function
End Module
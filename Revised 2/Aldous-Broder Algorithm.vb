Module Aldous_Broder
    Function AldousBroder(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim TotalCellCount As Integer
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim RecentCells As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PrevCell As Cell = PickRandomStartingCell(Limits)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                TotalCellCount += 1
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        VisitedCells(CurrentCell) = True
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        Dim UsedCellCount As Integer = 1
        While UsedCellCount <> TotalCellCount
            If ExitCase() Then Return Nothing
            RecentCells.Clear()
            RecentCells = RanNeighbour(CurrentCell, Limits)
            Dim Index As Integer = R.Next(0, RecentCells.Count)
            Dim TemporaryCell As Cell = RecentCells(Index)
            Dim TempNodeCell As New Node(TemporaryCell.X, TemporaryCell.Y)
            If Not VisitedCells(TemporaryCell) Then
                VisitedCells(New Cell(TemporaryCell.X, TemporaryCell.Y)) = True
                UsedCellCount += 1
                WallCell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                ReturnablePath.Add(New Node(TemporaryCell.X, TemporaryCell.Y))
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
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(ReturnablePath, Limits(2), Limits(3))
        Else
            SetBoth(ConsoleColor.White)
            PrevCell.Print("██")
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(ReturnablePath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnablePath
    End Function
End Module

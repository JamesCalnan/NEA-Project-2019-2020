Module Growing_Tree_Algorithm
    Function GrowingTree(ByVal Limits() As Integer, ByVal delay As Integer, ByVal CellSelectionMethod() As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim PreviousCell As Cell = CurrentCell
        Dim WallCell As Cell
        Dim Index As Integer
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim ReturnablePath As New List(Of Node)
        VisitedCells(CurrentCell) = True
        If ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            CurrentCell.Print("██")
        End If
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
            Next
            Dim RandomNumber As Integer
            If CellSelectionMethod(0) = 1 Then
                'Recursive Backtracker
                If RecentFrontierSet.Count > 0 Then
                    Index = R.Next(0, RecentFrontierSet.Count)
                    CurrentCell = RecentFrontierSet(Index)
                Else
                    If FrontierSet.Count = 0 Then Exit While
                    Index = FrontierSet.Count - 1
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(1) = 1 Then
                'Prim's
                If FrontierSet.Count = 0 Then Exit While
                Index = R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(2) = 1 Or CellSelectionMethod(3) = 1 Or CellSelectionMethod(4) = 1 Then
                Dim Chance As Integer
                If CellSelectionMethod(2) = 1 Then
                    '75/25 split
                    Chance = 75
                ElseIf CellSelectionMethod(3) = 1 Then
                    '50/50 split
                    Chance = 50
                ElseIf CellSelectionMethod(4) = 1 Then
                    '25/75 split
                    Chance = 25
                End If
                RandomNumber = R.Next(1, 101)
                If RandomNumber < Chance Then
                    'Newest
                    If RecentFrontierSet.Count > 0 Then
                        Index = R.Next(0, RecentFrontierSet.Count)
                        CurrentCell = RecentFrontierSet(Index)
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(5) = 1 Then
                'Oldest
                If FrontierSet.Count = 0 Then Exit While
                Index = 0
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(6) = 1 Then
                'Middle
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count / 2
                CurrentCell = FrontierSet(Index)
                Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
            ElseIf CellSelectionMethod(7) = 1 Then
                'Newest/Oldest, 50/50 split
                RandomNumber = R.Next(1, 101)
                If RandomNumber > 50 Then
                    'Newest
                    If RecentFrontierSet.Count > 0 Then
                        Index = R.Next(0, RecentFrontierSet.Count)
                        CurrentCell = RecentFrontierSet(Index)
                    Else
                        If FrontierSet.Count = 0 Then Exit While
                        Index = R.Next(0, FrontierSet.Count)
                        CurrentCell = FrontierSet(Index)
                        Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                        PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                    End If
                Else
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0 'R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            ElseIf CellSelectionMethod(8) = 1 Then
                'Oldest/Random, 50/50 split
                RandomNumber = R.Next(1, 101)
                If RandomNumber > 50 Then
                    'Oldest
                    If FrontierSet.Count = 0 Then Exit While
                    Index = 0
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                Else
                    'Random
                    If FrontierSet.Count = 0 Then Exit While
                    Index = R.Next(0, FrontierSet.Count)
                    CurrentCell = FrontierSet(Index)
                    Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
                    PreviousCell = PickAdjancentCell(CurrentCell, AdjancencyList)
                End If
            End If
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                SetBoth(ConsoleColor.White)
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
            PreviousCell = CurrentCell
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

Module Custom_Algorithm
    Function Custom(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet, RecentFrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim WallCell As Cell
        Dim Index As Integer
        Dim ReturnablePath As New List(Of Node)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        VisitedCells(CurrentCell) = True
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
                RecentFrontierSet.Add(cell)
            Next
            If RecentFrontierSet.Count > 0 Then
                Index = R.Next(0, RecentFrontierSet.Count)
                CurrentCell = RecentFrontierSet(Index)
            Else
                If FrontierSet.Count = 0 Then Exit While
                Index = FrontierSet.Count - 1 'R.Next(0, FrontierSet.Count)
                CurrentCell = FrontierSet(Index)
            End If
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                SetBoth(ConsoleColor.White)
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
            Threading.Thread.Sleep(delay)
            RecentFrontierSet.Clear()
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

Module Prims
    Function Prims_Simplified(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        'Assumes the weights of each cell in the grid is the same and therefore chooses a random cell from the frontier set
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet As New List(Of Cell)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        VisitedCells(CurrentCell) = True
        SetBoth(ConsoleColor.White)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
            Next
            If FrontierSet.Count = 0 Then Exit While
            CurrentCell = FrontierSet(R.Next(0, FrontierSet.Count))
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                WallCell.Print("██")
                CurrentCell.Print("██")
                Threading.Thread.Sleep(delay)
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
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
    Function Prims_True(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        'Assigns a random weight between 0, 99 to each available in the grid, it then chooses the cell with the lowest weight out of the frontier set
        Dim R As New Random
        Dim availablepath As New List(Of Cell)
        Dim FrontierSet As New List(Of Cell)
        Dim WallCell As Cell
        Dim ReturnablePath As New List(Of Node)
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim Weights As New Dictionary(Of Cell, Integer)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                Dim tempNode As New Cell(x, y)
                Weights(tempNode) = R.Next(0, 99) 'Assigning random weights to each cell in the grid
            Next
        Next
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(FrontierSet(idx).X, FrontierSet(idx).Y) '(Limits(0) + 3, Limits(1) + 2)
        VisitedCells(CurrentCell) = True
        SetBoth(ConsoleColor.White)
        If ShowMazeGeneration Then CurrentCell.Print("██")
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(CurrentCell, VisitedCells, Limits, True)
                If Not FrontierSet.Contains(cell) Then FrontierSet.Add(cell)
            Next
            If FrontierSet.Count = 0 Then Exit While
            Dim HighestWeightCell As Cell = FrontierSet(0)
            For Each cell In FrontierSet
                If Weights(HighestWeightCell) < Weights(cell) Then HighestWeightCell = cell
            Next
            CurrentCell = HighestWeightCell
            Dim AdjancencyList() As Integer = AdjacentCheck(CurrentCell, VisitedCells)
            Dim PreviousCell As Cell = PickAdjancentCell(CurrentCell, AdjancencyList)
            WallCell = MidPoint(CurrentCell, PreviousCell)
            If ShowMazeGeneration Then
                WallCell.Print("██")
                CurrentCell.Print("██")
            End If
            VisitedCells(CurrentCell) = True
            FrontierSet.Remove(CurrentCell)
            AddToPath(ReturnablePath, CurrentCell, WallCell)
            Threading.Thread.Sleep(delay)
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

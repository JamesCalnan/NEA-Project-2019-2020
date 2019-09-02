Module Sidewinder_Algorithm
    Function Sidewinder(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        While Limits(2) Mod 4 <> 2
            Limits(2) -= 1
        End While
        Dim WallCell As Cell
        Dim RunSet As New List(Of Cell)
        Dim Availablepath As New List(Of Node)
        Dim R As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim CurrentCell As New Cell(x, y)
                Availablepath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                If ShowMazeGeneration Then CurrentCell.Print("██")
                If y <> Limits(1) Then RunSet.Add(New Cell(CurrentCell.X, CurrentCell.Y))
                Dim EastCell As New Cell(x + 4, y)
                Dim RanNum As Integer = R.Next(1, 101)
                If x + 2 = Limits(2) And y <> Limits(1) Then
                    RanNum = 1
                End If
                If RanNum > 50 Or y = Limits(1) Then
                    If EastCell.WithinLimits(Limits) Then
                        WallCell = MidPoint(CurrentCell, EastCell)
                        If ShowMazeGeneration Then WallCell.Print("██")
                        Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    End If
                Else
                    Dim RandomRunSet As Integer = R.Next(0, RunSet.Count)
                    Dim RandomRunSetCell As Cell = RunSet(RandomRunSet)
                    Dim NorthCell As New Cell(RandomRunSetCell.X, y - 2)
                    WallCell = MidPoint(RandomRunSetCell, NorthCell)
                    Availablepath.Add(New Node(WallCell.X, WallCell.Y))
                    If ShowMazeGeneration Then WallCell.Print("██")
                    RunSet.Clear()
                End If
                Threading.Thread.Sleep(Delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(Availablepath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(Availablepath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return Availablepath
    End Function
End Module

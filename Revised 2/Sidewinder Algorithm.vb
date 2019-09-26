Module SidewinderAlgorithm
    Function Sidewinder(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        While limits(2) Mod 4 <> 2
            limits(2) -= 1
        End While
        Dim wallCell As Cell
        Dim runSet As New List(Of Cell)
        Dim availablepath As New List(Of Node)
        Dim r As New Random
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                If ExitCase() Then Return Nothing
                Dim currentCell As New Cell(x, y)
                availablepath.Add(New Node(currentCell.X, currentCell.Y))
                If showMazeGeneration Then currentCell.Print("██")
                If y <> limits(1) Then runSet.Add(New Cell(currentCell.X, currentCell.Y))
                Dim eastCell As New Cell(x + 4, y)
                Dim ranNum As Integer = r.Next(1, 101)
                If x + 2 = limits(2) And y <> limits(1) Then
                    ranNum = 1
                End If
                If ranNum > 50 Or y = limits(1) Then
                    If eastCell.WithinLimits(limits) Then
                        wallCell = MidPoint(currentCell, eastCell)
                        If showMazeGeneration Then wallCell.Print("██")
                        availablepath.Add(New Node(wallCell.X, wallCell.Y))
                    End If
                Else
                    Dim randomRunSet As Integer = r.Next(0, runSet.Count)
                    Dim randomRunSetCell As Cell = runSet(randomRunSet)
                    Dim northCell As New Cell(randomRunSetCell.X, y - 2)
                    wallCell = MidPoint(randomRunSetCell, northCell)
                    availablepath.Add(New Node(wallCell.X, wallCell.Y))
                    If showMazeGeneration Then wallCell.Print("██")
                    runSet.Clear()
                End If
                Threading.Thread.Sleep(delay)
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(availablepath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(availablepath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return availablepath
    End Function
End Module

Module Ellers_Algorithm
    Function Ellers(ByVal Limits() As Integer, ByVal Delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim LowerBound As Integer = Limits(2)
        Dim UpperBound As Integer = Limits(2)
        Do
            UpperBound += 1
            If UpperBound Mod 4 = 0 Then
                Limits(2) = UpperBound
                Exit Do
            End If
            LowerBound -= 1
            If LowerBound Mod 4 = 0 Then
                Limits(2) = LowerBound
                Exit Do
            End If
        Loop
        Dim Row As New List(Of Cell)
        Dim RowSet As New Dictionary(Of Cell, Integer)
        Dim SetNum As Integer = 0
        Dim R As New Random
        Dim ReturnPath As New List(Of Node)
        Dim availableCellPositions As New List(Of Cell)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim availableCells As New Dictionary(Of Cell, Boolean)
        For y = Limits(1) To Limits(3) Step 2
            For x = Limits(0) + 3 To Limits(2) Step 4
                Dim cur As New Cell(x, y)
                availableCells(cur) = True
            Next
        Next
        SetBoth(ConsoleColor.White)
        For y = Limits(1) To Limits(3) - 2 Step 2
            For i = 0 To 1
                For x = Limits(0) + 3 To Limits(2) Step 4
                    If ExitCase() Then Return Nothing
                    If i = 0 Then
                        'first pass of the row!
                        Dim CurCell As New Cell(x, y)
                        Row.Add(CurCell)
                        If Not RowSet.ContainsKey(CurCell) And availableCells(CurCell) Then
                            SetNum += 1
                            RowSet(CurCell) = SetNum
                            If ShowMazeGeneration Then CurCell.Print($"██")
                            If Not ReturnPath.Contains(New Node(CurCell.X, CurCell.Y)) Then ReturnPath.Add(New Node(CurCell.X, CurCell.Y))
                        End If
                        availableCellPositions.Add(CurCell)
                    Else
                        'second pass of the row, need to join cells together
                        Dim CurCell As New Cell(x, y)
                        Dim NextCell As New Cell(x + 4, y)
                        Dim CurrentCellSet As Integer = RowSet(CurCell)
                        Dim AdjacentCellSet As Integer = -1
                        If Row.Contains(NextCell) Then AdjacentCellSet = RowSet(NextCell)
                        If AdjacentCellSet <> -1 AndAlso CurrentCellSet <> AdjacentCellSet And R.Next(0, 101) > 50 Then
                            'join sets together
                            Dim WallCell As Cell = MidPoint(CurCell, NextCell)
                            If ShowMazeGeneration Then WallCell.Print("██")
                            If Not ReturnPath.Contains(New Node(WallCell.X, WallCell.Y)) Then ReturnPath.Add(New Node(WallCell.X, WallCell.Y))
                            Dim SetNumToBeChanged As Integer = RowSet(NextCell)
                            Dim CellsToBeChanged As New List(Of Cell)
                            For Each thing In RowSet
                                If Row.Contains(thing.Key) Then
                                    If thing.Value = SetNumToBeChanged Then
                                        CellsToBeChanged.Add(thing.Key)
                                    End If
                                End If
                            Next
                            For Each thing In CellsToBeChanged
                                RowSet(thing) = RowSet(CurCell)
                            Next
                        ElseIf CurrentCellSet <> AdjacentCellSet And AdjacentCellSet <> -1 And y >= Limits(3) - 3 Then
                            'final row, need to join sets together
                            Dim WallCell As Cell = MidPoint(CurCell, NextCell)
                            If ShowMazeGeneration Then WallCell.Print("██")
                            If Not ReturnPath.Contains(New Node(WallCell.X, WallCell.Y)) Then ReturnPath.Add(New Node(WallCell.X, WallCell.Y))
                            Dim SetNumToBeChanged As Integer = RowSet(NextCell)
                            Dim CellsToBeChanged As New List(Of Cell)
                            For Each thing In RowSet
                                If Row.Contains(thing.Key) Then
                                    If thing.Value = SetNumToBeChanged Then
                                        CellsToBeChanged.Add(thing.Key)
                                    End If
                                End If
                            Next
                            For Each thing In CellsToBeChanged
                                RowSet(thing) = RowSet(CurCell)
                            Next
                        End If
                        If x = Limits(2) And y <> Limits(3) - 2 And y < Limits(3) - 3 Then
                            'need to carve south
                            Dim CurrentSet As New List(Of Cell)
                            Dim thingy As New List(Of List(Of Cell))
                            Row.Reverse()
                            Dim FinalCell As Cell = Row(Row.Count - 1)

                            For j = 0 To Row.Count - 1
                                If RowSet(Row(j)) = If(Row(j).Equals(FinalCell), True, RowSet(Row(j + 1))) Then
                                    'if the current cell is in the same set as the next cell then they are in the same set
                                    CurrentSet.Add(Row(j))
                                    CurrentSet.Add(Row(j + 1))
                                Else
                                    'the next cell isnt in the same set as the current cell and therefore a path can be carved south from random cells in the set
                                    If CurrentSet.Count = 0 Then
                                        'individual cell
                                        Dim SouthWallCell As New Cell(Row(j).X, Row(j).Y + 1)
                                        Dim southCell As New Cell(Row(j).X, Row(j).Y + 2)
                                        If Not ReturnPath.Contains(New Node(southCell.X, southCell.Y)) Then ReturnPath.Add(New Node(southCell.X, southCell.Y))
                                        If Not ReturnPath.Contains(New Node(SouthWallCell.X, SouthWallCell.Y)) Then ReturnPath.Add(New Node(SouthWallCell.X, SouthWallCell.Y))
                                        RowSet(southCell) = RowSet(Row(j))
                                        availableCells(southCell) = False
                                        If ShowMazeGeneration Then southCell.Print($"██")
                                        If ShowMazeGeneration Then SouthWallCell.Print($"██")
                                    Else
                                        'multiple cells
                                        Dim Indexes As New List(Of Integer)
                                        For k = 0 To 1
                                            Dim idx As Integer = R.Next(0, CurrentSet.Count)
                                            If Not Indexes.Contains(idx) Then Indexes.Add(idx)
                                        Next
                                        Dim Positions As New List(Of Cell)
                                        For k = 0 To Indexes.Count - 1
                                            'If Indexes(k) = 1 Then Continue For
                                            Dim SouthWallCell As New Cell(CurrentSet(Indexes(k)).X, CurrentSet(Indexes(k)).Y + 1)
                                            Dim southCell As New Cell(CurrentSet(Indexes(k)).X, CurrentSet(Indexes(k)).Y + 2)
                                            availableCells(southCell) = False
                                            RowSet(southCell) = RowSet(Row(j))
                                            If ShowMazeGeneration Then southCell.Print($"██")
                                            If ShowMazeGeneration Then SouthWallCell.Print($"██")
                                            If Not ReturnPath.Contains(New Node(southCell.X, southCell.Y)) Then ReturnPath.Add(New Node(southCell.X, southCell.Y))
                                            If Not ReturnPath.Contains(New Node(SouthWallCell.X, SouthWallCell.Y)) Then ReturnPath.Add(New Node(SouthWallCell.X, SouthWallCell.Y))
                                        Next
                                        For Each position In Positions
                                            RowSet(position) = RowSet(Row(j))
                                            availableCells(position) = False
                                        Next
                                        CurrentSet.Clear()
                                    End If
                                End If
                                Threading.Thread.Sleep(Delay)
                            Next
                            Row.Clear()
                        End If
                    End If
                    Threading.Thread.Sleep(Delay)
                Next
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not ShowMazeGeneration Then
            Console.ForegroundColor = (ConsoleColor.White)
            Console.BackgroundColor = (ConsoleColor.White)
            PrintMazeHorizontally(ReturnPath, Limits(2), Limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        Limits(3) -= 2
        AddStartAndEnd(ReturnPath, Limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return ReturnPath
    End Function
End Module

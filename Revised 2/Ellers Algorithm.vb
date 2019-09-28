Module EllersAlgorithm
    Function Ellers(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour as consolecolor, backGroundColour as consolecolor)
        dim tempLimits() = {limits(0),limits(1),limits(2),limits(3)-2}
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,tempLimits)
        Dim lowerBound As Integer = limits(2)
        Dim upperBound As Integer = limits(2)
        Do
            upperBound += 1
            If upperBound Mod 4 = 0 Then
                limits(2) = upperBound
                Exit Do
            End If
            lowerBound -= 1
            If lowerBound Mod 4 = 0 Then
                limits(2) = lowerBound
                Exit Do
            End If
        Loop
        Dim row As New List(Of Cell)
        Dim rowSet As New Dictionary(Of Cell, Integer)
        Dim setNum = 0
        Dim r As New Random
        Dim returnPath As New List(Of Node)
        Dim availableCellPositions As New List(Of Cell)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim availableCells As New Dictionary(Of Cell, Boolean)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                Dim cur As New Cell(x, y)
                availableCells(cur) = True
            Next
        Next
        SetBoth(pathColour)
        For y = limits(1) To limits(3) - 2 Step 2
            For i = 0 To 1
                For x = limits(0) + 3 To limits(2) Step 4
                    If ExitCase() Then Return Nothing
                    If i = 0 Then
                        'first pass of the row!
                        Dim curCell As New Cell(x, y)
                        row.Add(curCell)
                        If Not rowSet.ContainsKey(curCell) And availableCells(curCell) Then
                            setNum += 1
                            rowSet(curCell) = setNum
                            If showMazeGeneration Then curCell.Print($"██")
                            If Not returnPath.Contains(New Node(curCell.X, curCell.Y)) Then returnPath.Add(New Node(curCell.X, curCell.Y))
                        End If
                        availableCellPositions.Add(curCell)
                    Else
                        'second pass of the row, need to join cells together
                        Dim curCell As New Cell(x, y)
                        Dim nextCell As New Cell(x + 4, y)
                        Dim currentCellSet As Integer = rowSet(curCell)
                        Dim adjacentCellSet As Integer = -1
                        If row.Contains(nextCell) Then adjacentCellSet = rowSet(nextCell)
                        If adjacentCellSet <> -1 AndAlso currentCellSet <> adjacentCellSet And r.Next(0, 101) > 50 Then
                            'join sets together
                            Dim wallCell As Cell = MidPoint(curCell, nextCell)
                            If showMazeGeneration Then wallCell.Print("██")
                            If Not returnPath.Contains(New Node(wallCell.X, wallCell.Y)) Then returnPath.Add(New Node(wallCell.X, wallCell.Y))
                            Dim setNumToBeChanged As Integer = rowSet(nextCell)
                            Dim cellsToBeChanged As List(Of Cell) = (From thing In rowSet Where row.Contains(thing.Key) Where thing.Value = setNumToBeChanged Select thing.Key).ToList()
                            For Each thing In cellsToBeChanged
                                rowSet(thing) = rowSet(curCell)
                            Next
                        ElseIf currentCellSet <> adjacentCellSet And adjacentCellSet <> -1 And y >= limits(3) - 3 Then
                            'final row, need to join sets together
                            Dim wallCell As Cell = MidPoint(curCell, nextCell)
                            If showMazeGeneration Then wallCell.Print("██")
                            If Not returnPath.Contains(New Node(wallCell.X, wallCell.Y)) Then returnPath.Add(New Node(wallCell.X, wallCell.Y))
                            Dim setNumToBeChanged As Integer = rowSet(nextCell)
                            Dim cellsToBeChanged As List(Of Cell) = (From thing In rowSet Where row.Contains(thing.Key) Where thing.Value = setNumToBeChanged Select thing.Key).ToList()
                            For Each thing In cellsToBeChanged
                                rowSet(thing) = rowSet(curCell)
                            Next
                        End If
                        If x = limits(2) And y <> limits(3) - 2 And y < limits(3) - 3 Then
                            'need to carve south
                            Dim currentSet As New List(Of Cell)
                            row.Reverse()
                            Dim finalCell As Cell = row(row.Count - 1)
                            For j = 0 To row.Count - 1
                                If rowSet(row(j)) = If(row(j).Equals(finalCell), True, rowSet(row(j + 1))) Then
                                    'if the current cell is in the same set as the next cell then they are in the same set
                                    currentSet.Add(row(j))
                                    currentSet.Add(row(j + 1))
                                Else
                                    'the next cell isnt in the same set as the current cell and therefore a path can be carved south from random cells in the set
                                    If currentSet.Count = 0 Then
                                        'individual cell
                                        Dim southWallCell As New Cell(row(j).X, row(j).Y + 1)
                                        Dim southCell As New Cell(row(j).X, row(j).Y + 2)
                                        If Not returnPath.Contains(New Node(southCell.X, southCell.Y)) Then returnPath.Add(New Node(southCell.X, southCell.Y))
                                        If Not returnPath.Contains(New Node(southWallCell.X, southWallCell.Y)) Then returnPath.Add(New Node(southWallCell.X, southWallCell.Y))
                                        rowSet(southCell) = rowSet(row(j))
                                        availableCells(southCell) = False
                                        If showMazeGeneration Then southCell.Print($"██")
                                        If showMazeGeneration Then southWallCell.Print($"██")
                                    Else
                                        'multiple cells
                                        Dim indexes As New List(Of Integer)
                                        For k = 0 To 1
                                            Dim idx As Integer = r.Next(0, currentSet.Count)
                                            If Not indexes.Contains(idx) Then indexes.Add(idx)
                                        Next
                                        Dim positions As New List(Of Cell)
                                        For k = 0 To indexes.Count - 1
                                            'If Indexes(k) = 1 Then Continue For
                                            Dim southWallCell As New Cell(currentSet(indexes(k)).X, currentSet(indexes(k)).Y + 1)
                                            Dim southCell As New Cell(currentSet(indexes(k)).X, currentSet(indexes(k)).Y + 2)
                                            availableCells(southCell) = False
                                            rowSet(southCell) = rowSet(row(j))
                                            If showMazeGeneration Then southCell.Print($"██")
                                            If showMazeGeneration Then southWallCell.Print($"██")
                                            If Not returnPath.Contains(New Node(southCell.X, southCell.Y)) Then returnPath.Add(New Node(southCell.X, southCell.Y))
                                            If Not returnPath.Contains(New Node(southWallCell.X, southWallCell.Y)) Then returnPath.Add(New Node(southWallCell.X, southWallCell.Y))
                                        Next
                                        For Each position In positions
                                            rowSet(position) = rowSet(row(j))
                                            availableCells(position) = False
                                        Next
                                        currentSet.Clear()
                                    End If
                                End If
                                Threading.Thread.Sleep(delay)
                            Next
                            row.Clear()
                        End If
                    End If
                    Threading.Thread.Sleep(delay)
                Next
            Next
        Next
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnPath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        limits(3) -= 2
        AddStartAndEnd(returnPath, limits, pathcolour)
        Console.SetCursorPosition(0, ypos)
        Return returnPath
    End Function
End Module

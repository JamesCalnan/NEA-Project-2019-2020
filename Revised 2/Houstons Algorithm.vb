Module HoustonsAlgorithm
    Function Houstons(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim totalCellCount As Integer
        Dim r As New Random
        Dim availablepath As New List(Of Cell)
        Dim visitedList, recentCells As New List(Of Cell)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        visitedList.Add(currentCell)
        Dim prevCell As Cell = PickRandomStartingCell(limits)
        Dim wallCell As Cell
        Dim returnablePath As New List(Of Node)
        Dim availableCells As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                totalCellCount += 1
                availableCells.Add(New Cell(x, y))
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(ConsoleColor.White)
        If showMazeGeneration Then currentCell.Print("██")
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        Dim initialiseWilsonCell = True
        While 1
            If totalCellCount / 3 > visitedList.Count Then
                If ExitCase() Then Return Nothing
                recentCells.Clear()
                For Each cell As Cell In RanNeighbour(currentCell, limits)
                    recentCells.Add(cell)
                Next
                Dim index As Integer = r.Next(0, recentCells.Count)
                Dim temporaryCell As Cell = recentCells(index)
                Dim tempNodeCell As New Node(temporaryCell.X, temporaryCell.Y)
                If Not visitedList.Contains(temporaryCell) Then
                    visitedList.Add(New Cell(temporaryCell.X, temporaryCell.Y))
                    availableCells.Remove(temporaryCell)
                    wallCell = MidPoint(currentCell, temporaryCell)
                    currentCell = temporaryCell
                    AddToPath(returnablePath, wallCell, temporaryCell)
                    If showMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        prevCell.Print("██")
                        wallCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        temporaryCell.Print("██")
                        prevCell = currentCell
                    End If
                Else
                    currentCell = temporaryCell
                    If showMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        prevCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        temporaryCell.Print("██")
                        prevCell = currentCell
                    End If
                End If
                Threading.Thread.Sleep(delay)
            Else
                If initialiseWilsonCell Then
                    currentCell = PickRandomCell(availableCells, visitedList, limits)
                    If showMazeGeneration Then
                        SetBoth(ConsoleColor.White)
                        prevCell.Print("██")
                    End If
                    initialiseWilsonCell = False
                End If
                'wilsons
                If ExitCase() Then Return Nothing
                recentCells.Clear()
                Dim temporaryCell As Cell
                For Each cell As Cell In RanNeighbour(currentCell, limits)
                    recentCells.Add(cell)
                Next
                temporaryCell = recentCells(r.Next(0, recentCells.Count))
                Dim dir As String = GetDirection(currentCell, temporaryCell, directions, showMazeGeneration)
                If visitedList.Contains(temporaryCell) Then 'Unvisited cell?
                    SetBoth(ConsoleColor.White)
                    Dim newList As New List(Of Cell)
                    Dim current As Cell = directions.Keys(0)
                    Dim cur As Cell = current
                    availableCells.Remove(current)
                    While 1
                        Dim prev111 As Cell = cur
                        cur = PickNextDir(prev111, directions, showMazeGeneration, delay, returnablePath)
                        newList.Add(cur)
                        availableCells.Remove(cur)
                        If visitedList.Contains(cur) Then Exit While
                    End While
                    Dim newcell As Cell = MidPoint(newList(0), directions.Keys(0))
                    If Not returnablePath.Contains(New Node(newcell.X, newcell.Y)) Then returnablePath.Add(New Node(newcell.X, newcell.Y))
                    If showMazeGeneration Then newcell.Print("██")
                    For i = 1 To newList.Count - 1
                        Dim wall As Cell = MidPoint(newList(i), newList(i - 1))
                        If showMazeGeneration Then
                            newList(i).Print("██")
                            wall.Print("██")
                            Threading.Thread.Sleep(delay)
                        End If
                        AddToPath(returnablePath, newList(i), wall)
                    Next
                    For Each value In newList
                        If Not visitedList.Contains(value) Then visitedList.Add(value)
                    Next
                    If Not visitedList.Contains(directions.Keys(0)) Then visitedList.Add(directions.Keys(0))
                    newList.Clear()
                    directions.Clear()
                    Threading.Thread.Sleep(delay)
                    If showMazeGeneration Then
                        For Each thing In direction
                            If visitedList.Contains(thing.Key) Then
                                SetBoth(ConsoleColor.White)
                                thing.Key.Print("  ")
                            Else
                                SetBoth(ConsoleColor.Black)
                                thing.Key.Print("  ")
                            End If
                        Next
                    End If
                    direction.Clear()
                    If totalCellCount = visitedList.Count Then Exit While
                    currentCell = PickRandomCell(availableCells, visitedList, limits)
                Else
                    currentCell = temporaryCell
                    If direction.ContainsKey(currentCell) Then
                        direction(currentCell) = GetDirection(currentCell, prevCell, directions, showMazeGeneration)
                    Else
                        direction.Add(currentCell, GetDirection(currentCell, prevCell, directions, showMazeGeneration))
                    End If
                    SetBoth(ConsoleColor.White)
                    prevCell = currentCell
                End If
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, 0)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module

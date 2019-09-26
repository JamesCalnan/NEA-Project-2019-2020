Module WilsonsAlgorithm
    Function Wilsons(limits() As Integer, delay As Integer, showMazeGeneration As Boolean)
        Dim r As New Random
        Dim availablepath As New List(Of Cell)
        Dim ust, recentCells, availablepositions As New List(Of Cell)
        Dim currentCell As New Cell(limits(0) + 3, limits(1) + 2)
        Dim prevCell As New Cell(limits(0) + 3, limits(1) + 2)
        Dim returnablePath As New List(Of Node)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                availablepositions.Add(New Cell(x, y))
            Next
        Next
        Dim cellCount As Integer = availablepositions.Count
        Dim startingCell As Cell = PickRandomCell(availablepositions, ust, limits)
        If showMazeGeneration Then
            SetBoth(ConsoleColor.White)
            startingCell.Print("██")
        End If
        ust.Add(startingCell)
        returnablePath.Add(New Node(startingCell.X, startingCell.Y))
        Dim direction, newdir As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While 1
            If ExitCase() Then Return Nothing
            recentCells.Clear()
            Dim temporaryCell As Cell
            For Each cell As Cell In RanNeighbour(currentCell, limits)
                recentCells.Add(cell)
            Next
            temporaryCell = recentCells(r.Next(0, recentCells.Count))
            Dim dir As String = GetDirection(currentCell, temporaryCell, directions, showMazeGeneration)
            If ust.Contains(temporaryCell) Then 'Unvisited cell?
                direction.Add(temporaryCell, GetDirection(temporaryCell, currentCell, directions, showMazeGeneration))
                SetBoth(ConsoleColor.White)
                Dim newList As New List(Of Cell)
                Dim current As Cell = directions.Keys(0)
                Dim cur As Cell = current
                While 1
                    Dim prev111 As Cell = cur
                    cur = PickNextDir(prev111, directions, showMazeGeneration, delay, returnablePath)
                    newList.Add(cur)
                    availablepositions.Remove(cur)
                    If ust.Contains(cur) Then Exit While
                End While
                Dim newcell As Cell = MidPoint(newList(0), directions.Keys(0))
                returnablePath.Add(New Node(newcell.X, newcell.Y))
                If showMazeGeneration Then newcell.Print("██")
                For i = 1 To newList.Count - 1
                    If showMazeGeneration Then newList(i).Print("██")
                    Dim wall As Cell = MidPoint(newList(i), newList(i - 1))
                    If showMazeGeneration Then wall.Print("██")
                    Dim tempNode As New Node(newList(i).X, newList(i).Y)
                    If Not returnablePath.Contains(tempNode) Then returnablePath.Add(New Node(newList(i).X, newList(i).Y))
                    tempNode.Update(wall.X, wall.Y)
                    If Not returnablePath.Contains(tempNode) Then returnablePath.Add(New Node(wall.X, wall.Y))
                    Threading.Thread.Sleep(delay)
                Next
                For Each value In newList
                    If Not ust.Contains(value) Then ust.Add(value)
                Next
                If Not ust.Contains(directions.Keys(0)) Then ust.Add(directions.Keys(0))
                newList.Clear()
                directions.Clear()
                Threading.Thread.Sleep(delay)
                If showMazeGeneration Then
                    For Each thing In direction
                        If ust.Contains(thing.Key) Then
                            SetBoth(ConsoleColor.White)
                            thing.Key.Print("  ")
                        Else
                            SetBoth(ConsoleColor.Black)
                            thing.Key.Print("  ")
                        End If
                    Next
                End If
                direction.Clear()
                If cellCount = ust.Count Then Exit While
                currentCell = PickRandomCell(availablepositions, ust, limits)
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

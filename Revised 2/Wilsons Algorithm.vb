Imports Enumerable = System.Linq.Enumerable

Module WilsonsAlgorithm
    Function WilsonsRefectored(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim r As New Random
        Dim fullMaze As New List(Of Node)
        Dim availablePositions, UST, randomWalkCells As New List(Of Cell)
        Dim cameFrom As New Dictionary(Of Cell, Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                availablePositions.Add(New Cell(x, y))
                cameFrom(New Cell(x, y)) = Nothing
            Next
        Next
        Dim startCell = PickRandomStartingCell(limits)
        UST.Add(startCell)
        SetBoth(pathColour)
        UST(0).Print("XX")
        availablePositions.Remove(startCell)
        Dim currentCell As Cell = PickRandomCell(availablePositions, UST, limits)
        Dim previousCell = currentCell
        While availablePositions.Count > 0
            Dim immediateNeighbours = RanNeighbour(currentCell, limits)
            currentCell = immediateNeighbours(r.Next(immediateNeighbours.count))
            SetBoth(pathColour)
            currentCell.Print("XX")
            cameFrom(previousCell) = currentCell
            If UST.Contains(currentCell) Then 'if the cell is in the uniform spanning tree
                Dim backtrackingCell = randomWalkCells(0)
                backtrackingCell.Print("XX")
                Dim pathToUst As New List(Of Cell) From {
                    backtrackingCell
                }
                Do
                    backtrackingCell = cameFrom(backtrackingCell)
                    backtrackingCell.Print("XX")
                    pathToUst.Add(backtrackingCell)
                Loop Until UST.Contains(backtrackingCell)
                fullMaze.Add(pathToUst(0).ToNode())
                availablePositions.Remove(pathToUst(0))
                UST.AddRange(pathToUst)
                For i = 0 To pathToUst.Count - 2
                    Dim wall As Cell = MidPoint(pathToUst(i), pathToUst(i + 1))
                    availablePositions.Remove(pathToUst(i + 1))
                    wall.Print("XX")
                    fullMaze.Add(wall.ToNode())
                    fullMaze.Add(pathToUst(i + 1).ToNode())
                Next
                SetBoth(ConsoleColor.Black)
                For Each thing In From thing1 In randomWalkCells Where Not UST.Contains(thing1)
                    thing.Print("XX")
                Next
                For y = limits(1) To limits(3) Step 2
                    For x = limits(0) + 3 To limits(2) - 1 Step 4
                        cameFrom(New Cell(x, y)) = Nothing
                    Next
                Next
                randomWalkCells.Clear()
                SetBoth(pathColour)
                If availablePositions.Count = 0 Then Exit While 'there are no available cells
                currentCell = PickRandomCell(availablePositions, UST, limits)
                previousCell = currentCell
                randomWalkCells.Add(currentCell)
                currentCell.Print("XX")
            Else
                cameFrom(previousCell) = currentCell
                If Not randomWalkCells.Contains(previousCell) Then randomWalkCells.Add(previousCell)
                previousCell = currentCell
            End If
        End While
        AddStartAndEnd(fullMaze, limits, pathColour)
        Return fullMaze
    End Function
    Function Wilsons(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim r As New Random
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
            SetBoth(pathColour)
            startingCell.Print("██")
        End If
        ust.Add(startingCell)
        returnablePath.Add(New Node(startingCell.X, startingCell.Y))
        Dim direction As New Dictionary(Of Cell, String)
        Dim directions As New Dictionary(Of Cell, String)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While 1
            If ExitCase() Then Return Nothing
            recentCells.Clear()
            Dim temporaryCell As Cell
            recentCells.AddRange(Enumerable.Cast(Of Cell)(RanNeighbour(currentCell, limits)))
            temporaryCell = recentCells(r.Next(0, recentCells.Count))
            Dim dir As String = GetDirection(currentCell, temporaryCell, directions, showMazeGeneration, 0)
            If ust.Contains(temporaryCell) Then 'Unvisited cell?
                direction.Add(temporaryCell, GetDirection(temporaryCell, currentCell, directions, showMazeGeneration, 0))
                SetBoth(pathColour)
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
                For Each value In From value1 In newList Where Not ust.Contains(value1)
                    ust.Add(value)
                Next
                If Not ust.Contains(directions.Keys(0)) Then ust.Add(directions.Keys(0))
                newList.Clear()
                directions.Clear()
                Threading.Thread.Sleep(delay)
                If showMazeGeneration Then
                    For Each thing In direction
                        If ust.Contains(thing.Key) Then
                            SetBoth(pathColour)
                            thing.Key.Print("  ")
                        Else
                            SetBoth(backGroundColour)
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
                    direction(currentCell) = GetDirection(currentCell, prevCell, directions, showMazeGeneration, delay)
                Else
                    direction.Add(currentCell, GetDirection(currentCell, prevCell, directions, showMazeGeneration, delay))
                End If
                SetBoth(pathColour)
                prevCell = currentCell
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
    Function PickRandomCell(availablePositions As List(Of Cell), ust As List(Of Cell), limits() As Integer)
        Dim r As New Random
        Dim startingCell As New Cell(r.Next(limits(1), limits(3)), r.Next(limits(0) + 3, limits(2) - 1))
        Do
            Dim idx As Integer = r.Next(0, availablePositions.Count)
            startingCell.Update(availablePositions(idx).X, availablePositions(idx).Y)
            If Not ust.Contains(startingCell) Then
                Exit Do
            End If
        Loop
        Return startingCell
    End Function
    Function GetDirection(cell1 As Cell, cell2 As Cell, ByRef newdir As Dictionary(Of Cell, String), showmazegeneration As Boolean, delay As Integer)
        Dim tempCell As New Cell(cell2.X, cell2.Y - 2)
        Console.BackgroundColor = (ConsoleColor.Black)
        Console.ForegroundColor = (ConsoleColor.Red)
        If cell1.Equals(tempCell) Then
            If showmazegeneration Then tempCell.Print("VV")
            If newdir.ContainsKey(tempCell) Then
                newdir(tempCell) = "VV"
            Else
                newdir.Add(tempCell, "VV")
            End If
            If showmazegeneration Then Threading.Thread.Sleep(delay)
            Return "VV"
        End If
        tempCell.Update(cell2.X + 4, cell2.Y)
        If cell1.Equals(tempCell) Then
            If showmazegeneration Then tempCell.Print("<<")
            If newdir.ContainsKey(tempCell) Then
                newdir(tempCell) = "<<"
            Else
                newdir.Add(tempCell, "<<")
            End If
            If showmazegeneration Then Threading.Thread.Sleep(delay)
            Return "<<"
        End If
        tempCell.Update(cell2.X, cell2.Y + 2)
        If cell1.Equals(tempCell) Then
            If showmazegeneration Then tempCell.Print("^^")
            If newdir.ContainsKey(tempCell) Then
                newdir(tempCell) = "^^"
            Else
                newdir.Add(tempCell, "^^")
            End If
            If showmazegeneration Then Threading.Thread.Sleep(delay)
            Return "^^"
        End If
        tempCell.Update(cell2.X - 4, cell2.Y)
        If cell1.Equals(tempCell) Then
            If showmazegeneration Then tempCell.Print(">>")
            If newdir.ContainsKey(tempCell) Then
                newdir(tempCell) = ">>"
            Else
                newdir.Add(tempCell, ">>")
            End If
            If showmazegeneration Then Threading.Thread.Sleep(delay)
            Return ">>"
        End If
        Return Nothing
    End Function
End Module
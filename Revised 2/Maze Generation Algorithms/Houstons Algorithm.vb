Imports Enumerable = System.Linq.Enumerable

Module HoustonsAlgorithm
    Function Houstons(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim cellList As New List(Of Cell)
        SetBoth(ConsoleColor.Red)
        For x = limits(0) + 3 To limits(2) Step 4
            For y = limits(1) To limits(3) Step 2
                cellList.Add(New Cell(x, y))
                Console.SetCursorPosition(x, y)
                'Console.Write("XX")
            Next
        Next
        'Console.ReadKey()
        ShuffleCells(cellList)
        Dim totalCellCount As Integer
        Dim r As New Random
        Dim visitedList, recentCells, randomWalkCells As New List(Of Cell)
        Dim currentCell As Cell = cellList(0) 'PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        visitedList.Add(currentCell)
        Dim prevCell As Cell = currentCell 'PickRandomStartingCell(limits)
        Dim wallCell As Cell
        Dim fullMaze As New List(Of Node)
        Dim availablePositions As New List(Of Cell)
        Dim cameFrom As New Dictionary(Of Cell, Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                totalCellCount += 1
                availablePositions.Add(New Cell(x, y))
                cameFrom(New Cell(x, y)) = Nothing
            Next
        Next
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        SetBoth(pathColour)
        If showMazeGeneration Then currentCell.Print("██")
        fullMaze.Add(New Node(currentCell.X, currentCell.Y))
        availablePositions.Remove(currentCell)
        cellList.Remove(currentCell)
        Dim initialiseWilsonCell = True
        While 1
            If ExitCase() Then Return Nothing
            If totalCellCount / 3 > visitedList.Count Then
                recentCells.Clear()
                recentCells.AddRange(Enumerable.Cast(Of Cell)(RanNeighbour(currentCell, limits)))
                Dim index As Integer = r.Next(0, recentCells.Count)
                Dim temporaryCell As Cell = recentCells(index)
                Dim tempNodeCell As New Node(temporaryCell.X, temporaryCell.Y)
                If Not visitedList.Contains(temporaryCell) Then
                    visitedList.Add(New Cell(temporaryCell.X, temporaryCell.Y))
                    availablePositions.Remove(temporaryCell)
                    cellList.Remove(temporaryCell)
                    wallCell = MidPoint(currentCell, temporaryCell)
                    currentCell = temporaryCell
                    AddToPath(fullMaze, wallCell, temporaryCell)
                    If showMazeGeneration Then
                        SetBoth(pathColour)
                        prevCell.Print("██")
                        wallCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        temporaryCell.Print("██")
                        prevCell = currentCell
                    End If
                    'Console.ReadKey()
                Else
                    currentCell = temporaryCell
                    If showMazeGeneration Then
                        SetBoth(pathColour)
                        prevCell.Print("██")
                        SetBoth(ConsoleColor.Blue)
                        temporaryCell.Print("██")
                        prevCell = currentCell
                    End If
                End If
                Threading.Thread.Sleep(delay)
            Else
                If initialiseWilsonCell Then
                    currentCell = cellList(0) 'PickRandomCell(availablePositions, visitedList, limits, "random", cellList)
                    If showMazeGeneration Then
                        SetBoth(pathColour)
                        prevCell.Print("██")
                    End If
                    prevCell = currentCell
                    randomWalkCells.Add(currentCell)
                    initialiseWilsonCell = False
                End If
                'wilsons
                Dim immediateNeighbours = RanNeighbour(currentCell, limits)
                currentCell = immediateNeighbours(r.Next(immediateNeighbours.count))
                SetBoth(pathColour)
                If showMazeGeneration Then currentCell.Print("XX")
                cameFrom(prevCell) = currentCell
                If visitedList.Contains(currentCell) Then 'if the cell is in the uniform spanning tree
                    Dim backtrackingCell = randomWalkCells(0)
                    If showMazeGeneration Then backtrackingCell.Print("XX")
                    Dim pathToUst As New List(Of Cell) From {backtrackingCell}
                    Do
                        backtrackingCell = cameFrom(backtrackingCell)
                        pathToUst.Add(backtrackingCell)
                    Loop Until visitedList.Contains(backtrackingCell)
                    fullMaze.Add(pathToUst(0).ToNode())
                    availablePositions.Remove(pathToUst(0))
                    cellList.Remove(pathToUst(0))
                    If showMazeGeneration Then pathToUst(0).Print("XX")
                    visitedList.AddRange(pathToUst)
                    For i = 0 To pathToUst.Count - 2
                        Dim wall As Cell = MidPoint(pathToUst(i), pathToUst(i + 1))
                        availablePositions.Remove(pathToUst(i + 1))
                        cellList.Remove(pathToUst(i + 1))
                        If showMazeGeneration Then
                            wall.Print("XX")
                            pathToUst(i + 1).Print("XX")
                            Threading.Thread.Sleep(delay)
                        End If
                        fullMaze.Add(wall.ToNode())
                        fullMaze.Add(pathToUst(i + 1).ToNode())
                    Next
                    SetBoth(backGroundColour)
                    For Each thing In From thing1 In randomWalkCells Where Not visitedList.Contains(thing1)
                        If showMazeGeneration Then thing.Print("XX")
                    Next
                    For y = limits(1) To limits(3) Step 2
                        For x = limits(0) + 3 To limits(2) - 1 Step 4
                            cameFrom(New Cell(x, y)) = Nothing
                        Next
                    Next
                    randomWalkCells.Clear()
                    SetBoth(pathColour)
                    If availablePositions.Count = 0 Then Exit While 'there are no available cells, therefore the maze is done
                    currentCell = cellList(0) 'PickRandomCell(availablePositions, visitedList, limits, "random", cellList)
                    prevCell = currentCell
                    randomWalkCells.Add(currentCell)
                    If showMazeGeneration Then currentCell.Print("XX")
                Else
                    cameFrom(prevCell) = currentCell
                    If Not randomWalkCells.Contains(prevCell) Then randomWalkCells.Add(prevCell)
                    prevCell = currentCell
                End If
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(fullMaze, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(fullMaze, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return fullMaze
    End Function
End Module

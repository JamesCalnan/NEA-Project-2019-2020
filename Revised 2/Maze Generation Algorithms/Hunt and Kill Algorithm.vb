Module HuntAndKill
    Function HuntAndKillRefactored(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As Consolecolor, backGroundColour As ConsoleColor)
        If backgroundcolour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim currentCell As Cell = PickRandomStartingCell(limits)
        Dim r As New Random
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim totalCellCount As Integer = visitedCells.Count
        visitedCells(currentCell) = True
        Dim returnablePath As New List(Of Node)
        Dim usedCellPositions = 1
        SetBoth(pathColour)
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While usedCellPositions <> totalcellcount
            If ExitCase() Then Return Nothing
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, currentCell, wallCell)
                usedCellPositions += 1
                recentCells.Clear()
                If showMazeGeneration Then
                    SetBoth(pathcolour)
                    wallCell.Print("██")
                    currentCell.Print("██")
                End If
                visitedCells(currentCell) = True
            Else
                Dim cellFound = False
                For y = limits(1) To limits(3) Step 2
                    For x = limits(0) + 3 To limits(2) - 1 Step 4
                        If showMazeGeneration Then
                            SetBoth(ConsoleColor.DarkCyan)
                            Console.SetCursorPosition(x, y)
                            Console.Write("██")
                            If x + 2 < limits(2) - 1 Then
                                Console.SetCursorPosition(x + 2, y)
                                Console.Write("██")
                            End If
                        End If
                        Dim adjacencyList As Integer() = AdjacentCheck(New Cell(x, y), visitedCells)
                        Dim pathCell As Cell = PickAdjancentCell(New Cell(x, y), adjacencyList)
                        If Not IsNothing(pathCell) Then
                            Dim wallCell As Cell = MidPoint(pathCell, New Cell(x, y))
                            currentCell = New Cell(x, y)
                            If showMazeGeneration Then
                                SetBoth(pathColour)
                                wallCell.Print("██")
                                currentCell.Print("██")
                                EraseLineHaK(limits, x + 1, returnablePath, y, pathColour, backGroundColour)
                            End If
                            usedCellPositions += 1
                            AddToPath(returnablePath, currentCell, wallCell)
                            cellFound = True
                            visitedCells(currentCell) = True
                            Exit For
                        End If
                    Next
                    If showMazeGeneration Then
                        Threading.Thread.Sleep(delay)
                        EraseLineHaK(limits, limits(2), returnablePath, y, pathColour, backGroundColour)
                    End If
                    If cellFound Then Exit For
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim yPos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathColour)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function


    Function HuntAndKillRefactoredRandom(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim currentCell As Cell = PickRandomStartingCell(limits)
        Dim r As New Random
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim totalCellCount As Integer = visitedCells.Count
        visitedCells(currentCell) = True
        Dim returnablePath As New List(Of Node)
        Dim usedCellPositions = 1
        SetBoth(pathColour)
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While usedCellPositions <> totalCellCount
            If ExitCase() Then Return Nothing
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(0, recentCells.Count))
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, currentCell, wallCell)
                usedCellPositions += 1
                recentCells.Clear()
                If showMazeGeneration Then
                    wallCell.Print("██")
                    currentCell.Print("██")
                    Threading.Thread.Sleep(delay)
                End If
                visitedCells(currentCell) = True
            Else
                Dim huntedCells As New List(Of Cell)
                For y = limits(1) To limits(3) Step 2
                    For x = limits(0) + 3 To limits(2) - 1 Step 4
                        Dim adjacencyList1 As Integer() = AdjacentCheck(New Cell(x, y), visitedCells)
                        If adjacencyList1.Contains(1) Then
                            huntedCells.Add(New Cell(x, y))
                        End If
                    Next
                Next
                currentCell = huntedCells(r.Next(huntedCells.Count))
                Dim adjacencyList As Integer() = AdjacentCheck(currentCell, visitedCells)
                Dim pathCell As Cell = PickAdjancentCell(currentCell, adjacencyList)
                Dim wallCell As Cell = MidPoint(pathCell, currentCell)
                If showMazeGeneration Then
                    wallCell.Print("██")
                    currentCell.Print("██")
                    Threading.Thread.Sleep(delay)
                End If
                AddToPath(returnablePath, currentCell, wallCell)
                visitedCells(currentCell) = True
                usedCellPositions += 1
                huntedCells.Clear()
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim yPos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathColour)
        Console.SetCursorPosition(0, yPos)
        Return returnablePath
    End Function
End Module
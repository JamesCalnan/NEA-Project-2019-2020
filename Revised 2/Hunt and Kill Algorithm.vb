Module Hunt_and_Kill
    Function HuntAndKillREFACTORED(ByVal Limits() As Integer, ByVal delay As Integer, ByVal ShowMazeGeneration As Boolean)
        Dim CurrentCell As Cell = PickRandomStartingCell(Limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim r As New Random
        Dim VisitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(Limits)
        Dim totalcellcount As Integer = VisitedCells.Count
        VisitedCells(CurrentCell) = True
        Dim ReturnablePath As New List(Of Node)
        Dim UsedCellPositions As Integer = 1
        SetBoth(ConsoleColor.White)
        ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
        If ShowMazeGeneration Then CurrentCell.Print("██")
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While UsedCellPositions <> totalcellcount
            If ExitCase() Then Return Nothing
            If Neighbour(CurrentCell, VisitedCells, Limits, False) Then
                Dim RecentCells As List(Of Cell) = Neighbour(CurrentCell, VisitedCells, Limits, True)
                Dim TemporaryCell As Cell = RecentCells(r.Next(0, RecentCells.Count))
                Dim WallCell As Cell = MidPoint(CurrentCell, TemporaryCell)
                CurrentCell = TemporaryCell
                ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                UsedCellPositions += 1
                RecentCells.Clear()

                If ShowMazeGeneration Then
                    SetBoth(ConsoleColor.White)
                    WallCell.Print("██")
                    CurrentCell.Print("██")
                End If
                VisitedCells(CurrentCell) = True
            Else
                Dim CellFound As Boolean = False
                For y = Limits(1) To Limits(3) Step 2
                    For x = Limits(0) + 3 To Limits(2) - 1 Step 4
                        If ShowMazeGeneration Then
                            SetBoth(ConsoleColor.Blue)
                            Console.SetCursorPosition(x, y)
                            Console.Write("██")
                            If x + 2 < Limits(2) - 1 Then
                                Console.SetCursorPosition(x + 2, y)
                                Console.Write("██")
                            End If
                        End If
                        Dim AdjancencyList As Integer() = AdjacentCheck(New Cell(x, y), VisitedCells)
                        Dim pathCell As Cell = PickAdjancentCell(New Cell(x, y), AdjancencyList)
                        If Not IsNothing(pathCell) Then
                            Dim WallCell As Cell = MidPoint(pathCell, New Cell(x, y))
                            CurrentCell = New Cell(x, y)
                            If ShowMazeGeneration Then
                                SetBoth(ConsoleColor.White)
                                WallCell.Print("██")
                                CurrentCell.Print("██")
                                EraseLineHaK(Limits, x + 1, ReturnablePath, y)
                            End If
                            UsedCellPositions += 1
                            ReturnablePath.Add(New Node(CurrentCell.X, CurrentCell.Y))
                            ReturnablePath.Add(New Node(WallCell.X, WallCell.Y))
                            CellFound = True
                            VisitedCells(CurrentCell) = True
                            Exit For
                        End If
                    Next
                    If ShowMazeGeneration Then
                        Threading.Thread.Sleep(delay)
                        EraseLineHaK(Limits, Limits(2), ReturnablePath, y)
                    End If
                    If CellFound Then Exit For
                Next
            End If
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        'EliminateDeadEnds(ReturnablePath)
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
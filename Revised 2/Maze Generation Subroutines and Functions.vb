Module MazeGenerationSubroutinesAndFunctions
    Function PickNextDir(currentcell As Cell, direction As Dictionary(Of Cell, String), showmazegeneation As Boolean, delay As Integer, ByRef returnablepath As List(Of Node))
        Threading.Thread.Sleep(delay)
        If showmazegeneation Then currentcell.Print("██")
        Dim tempNode As New Node(currentcell.X, currentcell.Y)
        If Not returnablepath.Contains(tempNode) Then returnablepath.Add(New Node(currentcell.X, currentcell.Y))
        Dim go As String = direction(currentcell)
        If go = "VV" Then 'down
            Return New Cell(currentcell.X, currentcell.Y + 2)
        ElseIf go = "<<" Then 'left
            Return New Cell(currentcell.X - 4, currentcell.Y)
        ElseIf go = "^^" Then 'up
            Return New Cell(currentcell.X, currentcell.Y - 2)
        ElseIf go = ">>" Then 'right
            Return New Cell(currentcell.X + 4, currentcell.Y)
        End If
        Return Nothing
    End Function
    Function PickRandomCell(availablepositions As List(Of Cell), ust As List(Of Cell), limits() As Integer)
        Dim r As New Random
        Dim startingcell As New Cell(r.Next(limits(1), limits(3)), r.Next(limits(0) + 3, limits(2) - 1))
        Do
            Dim idx As Integer = r.Next(0, availablepositions.Count)
            startingcell.Update(availablepositions(idx).X, availablepositions(idx).Y)
            If Not ust.Contains(startingcell) Then
                Exit Do
            End If
        Loop
        Return startingcell
    End Function
    Function GetDirection(cell1 As Cell, cell2 As Cell, ByRef newdir As Dictionary(Of Cell, String), showmazegeneration As Boolean)
        Dim tempcell As New Cell(cell2.X, cell2.Y - 2)
        Console.BackgroundColor = (ConsoleColor.Black)
        Console.ForegroundColor = (ConsoleColor.Red)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("VV")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "VV"
            Else
                newdir.Add(tempcell, "VV")
            End If
            Return "VV"
        End If
        tempcell.Update(cell2.X + 4, cell2.Y)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("<<")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "<<"
            Else
                newdir.Add(tempcell, "<<")
            End If
            Return "<<"
        End If
        tempcell.Update(cell2.X, cell2.Y + 2)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print("^^")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = "^^"
            Else
                newdir.Add(tempcell, "^^")
            End If
            Return "^^"
        End If
        tempcell.Update(cell2.X - 4, cell2.Y)
        If cell1.Equals(tempcell) Then
            If showmazegeneration Then tempcell.Print(">>")
            If newdir.ContainsKey(tempcell) Then
                newdir(tempcell) = ">>"
            Else
                newdir.Add(tempcell, ">>")
            End If
            Return ">>"
        End If
        Return Nothing
    End Function
    Sub AddToPath(ByRef list As List(Of Node), cell1 As Cell, cell2 As Cell)
        Dim tempnode As New Node(cell1.X, cell1.Y)
        If Not list.Contains(tempnode) Then list.Add(New Node(cell1.X, cell1.Y))
        tempnode.Update(cell2.X, cell2.Y)
        If Not list.Contains(tempnode) Then list.Add(New Node(cell2.X, cell2.Y))
    End Sub
    Sub EraseLineHaK(limits() As Integer, xCount As Integer, visitedlistAndWall As List(Of Node), y As Integer)
        For i = limits(0) + 3 To xCount + 2 Step 2
            Dim tempcell As New Node(i, y)
            If Not visitedlistAndWall.Contains(tempcell) Then
                SetBoth(ConsoleColor.Black)
                tempcell.Print("  ")
            Else
                SetBoth(ConsoleColor.White)
                tempcell.Print("██")
            End If
        Next
    End Sub
    Sub AddStartAndEnd(ByRef maze As List(Of Node), limits() As Integer, evenWidth As Integer, Optional ByVal chooseFirstAndLast As Boolean = False)
        SetBoth(ConsoleColor.Red)
        Dim availableStartPositions As New List(Of Node)
        For x = limits(0) + 3 To limits(2)
            If maze.Contains(New Node(x, limits(1))) Then availableStartPositions.Add(New Node(x, limits(1)))
        Next
        Dim r As New Random
        Dim index As Integer = If(chooseFirstAndLast, 0, r.Next(0, availableStartPositions.Count))
        maze.Add(New Node(availableStartPositions(index).X, availableStartPositions(index).Y - 1))
        SetBoth(ConsoleColor.Red)
        maze(maze.Count - 1).Print("██")
        availableStartPositions.Clear()
        For x = limits(0) + 3 To limits(2)
            If maze.Contains(New Node(x, limits(3))) Then availableStartPositions.Add(New Node(x, limits(3)))
        Next
        index = If(chooseFirstAndLast, availableStartPositions.Count - 1, r.Next(0, availableStartPositions.Count))
        maze.Add(New Node(availableStartPositions(index).X, availableStartPositions(index).Y + 1))
        SetBoth(ConsoleColor.Green)
        maze(maze.Count - 1).Print("██")
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Function AdjacentCheck(cell As Cell, visitedcells As Dictionary(Of Cell, Boolean))
        Dim adjancent() As Integer = {0, 0, 0, 0}
        Dim neighbours As New List(Of Cell)
        Dim tempcell As New Cell(cell.X, cell.Y - 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then adjancent(0) = 1
        tempcell.Update(cell.X + 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then adjancent(1) = 1
        tempcell.Update(cell.X, cell.Y + 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then adjancent(2) = 1
        tempcell.Update(cell.X - 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then adjancent(3) = 1
        Return adjancent
    End Function
    Function PickAdjancentCell(cell As Cell, adjancencylist() As Integer)
        Dim returnCell As Cell
        Dim neighbours As New List(Of Cell)
        If adjancencylist(0) = 1 Then
            neighbours.Add(New Cell(cell.X, cell.Y - 2))
        End If
        If adjancencylist(1) = 1 Then
            neighbours.Add(New Cell(cell.X + 4, cell.Y))
        End If
        If adjancencylist(2) = 1 Then
            neighbours.Add(New Cell(cell.X, cell.Y + 2))
        End If
        If adjancencylist(3) = 1 Then
            neighbours.Add(New Cell(cell.X - 4, cell.Y))
        End If
        Dim r As New Random
        If neighbours.Count > 0 Then
            returnCell = neighbours(r.Next(0, neighbours.Count))
        End If
        Return returnCell
    End Function
    Function RanNeighbour(current As Cell, limits() As Integer)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        Return neighbours
    End Function
    Function Neighbour(current As Cell, visited As Dictionary(Of Cell, Boolean), limits() As Integer, bool As Boolean)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        If bool Then
            Return neighbours
        Else
            If neighbours.Count > 0 Then Return True
        End If
        Return False
    End Function
    Sub PrintMazeHorizontally(maze As List(Of Node), greatestX As Integer, greatestY As Integer)
        For __x = 4 To greatestX + 1 Step 2
            For __y = 1 To greatestY + 1
                If maze.Contains(New Node(__x, __y)) Then
                    Console.SetCursorPosition(__x, __y)
                    Console.Write("██")
                End If
            Next
        Next
    End Sub
End Module

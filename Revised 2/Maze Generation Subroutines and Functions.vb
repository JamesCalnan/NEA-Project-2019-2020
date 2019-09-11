Module Maze_Generation_Subroutines_and_Functions
    Function PickNextDir(ByVal currentcell As Cell, ByVal direction As Dictionary(Of Cell, String), ByVal showmazegeneation As Boolean, ByVal delay As Integer, ByRef returnablepath As List(Of Node))
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
    Function PickRandomCell(ByVal availablepositions As List(Of Cell), ByVal ust As List(Of Cell), ByVal limits() As Integer)
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
    Function GetDirection(ByVal cell1 As Cell, ByVal cell2 As Cell, ByRef newdir As Dictionary(Of Cell, String), ByVal showmazegeneration As Boolean)
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
    Sub AddToPath(ByRef List As List(Of Node), ByVal cell1 As Cell, ByVal cell2 As Cell)
        Dim tempnode As New Node(cell1.X, cell1.Y)
        If Not List.Contains(tempnode) Then List.Add(New Node(cell1.X, cell1.Y))
        tempnode.update(cell2.X, cell2.Y)
        If Not List.Contains(tempnode) Then List.Add(New Node(cell2.X, cell2.Y))
    End Sub
    Sub EraseLineHaK(ByVal limits() As Integer, ByVal xCount As Integer, ByVal VisitedlistAndWall As List(Of Node), ByVal y As Integer)
        For i = limits(0) + 3 To xCount + 2 Step 2
            Dim tempcell As New Node(i, y)
            If Not VisitedlistAndWall.Contains(tempcell) Then
                SetBoth(ConsoleColor.Black)
                tempcell.Print("  ")
            Else
                SetBoth(ConsoleColor.White)
                tempcell.Print("██")
            End If
        Next
    End Sub
    Sub AddStartAndEnd(ByRef Maze As List(Of Node), ByVal Limits() As Integer, ByVal EvenWidth As Integer)
        Dim AvailableStartPositions As New List(Of Node)
        For x = Limits(0) + 3 To Limits(2)
            If Maze.Contains(New Node(x, Limits(1))) Then AvailableStartPositions.Add(New Node(x, Limits(1)))
        Next
        Dim R As New Random
        Dim Index As Integer = R.Next(0, AvailableStartPositions.Count)
        Maze.Add(New Node(AvailableStartPositions(Index).X, AvailableStartPositions(Index).Y - 1))
        SetBoth(ConsoleColor.Red)
        Maze(Maze.Count - 1).Print("██")
        AvailableStartPositions.Clear()
        For x = Limits(0) + 3 To Limits(2)
            If Maze.Contains(New Node(x, Limits(3))) Then AvailableStartPositions.Add(New Node(x, Limits(3)))
        Next
        Index = R.Next(0, AvailableStartPositions.Count)
        Maze.Add(New Node(AvailableStartPositions(Index).X, AvailableStartPositions(Index).Y + 1))
        SetBoth(ConsoleColor.Green)
        Maze(Maze.Count - 1).Print("██")
        Console.BackgroundColor = (ConsoleColor.Black)
    End Sub
    Function AdjacentCheck(ByVal cell As Cell, ByVal visitedcells As Dictionary(Of Cell, Boolean))
        Dim Adjancent() As Integer = {0, 0, 0, 0}
        Dim Neighbours As New List(Of Cell)
        Dim tempcell As New Cell(cell.X, cell.Y - 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(0) = 1
        tempcell.Update(cell.X + 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(1) = 1
        tempcell.Update(cell.X, cell.Y + 2)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(2) = 1
        tempcell.Update(cell.X - 4, cell.Y)
        If If(visitedcells.ContainsKey(tempcell), visitedcells(tempcell), False) And Not visitedcells(cell) Then Adjancent(3) = 1
        Return Adjancent
    End Function
    Function PickAdjancentCell(ByVal cell As Cell, ByVal adjancencylist() As Integer)
        Dim ReturnCell As Cell
        Dim Neighbours As New List(Of Cell)
        If adjancencylist(0) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y - 2))
        End If
        If adjancencylist(1) = 1 Then
            Neighbours.Add(New Cell(cell.X + 4, cell.Y))
        End If
        If adjancencylist(2) = 1 Then
            Neighbours.Add(New Cell(cell.X, cell.Y + 2))
        End If
        If adjancencylist(3) = 1 Then
            Neighbours.Add(New Cell(cell.X - 4, cell.Y))
        End If
        Dim R As New Random
        If Neighbours.Count > 0 Then
            ReturnCell = Neighbours(R.Next(0, Neighbours.Count))
        End If
        Return ReturnCell
    End Function
    Function RanNeighbour(ByVal current As Cell, ByVal Limits() As Integer)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If newPoint.WithinLimits(Limits) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        Return neighbours
    End Function
    Function Neighbour(ByVal current As Cell, ByVal visited As Dictionary(Of Cell, Boolean), ByVal Limits() As Integer, ByVal bool As Boolean)
        Dim neighbours As New List(Of Cell)
        Dim newPoint As New Cell(current.X - 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y - 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X, current.Y + 2)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        newPoint.Update(current.X + 4, current.Y)
        If visited.ContainsKey(newPoint) Then If newPoint.WithinLimits(Limits) Then If Not visited(newPoint) Then neighbours.Add(New Cell(newPoint.X, newPoint.Y))
        If bool Then
            Return neighbours
        Else
            If neighbours.Count > 0 Then Return True
        End If
        Return False
    End Function
    Sub PrintMazeHorizontally(ByVal Maze As List(Of Node), ByVal GreatestX As Integer, ByVal GreatestY As Integer)
        For __x = 4 To GreatestX + 1 Step 2
            For __y = 1 To GreatestY + 1
                If Maze.Contains(New Node(__x, __y)) Then
                    Console.SetCursorPosition(__x, __y)
                    Console.Write("██")
                End If
            Next
        Next
    End Sub
End Module

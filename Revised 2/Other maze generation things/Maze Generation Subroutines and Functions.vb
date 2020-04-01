Module MazeGenerationSubroutinesAndFunctions
    Sub AddToPath(ByRef list As List(Of Node), cell1 As Cell, cell2 As Cell)
        Dim tempNode As New Node(cell1.X, cell1.Y)
        If Not list.Contains(tempNode) Then list.Add(New Node(cell1.X, cell1.Y))
        tempNode.Update(cell2.X, cell2.Y)
        If Not list.Contains(tempNode) Then list.Add(New Node(cell2.X, cell2.Y))
    End Sub
    Sub EraseLineHaK(limits() As Integer, xCount As Integer, visitedlistAndWall As List(Of Node), y As Integer, pathColour as ConsoleColor, backGroundColour as ConsoleColor)
        For i = limits(0) + 3 To xCount + 2 Step 2
            Dim tempCell As New Node(i, y)
            If Not visitedlistAndWall.Contains(tempcell) Then
                SetBoth(backgroundcolour)
                tempcell.Print("  ")
            Else
                SetBoth(pathcolour)
                tempcell.Print("██")
            End If
        Next
    End Sub
    Sub AddStartAndEnd(ByRef maze As List(Of Node), limits() As Integer, pathColour as ConsoleColor,Optional ByVal chooseFirstAndLast As Boolean = False)
        SetBoth(ConsoleColor.Red)
        Dim availableStartPositions As New List(Of Node)
        For x = limits(0) + 3 To limits(2)
            If maze.Contains(New Node(x, limits(1))) Then availableStartPositions.Add(New Node(x, limits(1)))
        Next
        Dim r As New Random
        dim needBackup = False
        If availableStartPositions.Count > 0
            Dim index1 As Integer = If(chooseFirstAndLast, 0, r.Next(0, availableStartPositions.Count))
            maze.Add(New Node(availableStartPositions(index1).X, availableStartPositions(index1).Y - 1))
            
            availableStartPositions.Clear()
            For x = limits(0) + 3 To limits(2)
                If maze.Contains(New Node(x, limits(3))) Then availableStartPositions.Add(New Node(x, limits(3)))
            Next
            if availableStartPositions.count > 0
index1 = If(chooseFirstAndLast, availableStartPositions.Count - 1, r.Next(0, availableStartPositions.Count))
            maze.Add(New Node(availableStartPositions(index1).X, availableStartPositions(index1).Y + 1))
            SetBoth(pathcolour)
            maze(maze.Count - 1).Print("██")
            maze(maze.Count - 2).Print("██")
            Console.BackgroundColor = (ConsoleColor.Black)
                Else 

                needbackup = True
            End If
            
        End If
        if needBackup
Console.SetCursorPosition(0, 0)
            Console.ResetColor()
            Console.Write("getting random node pairs...")
            dim pairs = getpairs(maze)
            Console.SetCursorPosition(0, 0)
            Console.Write("finding min pair...              ")
            dim index = getIndexOfMax(pairs)
            Console.SetCursorPosition(0, 0)
            Console.Write("                        ")
            maze.Add(New Node(pairs.Keys(index).Value.X, pairs.Keys(index).Value.Y))
            SetBoth(ConsoleColor.Red)
            maze(maze.Count - 1).Print("██")

            maze.Add(New Node(pairs.Keys(index).Key.X, pairs.Keys(index).Key.Y))
            maze(maze.Count - 1).Print("██")
        End If
            

    End Sub
    Function getpairs(maze As List(Of Node)) As Dictionary(Of KeyValuePair(Of node,Node), Integer)

        dim values as New Dictionary(Of KeyValuePair(Of node,Node), Integer)

        dim indexes as New List(Of Integer)
        dim r as New Random
        for i = 0 to maze.Count-1
            indexes.Add(r.Next(maze.Count-1))
        Next
        dim reverseIndex As New List(Of Integer)
        for i = 0 to maze.Count-1
            reverseIndex.Add(r.Next(maze.Count-1))
        Next

        reverseIndex.Reverse()

        for i = 0 to maze.Count-1
            dim node1 = maze(indexes(i))
            dim node2 = maze(reverseIndex(i))
            if values.ContainsKey(New KeyValuePair(Of Node,Node)(node1,node2)) then continue for
            values.Add(New KeyValuePair(Of Node,Node)(node1,node2), GetDistance(node1,node2))
        Next

        Return values
    End Function

    function getIndexOfMax(values as Dictionary(Of KeyValuePair(Of node,Node), Integer)) As Integer

        dim curDist = values.Values.First()
        dim returnIndex = 0

        for i = 0 To values.Values.Count-1

            If values.Values(i) > curdist
                curDist = values.Values(i)
                returnIndex = i
            End If

        Next

        Return returnIndex
    End function

    Function AdjacentCheck(cell As Cell, visitedcells As Dictionary(Of Cell, Boolean))
        Dim adjancent() As Integer = {0, 0, 0, 0}
        Dim neighbours As New List(Of Cell)
        Dim tempCell As New Cell(cell.X, cell.Y - 2)
        If visitedcells.ContainsKey(tempCell) AndAlso visitedcells(tempCell) And Not visitedcells(cell) Then adjancent(0) = 1
        tempCell.Update(cell.X + 4, cell.Y)
        If visitedcells.ContainsKey(tempCell) AndAlso visitedcells(tempCell) And Not visitedcells(cell) Then adjancent(1) = 1
        tempCell.Update(cell.X, cell.Y + 2)
        If visitedcells.ContainsKey(tempCell) AndAlso visitedcells(tempCell) And Not visitedcells(cell) Then adjancent(2) = 1
        tempCell.Update(cell.X - 4, cell.Y)
        If visitedcells.ContainsKey(tempCell) AndAlso visitedcells(tempCell) And Not visitedcells(cell) Then adjancent(3) = 1
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

    Function InitialiseVisited(limits() As Integer) As Dictionary(Of Cell, Boolean)
        Dim dict As New Dictionary(Of Cell, Boolean)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                dict(New Cell(x, y)) = False
            Next
        Next
        Return dict
    End Function
    Function MidPoint(cell1 As Object, cell2 As Object)
        If cell1.GetType.ToString = "NEA_2019.Cell" Then
            Return New Cell((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        Else
            Return New Node((cell1.X + cell2.X) / 2, (cell1.Y + cell2.Y) / 2)
        End If
    End Function
    Function PickRandomStartingCell(limits() As Integer) As Cell
        Dim li As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                li.Add(New Cell(x, y))
            Next
        Next
        Dim r As New Random
        Return li(r.Next(0, li.Count - 1))
    End Function

End Module

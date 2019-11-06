Imports Enumerable = System.Linq.Enumerable

Module WilsonsAlgorithm
    Function expandingCircle(limits() As Integer) As List(Of Cell)
        Dim availableCells As New List(Of Cell)
        SetBoth(ConsoleColor.White)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                'top to bottom
                availableCells.Add(New Cell(x, y))
            Next
        Next
        Dim cellList As New List(Of Cell)
        Dim circleCentre As New Cell((limits(2) + 2) \ 2, (limits(3) + 1) \ 2)
        If circleCentre.Y Mod 2 = 0 Then circleCentre.Y += 1
        While Not availableCells.Contains(circleCentre)
            circleCentre.Update(circleCentre.X + 1, circleCentre.Y)
        End While
        Dim discovered As New Dictionary(Of Cell, Boolean)
        For Each cell In availableCells
            discovered(cell) = Nothing
        Next
        Dim q As New Queue(Of Cell)
        SetBoth(ConsoleColor.Green)
        q.Enqueue(circleCentre)
        discovered(circleCentre) = True
        While q.Count > 0
            Dim v = q.Dequeue()
            If Not cellList.Contains(New Cell(v.X, v.Y)) Then cellList.Add(New Cell(v.X, v.Y))
            For Each w As Cell In adjacentVertices(v, availableCells)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w)
                End If
            Next
        End While
        Return cellList
    End Function
    Function adjacentVertices(ByRef current As Cell, ByRef availablepath As List(Of Cell))
        Dim neighbours As New List(Of Cell)
        Dim newNode As New Cell(current.X, current.Y - 2)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X + 4, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X, current.Y + 2)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        newNode.Update(current.X - 4, current.Y)
        If availablepath.Contains(newNode) Then neighbours.Add(New Cell(newNode.X, newNode.Y))
        Return neighbours
    End Function
    Function collapsingRectangle(limits() As Integer)
        Dim totalCount As Integer = 0
        Dim gridCells As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                totalCount += 1
                gridCells.Add(New Cell(x, y))
            Next
        Next
        Dim availableCells As New List(Of Cell)
        Dim topLeft As New Cell(limits(0) + 3, limits(1))
        Dim topRight As New Cell(limits(2) - 1, limits(1))
        If Not gridCells.Contains(topRight) Then topRight.Update(topRight.X - 2, topRight.Y)
        Dim bottomLeft As New Cell(limits(0) + 3, limits(3))
        Dim bottomRight As New Cell(limits(2) - 1, limits(3))
        If Not gridCells.Contains(bottomRight) Then bottomRight.Update(bottomRight.X - 2, bottomRight.Y)
        Do
            For i = topLeft.X To topRight.X Step 4
                If Not availableCells.Contains(New Cell(i, topLeft.Y)) Then availableCells.Add(New Cell(i, topLeft.Y))
            Next
            For i = topRight.Y To bottomRight.Y Step 2
                If Not availableCells.Contains(New Cell(topRight.X, i)) Then availableCells.Add(New Cell(topRight.X, i))
            Next
            For i = bottomRight.X To bottomLeft.X Step -4
                If Not availableCells.Contains(New Cell(i, bottomLeft.Y)) Then availableCells.Add(New Cell(i, bottomLeft.Y))
            Next
            For i = bottomLeft.Y To topLeft.Y Step -2
                If Not availableCells.Contains(New Cell(topLeft.X, i)) Then availableCells.Add(New Cell(topLeft.X, i))
            Next
            topLeft = goIn("topleft", topLeft)
            topRight = goIn("topright", topRight)
            bottomLeft = goIn("bottomleft", bottomLeft)
            bottomRight = goIn("bottomright", bottomRight)
        Loop Until availableCells.Count = totalCount
        Return availableCells
    End Function
    Function goIn(rule As String, cell As Cell)
        If rule = "topleft" Then
            Return New Cell(cell.X + 4, cell.Y + 2)
        ElseIf rule = "topright" Then
            Return New Cell(cell.X - 4, cell.Y + 2)
        ElseIf rule = "bottomleft" Then
            Return New Cell(cell.X + 4, cell.Y - 2)
        ElseIf rule = "bottomright" Then
            Return New Cell(cell.X - 4, cell.Y - 2)
        End If
        Return Nothing
    End Function

    Function WilsonsRefectored(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, cellPickingMethod As String, pathColour As ConsoleColor, backGroundColour As ConsoleColor)
        Dim cellList As New List(Of Cell)
        If cellPickingMethod = "top to bottom" Or cellPickingMethod = "bottom to top" Then
            For y = limits(1) To limits(3) Step 2
                For x = limits(0) + 3 To limits(2) Step 4
                    'top to bottom
                    cellList.Add(New Cell(x, y))
                Next
            Next
            If cellPickingMethod = "bottom to top" Then cellList.Reverse()
        ElseIf cellPickingMethod = "left to right" Or cellPickingMethod = "right to left" Then
            For x = limits(0) + 3 To limits(2) Step 4
                For y = limits(1) To limits(3) Step 2
                    cellList.Add(New Cell(x, y))
                Next
            Next
            If cellPickingMethod = "right to left" Then cellList.Reverse()
        ElseIf cellPickingMethod = "random" Then
            For x = limits(0) + 3 To limits(2) Step 4
                For y = limits(1) To limits(3) Step 2
                    cellList.Add(New Cell(x, y))
                Next
            Next
            ShuffleCells(cellList)
        ElseIf cellPickingMethod = "collapsing rectangle" Then
            cellList = collapsingRectangle(limits)
        ElseIf cellPickingMethod = "expanding rectangle" Then
            cellList = collapsingRectangle(limits)
            cellList.Reverse()
        ElseIf cellPickingMethod = "collapsing diamond" Then
            cellList = expandingCircle(limits)
            cellList.Reverse()
        ElseIf cellPickingMethod = "expanding diamond" Then
            cellList = expandingCircle(limits)
            'cellList.Reverse()
        End If
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim r As New Random
        Dim fullMaze As New List(Of Node)
        Dim availablePositions, UST, randomWalkCells As New List(Of Cell)
        Dim cameFrom As New Dictionary(Of Cell, Cell)
        Dim radius = 2
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                availablePositions.Add(New Cell(x, y))
                cameFrom(New Cell(x, y)) = Nothing
            Next
        Next
        Dim startCell = PickRandomStartingCell(limits)
        UST.Add(startCell)
        SetBoth(pathColour)
        If showMazeGeneration Then UST(0).Print("XX")
        availablePositions.Remove(startCell)
        cellList.Remove(startCell)
        Dim currentCell As Cell = cellList(0) 'PickRandomCell(availablePositions, UST, limits, cellPickingMethod, cellList)
        randomWalkCells.Add(currentCell)
        Dim previousCell = currentCell
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While availablePositions.Count > 0
            If ExitCase() Then Return Nothing
            Dim immediateNeighbours = RanNeighbour(currentCell, limits)
            currentCell = immediateNeighbours(r.Next(immediateNeighbours.count))
            If showMazeGeneration Then
                SetBoth(ConsoleColor.Blue)
                currentCell.Print("XX")
                SetBoth(pathColour)
                previousCell.Print("XX")
                Threading.Thread.Sleep(delay \ 2)
            End If
            cameFrom(previousCell) = currentCell
            If UST.Contains(currentCell) Then 'if the cell is in the uniform spanning tree
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    currentCell.Print("XX")
                End If
                Dim backtrackingCell = randomWalkCells(0)
                If showMazeGeneration Then backtrackingCell.Print("XX")
                Dim pathToUst As New List(Of Cell) From {
                    backtrackingCell
                }
                Do
                    backtrackingCell = cameFrom(backtrackingCell)
                    pathToUst.Add(backtrackingCell)
                Loop Until UST.Contains(backtrackingCell)
                fullMaze.Add(pathToUst(0).ToNode())
                availablePositions.Remove(pathToUst(0))
                cellList.Remove(pathToUst(0))
                If showMazeGeneration Then pathToUst(0).Print("XX")
                UST.AddRange(pathToUst)
                For i = 0 To pathToUst.Count - 2
                    Dim wall As Cell = MidPoint(pathToUst(i), pathToUst(i + 1))
                    availablePositions.Remove(pathToUst(i + 1))
                    cellList.Remove(pathToUst(i + 1))
                    If showMazeGeneration Then
                        wall.Print("XX")
                        pathToUst(i + 1).Print("XX")
                        Threading.Thread.Sleep(delay \ 2)
                    End If
                    fullMaze.Add(wall.ToNode())
                    fullMaze.Add(pathToUst(i + 1).ToNode())
                Next
                SetBoth(backGroundColour)
                For Each thing In From thing1 In randomWalkCells Where Not UST.Contains(thing1)
                    If showMazeGeneration Then thing.Print("XX")
                Next
                For y = limits(1) To limits(3) Step 2
                    For x = limits(0) + 3 To limits(2) - 1 Step 4
                        cameFrom(New Cell(x, y)) = Nothing
                    Next
                Next
                randomWalkCells.Clear()
                SetBoth(pathColour)
                If availablePositions.Count = 0 Or cellList.Count = 0 Then Exit While 'there are no available cells, therefore the maze is done
                currentCell = cellList(0) 'PickRandomCell(availablePositions, UST, limits, cellPickingMethod, cellList)
                radius += 1
                previousCell = currentCell
                randomWalkCells.Add(currentCell)
                If showMazeGeneration Then currentCell.Print("XX")
            Else
                cameFrom(previousCell) = currentCell
                If Not randomWalkCells.Contains(previousCell) Then randomWalkCells.Add(previousCell)
                previousCell = currentCell
            End If
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(fullMaze, limits(2), limits(3))
        End If
        AddStartAndEnd(fullMaze, limits, pathColour)
        Return fullMaze
    End Function
End Module
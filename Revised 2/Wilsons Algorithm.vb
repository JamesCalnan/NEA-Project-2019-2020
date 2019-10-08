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
        While Not availableCells.Contains(circleCentre)
            circleCentre.Update(circleCentre.X + 1, circleCentre.Y + 1)
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
            For Each w As Cell In adjacentVertices(v, availableCells)
                If Not discovered(w) Then
                    discovered(w) = True
                    q.Enqueue(w)
                    cellList.Add(New Cell(w.X, w.Y))
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
    Function collapsingDiamond(limits() As Integer)
        Dim totalCount As Integer = 0
        Dim gridCells As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) Step 4
                totalCount += 1
                gridCells.Add(New Cell(x, y))
            Next
        Next
        Dim topLeft As New Cell(limits(0) + 3, limits(1))
        Dim topRight As New Cell(limits(2) - 1, limits(1))
        If Not gridCells.Contains(topRight) Then topRight.Update(topRight.X - 2, topRight.Y)
        Dim bottomLeft As New Cell(limits(0) + 3, limits(3))
        Dim bottomRight As New Cell(limits(2) - 1, limits(3))
        If Not gridCells.Contains(bottomRight) Then bottomRight.Update(bottomRight.X - 2, bottomRight.Y)
        Dim availableCells As New List(Of Cell) From {
                topLeft,
                topRight,
                bottomLeft,
                bottomRight
            }
        Do
            Dim tempCorner As New Cell(topLeft.X, topLeft.Y)
            tempCorner.Update(tempCorner.X + 4, tempCorner.Y)
            If tempCorner.WithinLimits(limits) Then
                If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                Do
                    If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                    tempCorner.Update(tempCorner.X - 4, tempCorner.Y + 2)
                Loop Until Not tempCorner.WithinLimits(limits)
                topLeft.Update(topLeft.X + 4, topLeft.Y)
            End If
            tempCorner.Update(topRight.X - 4, topRight.Y)
            If tempCorner.WithinLimits(limits) Then
                If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                Do
                    If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                    tempCorner.Update(tempCorner.X + 4, tempCorner.Y + 2)
                Loop Until Not tempCorner.WithinLimits(limits)
                topRight.Update(topRight.X - 4, topRight.Y)
            End If
            tempCorner.Update(bottomLeft.X + 4, bottomLeft.Y)
            If tempCorner.WithinLimits(limits) Then
                If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                Do
                    If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                    tempCorner.Update(tempCorner.X - 4, tempCorner.Y - 2)
                Loop Until Not tempCorner.WithinLimits(limits)
                bottomLeft.Update(bottomLeft.X + 4, bottomLeft.Y)
            End If
            tempCorner.Update(bottomRight.X - 4, bottomRight.Y)
            If tempCorner.WithinLimits(limits) Then
                If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                Do
                    If Not availableCells.Contains(New Cell(tempCorner.X, tempCorner.Y)) Then availableCells.Add(New Cell(tempCorner.X, tempCorner.Y))
                    tempCorner.Update(tempCorner.X + 4, tempCorner.Y - 2)
                Loop Until Not tempCorner.WithinLimits(limits)
                bottomRight.Update(bottomRight.X - 4, bottomRight.Y)
            End If
        Loop Until availableCells.Count > totalCount
        For Each cell In From cell1 In gridCells Where Not availableCells.Contains(cell1)
            availableCells.Add(cell)
        Next
        Return availableCells
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
            cellList = collapsingDiamond(limits)
        ElseIf cellPickingMethod = "expanding diamond" Then
            cellList = collapsingDiamond(limits)
            cellList.Reverse()
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
        While availablePositions.Count > 0
            If ExitCase() Then Return Nothing
            Dim immediateNeighbours = RanNeighbour(currentCell, limits)
            currentCell = immediateNeighbours(r.Next(immediateNeighbours.count))
            SetBoth(pathColour)
            If showMazeGeneration Then currentCell.Print("XX")
            cameFrom(previousCell) = currentCell
            If UST.Contains(currentCell) Then 'if the cell is in the uniform spanning tree
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
                        Threading.Thread.Sleep(delay)
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
                If availablePositions.Count = 0 Then Exit While 'there are no available cells, therefore the maze is done
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
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(fullMaze, limits(2), limits(3))
        End If
        AddStartAndEnd(fullMaze, limits, pathColour)
        Return fullMaze
    End Function
    Function PickRandomCell(availablePositions As List(Of Cell), ust As List(Of Cell), limits() As Integer, version As String, CellPick As List(Of Cell))
        Dim ran As New Random
        Dim startingCell As New Cell(ran.Next(limits(1), limits(3)), ran.Next(limits(0) + 3, limits(2) - 1))

        If version = "expanding circle" Then
            Dim radius = 5
            Dim availableCells As New List(Of Cell)
            Dim c As New Cell((limits(2) + 2) \ 2, (limits(3) + 1) \ 2) 'circle centre
            For i = 1 To 360
                If New Cell(((c.X + radius * Math.Cos(i)) * 2) - c.X, c.Y + radius * Math.Sin(i)).WithinLimits(limits) And Not availableCells.Contains(New Cell(((c.X + radius * Math.Cos(i)) * 2) - c.X, c.Y + radius * Math.Sin(i))) And availablePositions.Contains(New Cell(((c.X + radius * Math.Cos(i)) * 2) - c.X, c.Y + radius * Math.Sin(i))) Then availableCells.Add(New Cell(((c.X + radius * Math.Cos(i)) * 2) - c.X, c.Y + radius * Math.Sin(i)))
            Next
            If availableCells.Count > 0 Then Return availableCells(ran.Next(availableCells.Count))
            For Each cell In From cell1 In availableCells Where Not ust.Contains(cell1)
                Return cell
            Next
        End If
        If version = "top to bottom" Then
            Return CellPick(0)
            For Each cell In From cell1 In CellPick Where Not ust.Contains(cell1)
                Return cell
            Next
        ElseIf version = "bottom to top" Then
            Return CellPick(0)
            For Each cell In From cell1 In CellPick Where Not ust.Contains(cell1)
                Return cell
            Next
        End If
        If version = "left to right" Then
            Return CellPick(0)
            For Each cell In From cell1 In CellPick Where Not ust.Contains(cell1)
                Return cell
            Next
        ElseIf version = "right to left" Then
            Return CellPick(0)
            'For Each cell In From cell1 In CellPick Where Not ust.Contains(cell1)
            '    Return cell
            'Next
        End If
        If version = "random" Then
            Return CellPick(0)
            For Each cell In From cell1 In CellPick Where Not ust.Contains(cell1)
                Return cell
            Next
        End If
        Do
            Dim idx As Integer = ran.Next(0, availablePositions.Count)
            startingCell.Update(availablePositions(idx).X, availablePositions(idx).Y)
            If Not ust.Contains(startingCell) Then
                Exit Do
            End If
        Loop
        Return startingCell
    End Function
End Module
Module Dungeon_Creation
    'Steps when making a dungeon
    '   Place a bunch of rectangles in random places make sure they dont overlap
    ''' <summary>
    ''' Pick a random cell from the grid but unlike the PickRandomStartingCell function it takes into account the visited cells
    ''' </summary>
    ''' <param name="limits"></param>
    ''' <param name="visitedCells"></param>
    ''' <returns></returns>
    Function PickStartingCellWithVisited(limits() As Integer, visitedCells As Dictionary(Of Cell, Boolean)) As Cell
        Dim li As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                If Not visitedCells(New Cell(x, y)) Then li.Add(New Cell(x, y))
            Next
        Next
        Dim r As New Random
        Return li(r.Next(0, li.Count - 1))
    End Function
    Function createPassages(limits() As Integer, showMazeGeneration As Boolean, pathColour As ConsoleColor, backGroundColour As ConsoleColor, delay As Integer)
        Dim r As New Random
        If backGroundColour <> ConsoleColor.Black Then DrawBackground(backGroundColour, limits)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim returnablePath As New List(Of Node)
        Dim roomSets As Dictionary(Of Cell, Integer) = GrenerateRooms(limits, pathColour, delay, showMazeGeneration)
        For Each cell In roomSets.Keys
            visitedCells(cell) = True
        Next
        Dim currentCell As Cell = PickStartingCellWithVisited(limits, visitedCells)
        Dim prevCell As Cell = currentCell
        Dim stack As New Stack(Of Cell)
        SetBoth(pathColour)
        visitedCells(currentCell) = True
        stack.Push(currentCell)
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        If showMazeGeneration Then currentCell.Print("██")
        While True
            If ExitCase() Then Return Nothing
            If showMazeGeneration Then
                prevCell.Print("██")
                SetBoth(pathColour)
            End If
            Dim recentCells As List(Of Cell) = Neighbour(currentCell, visitedCells, limits)
            If recentCells.Count > 0 Then
                Dim temporaryCell As Cell = recentCells(r.Next(recentCells.Count))
                visitedCells(temporaryCell) = True
                stack.Push(temporaryCell)
                Dim wallCell As Cell = MidPoint(currentCell, temporaryCell)
                currentCell = temporaryCell
                AddToPath(returnablePath, temporaryCell, wallCell)
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    wallCell.Print("██")
                    temporaryCell.Print("██")
                    prevCell = currentCell
                End If
            ElseIf stack.Count > 1 Then
                currentCell = stack.Pop
                If showMazeGeneration Then
                    SetBoth(pathColour)
                    currentCell.Print("██")
                    prevCell = currentCell
                End If
            Else
                Exit While
            End If
            Threading.Thread.Sleep(delay)
        End While
        Dim rooms As List(Of Cell) = roomSets.Keys.ToList

        Dim path As List(Of Cell) = (From cell In visitedCells Where cell.Value And returnablePath.Contains(cell.Key.ToNode) Select cell.Key).ToList()

        Dim connections As List(Of Cell) = GetAvailableConnections(limits, rooms, path)
        For Each cell In path
            roomSets(cell) = -1
        Next
        returnablePath.AddRange(From cell In rooms Select cell.ToNode)
        While UniqueComponentNumber(roomSets)
            Dim index = r.Next(connections.Count)
            Dim connection = connections(index)
            Dim surroundingCells As New List(Of Cell) From {
               New Cell(connection.X + 2, connection.Y),
               New Cell(connection.X - 2, connection.Y),
               New Cell(connection.X, connection.Y + 1),
               New Cell(connection.X, connection.Y - 1)}
            Dim adjacentCells As New List(Of Cell)
            For Each cell In surroundingCells
                For Each cell1 In From cell11 In surroundingCells Where path.Contains(cell) And rooms.Contains(cell11)
                    adjacentCells.Add(cell)
                    adjacentCells.Add(cell1)
                Next
            Next
            If roomSets(adjacentCells(1)) <> roomSets(adjacentCells(0)) Then
                Dim setNumToBeChanged As Integer = roomSets(adjacentCells(1))
                Dim cellsToBeChanged As List(Of Cell) = (From thing In roomSets Where thing.Value = setNumToBeChanged Select thing.Key).ToList()
                For Each thing In cellsToBeChanged
                    roomSets(thing) = roomSets(adjacentCells(0))
                Next
                If showMazeGeneration Then connection.Print("XX")
                returnablePath.Add(connection.ToNode)
            End If
            connections.RemoveAt(index)
        End While
        returnablePath = DeadEndFiller(returnablePath, True, delay, backGroundColour, backGroundColour, False, False)
        If Not showMazeGeneration Then
            SetBoth(pathColour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        AddStartAndEnd(returnablePath, limits, pathColour)
        'returnablePath = DeadEndFiller(returnablePath, True, delay, backGroundColour, backGroundColour, False)
        Return returnablePath

    End Function
    Function GetAvailableConnections(limits() As Integer, rooms As List(Of Cell), path As List(Of Cell)) As List(Of Cell)
        Dim availableConnections As New List(Of Cell)

        For y = limits(1) To limits(3) Step 1
            For x = limits(0) + 3 To limits(2) - 1 Step 2
                If rooms.Contains(New Cell(x, y)) OrElse path.Contains(New Cell(x, y)) Then Continue For
                Dim surroundingCells As New List(Of Cell) From {
                        New Cell(x + 2, y),
                        New Cell(x - 2, y),
                        New Cell(x, y + 1),
                        New Cell(x, y - 1)
                }
                For Each cell In surroundingCells
#Disable Warning BC42327 ' Using the iteration variable in a query expression may have unexpected results
                    availableConnections.AddRange(From cell1 In surroundingCells Where path.Contains(cell) AndAlso rooms.Contains(cell1) Select New Cell(x, y))
#Enable Warning BC42327 ' Using the iteration variable in a query expression may have unexpected results
                Next
            Next
        Next
        Return availableConnections
    End Function
    Function ReturnAvailableCells(limits() As Integer) As List(Of Cell)
        Dim availableCells As New List(Of Cell)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                If x <> limits(0) + 3 AndAlso x <> limits(2) - 1 AndAlso y <> limits(1) AndAlso y <> limits(3) Then
                    availableCells.Add(New Cell(x, y))
                End If
            Next
        Next
        Return availableCells
    End Function
    Function GrenerateRooms(limits() As Integer, pathColour As ConsoleColor, delay As Integer, showMazeGeneration As Boolean) As Dictionary(Of Cell, Integer)
        SetBoth(pathColour)
        Dim r As New Random
        Dim availableCells = ReturnAvailableCells(limits)
        Dim rooms As New List(Of Cell)
        Dim roomSets As New Dictionary(Of Cell, Integer)
        Dim currentSet As Integer = 0
        For Each cornerCell In From cornerCell1 In availableCells Where r.Next(1, 11) <= 2
            Dim potentialRoom As New List(Of Cell)
            Dim width As Integer = r.Next(2, 6)
            While width Mod 4 <> 0
                width += 1
            End While
            Dim height As Integer = r.Next(2, 6)
            While height Mod 2 <> 0
                height += 1
            End While
            For x = cornerCell.X To cornerCell.X + width Step 2
                For y = cornerCell.Y To cornerCell.Y + height
                    potentialRoom.Add(New Cell(x, y))
                Next
            Next
            Dim validRoom As Boolean = potentialRoom.All(Function(cell) Not rooms.Contains(cell) And cell.WithinLimits(limits) And AdjacentRoom(cell, rooms))
            If potentialRoom.Count < 2 Then validRoom = False
            If validRoom Then
                For Each cell In potentialRoom
                    roomSets.Add(cell, currentSet)
                    If showMazeGeneration Then cell.Print("XX")
                    rooms.Add(cell)
                Next
                Threading.Thread.Sleep(delay)
                currentSet += 1
            End If
        Next
        Return roomSets
    End Function
    Function AdjacentRoom(centreCell As Cell, rooms As List(Of Cell)) As Boolean
        Dim potentialRoomCells As New List(Of Cell) From {
                New Cell(centreCell.X - 4, centreCell.Y),
                New Cell(centreCell.X, centreCell.Y - 2),
                New Cell(centreCell.X + 4, centreCell.Y),
                New Cell(centreCell.X, centreCell.Y + 2),
                New Cell(centreCell.X + 4, centreCell.Y + 2),
                New Cell(centreCell.X + 4, centreCell.Y - 2),
                New Cell(centreCell.X - 4, centreCell.Y + 2),
                New Cell(centreCell.X - 4, centreCell.Y - 2)
        }
        Return potentialRoomCells.All(Function(cell) Not rooms.Contains(cell))
    End Function
End Module
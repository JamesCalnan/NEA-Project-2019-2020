﻿Module Prims
    Function Prims_Simplified(limits() As Integer, delay As Integer, showMazeGeneration As Boolean,  pathColour as Consolecolor,  backGroundColour as ConsoleColor)
        'Assumes the weights of each cell in the grid is the same and therefore chooses a random cell from the frontier set
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,limits)
        Dim r As New Random
        Dim frontierSet As New List(Of Cell)
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(Limits(0) + 3, Limits(1) + 2)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim wallCell As Cell
        Dim returnablePath As New List(Of Node)
        visitedCells(currentCell) = True
        SetBoth(pathColour)
        If showMazeGeneration Then currentCell.Print("██")
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        While True
            If ExitCase() Then Return Nothing
            For Each cell As Cell In Neighbour(currentCell, visitedCells, limits)
                If Not frontierSet.Contains(cell) Then frontierSet.Add(cell)
            Next
            If frontierSet.Count = 0 Then Exit While
            currentCell = frontierSet(r.Next(0, frontierSet.Count))
            Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
            Dim previousCell As Cell = PickAdjancentCell(currentCell, adjancencyList)
            wallCell = MidPoint(currentCell, previousCell)
            If showMazeGeneration Then
                wallCell.Print("██")
                currentCell.Print("██")
                Threading.Thread.Sleep(delay)
            End If
            visitedCells(currentCell) = True
            frontierSet.Remove(currentCell)
            AddToPath(returnablePath, currentCell, wallCell)
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
    Function Prims_True(limits() As Integer, delay As Integer, showMazeGeneration As Boolean, pathColour as Consolecolor,  backGroundColour as ConsoleColor)
        'Assigns a random weight between 0, 99 to each available in the grid, it then chooses the cell with the lowest weight out of the frontier set
        If backGroundColour <> ConsoleColor.black Then DrawBackground(backGroundColour,limits)
        Dim r As New Random
        Dim frontierSet As New List(Of Cell)
        Dim wallCell As Cell
        Dim returnablePath As New List(Of Node)
        Dim visitedCells As Dictionary(Of Cell, Boolean) = InitialiseVisited(limits)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Dim weights As New Dictionary(Of Cell, Integer)
        Dim weightsprioqueue As New PriorityQueue(Of Node) 'Dictionary(Of Cell, Integer)
        For y = limits(1) To limits(3) Step 2
            For x = limits(0) + 3 To limits(2) - 1 Step 4
                Dim tempNode As New Cell(x, y)
                'weightsprioqueue.Enqueue(New Node(x, y), r.Next(0, 99))
                weights(tempNode) = r.Next(0, 99) 'Assigning random weights to each cell in the grid
            Next
        Next
        Dim currentCell As Cell = PickRandomStartingCell(limits) '(FrontierSet(idx).X, FrontierSet(idx).Y) '(Limits(0) + 3, Limits(1) + 2)
        visitedCells(currentCell) = True
        SetBoth(pathcolour)
        If showMazeGeneration Then currentCell.Print("██")
        returnablePath.Add(New Node(currentCell.X, currentCell.Y))
        While True
            If ExitCase() Then Return Nothing
            For Each cell In From cell1 In Neighbour(currentCell, visitedCells, limits) Where Not frontierSet.Contains(cell1)
                frontierSet.Add(cell)
                weightsprioqueue.Enqueue(cell.ToNode(), weights(cell))
            Next
            If frontierSet.Count = 0 Then Exit While
            'Dim highestWeightCell As Cell = weightsprioqueue.ExtractMin().ToCell() 'frontierSet(0)
            'For Each cell In frontierSet
            '    If weights(highestWeightCell) < weights(cell) Then highestWeightCell = cell
            'Next
            currentCell = weightsprioqueue.ExtractMin().ToCell()
            Dim adjancencyList() As Integer = AdjacentCheck(currentCell, visitedCells)
            Dim previousCell As Cell = PickAdjancentCell(currentCell, adjancencyList)
            wallCell = MidPoint(currentCell, previousCell)
            If showMazeGeneration Then
                wallCell.Print("██")
                currentCell.Print("██")
            End If
            visitedCells(currentCell) = True
            frontierSet.Remove(currentCell)
            AddToPath(returnablePath, currentCell, wallCell)
            Threading.Thread.Sleep(delay)
        End While
        PrintMessageMiddle($"Time taken to generate the maze: {stopwatch.Elapsed.TotalSeconds}", 1, ConsoleColor.Yellow)
        If Not showMazeGeneration Then
            SetBoth(pathcolour)
            PrintMazeHorizontally(returnablePath, limits(2), limits(3))
        End If
        Dim ypos As Integer = Console.CursorTop
        AddStartAndEnd(returnablePath, limits, pathcolour)
        Console.SetCursorPosition(0, ypos)
        Return returnablePath
    End Function
End Module

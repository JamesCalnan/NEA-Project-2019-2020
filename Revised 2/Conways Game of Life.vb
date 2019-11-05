Imports System.Deployment.Application

Module Conways_Game_of_Life

    Sub simulateLife(limits() As Integer, forMazeGeneration As Boolean)
        Dim r As New Random
        Dim grid(limits(3) - 2, limits(2) \ 2) As Boolean
        Dim minX As Integer = Math.Floor(((limits(2) \ 2) * 2) / 5)
        Dim maxX As Integer = Math.Floor(((limits(2) \ 2) * 3) / 5) + 1
        Dim minY As Integer = Math.Floor(((limits(3) - 2) * 2) / 5)
        Dim maxY As Integer = Math.Floor(((limits(3) - 2) * 3) / 5) + 1

        For i = 0 To grid.GetUpperBound(0)
            For j = 0 To grid.GetUpperBound(1)
                If forMazeGeneration Then
                    If i > minY And i < maxY And j > minX And j < maxX Then grid(i, j) = r.Next(11) < 2 'j Mod 3
                Else
                    grid(i, j) = r.Next(11) < 3
                End If
            Next
        Next
        Console.ForegroundColor = ConsoleColor.White
        PrintMessageMiddle("Press escape to exit", Console.WindowHeight - 2, ConsoleColor.White)
        While 1
            If ExitCase() Then Exit While
            Console.SetCursorPosition(0, 0)
            animateGrid(grid)
            Dim duplicateGrid As Boolean(,) = grid.Clone()
            For i = 0 To duplicateGrid.GetUpperBound(0)
                For j = 0 To duplicateGrid.GetUpperBound(1)
                    Dim neighbours As Integer = countNeighbours(i, j, grid)
                    Dim state As Boolean = duplicateGrid(i, j)
                    If Not state And neighbours = 3 Then
                        duplicateGrid(i, j) = True
                    ElseIf If(forMazeGeneration, state And (neighbours >= 1 And neighbours <= 5), state And (neighbours < 2 Or neighbours > 3)) Then 'state And (neighbours < 2 Or neighbours > 3)
                        duplicateGrid(i, j) = If(forMazeGeneration, True, False)
                    Else
                        duplicateGrid(i, j) = If(forMazeGeneration, False, state)
                    End If
                Next
            Next
            grid = duplicateGrid.Clone()
        End While
    End Sub
    Sub animateGrid(grid(,) As Boolean)
        Dim nl = Environment.NewLine
        Dim outputString As String = ""
        outputString += Environment.NewLine + Environment.NewLine + Environment.NewLine
        For i = 0 To grid.GetUpperBound(0)
            outputString += "      "
            For j = 0 To grid.GetUpperBound(1)
                outputString += If(grid(i, j), "██", "  ")
            Next
            outputString += nl
        Next
        Console.Write(outputString)
    End Sub

    Function countNeighbours(row As Integer, column As Integer, grid As Boolean(,)) As Integer
        Dim sum As Integer
        For i As Integer = -1 To 1
            For j As Integer = -1 To 1
                If i = 0 And j = 0 Then Continue For
                Dim currentRow = (row + i + grid.GetUpperBound(0)) Mod grid.GetUpperBound(0)
                Dim currentColumn = (column + j + grid.GetUpperBound(1)) Mod grid.GetUpperBound(1)
                If grid(currentRow, currentColumn) Then sum += 1
            Next
        Next
        Return sum
    End Function

End Module

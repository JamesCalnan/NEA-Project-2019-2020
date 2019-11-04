Imports System.Deployment.Application

Module Conways_Game_of_Life

    Sub simulateLife(limits() As Integer)
        Dim r As New Random
        Dim grid(limits(3) - 2, limits(2) \ 2) As Boolean
        Dim minX As Integer = Math.Floor(grid.GetLength(0) * 2 / 5)
        Dim maxX As Integer = Math.Floor(grid.GetLength(0) * 3 / 5) + 1
        Dim minY As Integer = Math.Floor(grid.GetLength(1) * 2 / 5)
        Dim maxY As Integer = Math.Floor(grid.GetLength(1) * 3 / 5) + 1
        For i = 0 To grid.GetUpperBound(0)
            For j = 0 To grid.GetUpperBound(1)
                If i > minY And i < maxY And j > minX And j < maxX Then grid(i, j) = j Mod 2
            Next
        Next
        Console.BackgroundColor = ConsoleColor.White
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Black
        While 1

            If Console.KeyAvailable Then Exit While
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
                    ElseIf state And (neighbours >= 1 And neighbours <= 5) Then ' state And (neighbours < 2 Or neighbours > 3)
                        duplicateGrid(i, j) = True
                    Else
                        duplicateGrid(i, j) = False
                    End If
                Next
            Next
            grid = duplicateGrid.Clone()
        End While
    End Sub
    Sub animateGrid(grid(,) As Boolean)
        Dim nl = Environment.NewLine
        Dim outString As String = ""
        outString += Environment.NewLine + Environment.NewLine + Environment.NewLine
        For i = 0 To grid.GetUpperBound(0)
            outString += "        "
            For j = 0 To grid.GetUpperBound(1)
                outString += If(grid(i, j), "██", "  ")
            Next
            outString += nl
        Next
        Console.Write(outString)
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

Module Conways_Game_of_Life_OOP
    Sub simulateLifeOOP(limits() As Integer)


        Dim changedCells As New List(Of Cell)
        Dim cellStates As Dictionary(Of Cell, Boolean) = initialiseGrid(limits, changedCells)
        animateGridOOP(cellStates, changedCells)
        While 1
            computeChangesOOP(cellStates, limits, changedCells)
            animateGridOOP(cellStates, changedCells)
        End While



    End Sub
    Function initialiseGrid(limits() As Integer, changedCells As List(Of Cell)) As Dictionary(Of Cell, Boolean)
        Dim r As New Random
        Dim dict As New Dictionary(Of Cell, Boolean)
        For y = limits(1) To limits(3) Step 1
            For x = limits(0) + 3 To limits(2) - 1 Step 2
                dict(New Cell(x, y)) = r.Next(9) < 3
                changedCells.Add(New Cell(x, y))
            Next
        Next
        Return dict
    End Function

    Sub animateGridOOP(cellStates As Dictionary(Of Cell, Boolean), changedCells As List(Of Cell))
        For Each cell In changedCells
            If cellStates(cell) Then
                SetBoth(ConsoleColor.White)
            Else
                SetBoth(ConsoleColor.Black)
            End If
            cell.Print("XX")
        Next
    End Sub

    Function countNeighboursOOP(cellStates As Dictionary(Of Cell, Boolean), currentCell As Cell, limits() As Integer) As Integer
        Dim neighbours As New List(Of Cell) From {
            New Cell(If(currentCell.X + 2 > limits(2) - 1, limits(0) + 3, currentCell.X + 2), currentCell.Y), 'right
            New Cell(If(currentCell.X - 2 < limits(0) + 3, limits(2) - 1, currentCell.X - 2), currentCell.Y), 'left
            New Cell(currentCell.X, If(currentCell.Y + 1 > limits(3), limits(1), currentCell.Y + 1)), 'down
            New Cell(currentCell.X, If(currentCell.Y - 1 < limits(1), limits(3), currentCell.Y - 1)), 'up
            New Cell(If(currentCell.X + 2 > limits(2) - 1, limits(0) + 3, currentCell.X + 2), If(currentCell.Y - 1 < limits(1), limits(3), currentCell.Y - 1)), 'right + up
            New Cell(If(currentCell.X - 2 < limits(0) + 3, limits(2) - 1, currentCell.X - 2), If(currentCell.Y - 1 < limits(1), limits(3), currentCell.Y - 1)), 'left + up
            New Cell(If(currentCell.X + 2 > limits(2) - 1, limits(0) + 3, currentCell.X + 2), If(currentCell.Y + 1 > limits(3), limits(1), currentCell.Y + 1)), 'right + down
            New Cell(If(currentCell.X - 2 < limits(0) + 3, limits(2) - 1, currentCell.X - 2), If(currentCell.Y + 1 > limits(3), limits(1), currentCell.Y + 1)) 'left + down
            }
        Dim neighbourCount = 0
        For Each cell In neighbours
            If cellStates(cell) Then neighbourCount += 1
        Next
        Return neighbourCount
    End Function

    Sub computeChangesOOP(ByRef nextState As Dictionary(Of Cell, Boolean), limits() As Integer, ByRef changedCells As List(Of Cell))
        changedCells.Clear()
        Dim duplicate As Dictionary(Of Cell, Boolean) = nextState
        Dim newState As New Dictionary(Of Cell, Boolean)
        For Each thing In duplicate
            Dim neighbours = countNeighboursOOP(duplicate, thing.Key, limits)
            If thing.Value = False And neighbours = 3 Then
                newState.Add(thing.Key, True)
            ElseIf thing.Value = True And (neighbours < 2 Or neighbours > 3) Then
                newState.Add(thing.Key, False)
            Else
                newState.Add(thing.Key, thing.Value)
            End If
        Next
        For Each thing In newState
            nextState(thing.Key) = thing.Value
            changedCells.Add(thing.Key)
        Next
    End Sub
End Module
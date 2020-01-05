Module Bogobogo_Sort
    Sub BogobogoSort(a As List(Of Double), delay As Integer)
        Dim index = 2
        While Not IsSorted(a)
            If ExitCase() Then Exit Sub
            BogoSort(a.GetRange(0, index - 1), delay)
            index += 1
            AnimateSort(a, delay)
            If Not IsSorted(a.GetRange(0, index - 1)) Then
                Shuffle(a)
                index = 2
            End If
        End While
        AnimateSort(a)
    End Sub
End Module

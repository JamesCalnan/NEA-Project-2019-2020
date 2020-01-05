Module Counting_Sort
    Sub countingSort(a As List(Of Double), delay As Integer)
        Dim min = a.Min()
        Dim max = a.Max()
        Dim z = 0
        Dim count(a.Count) As Integer
        For i = min To max
            count(i) = 0
        Next
        For i = 0 To a.Count - 1
            count(a(i)) += 1
        Next
        For i = min To max
            While count(i) > 0
                If ExitCase() Then Exit Sub
                AnimateSort(a, delay)
                a(z) = i
                z += 1
                count(i) -= 1
            End While
        Next
        AnimateSort(a)
    End Sub
End Module
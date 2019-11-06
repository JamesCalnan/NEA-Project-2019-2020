Module SelectionSortAlgorithm

    Function SelectionSort(a As List(Of Double), Optional delay As Integer = 0)
        Dim count As Integer = a.Count
        Dim i, j As Integer
        Dim minimum As Integer
        For i = 0 To count - 1
            If ExitCase() Then Exit Function
            minimum = i
            For j = i + 1 To count - 1
                If a(minimum) > a(j) Then minimum = j
            Next
            Swap(a, i, minimum)
            AnimateSort(a, delay)
        Next
        Return a
    End Function

End Module

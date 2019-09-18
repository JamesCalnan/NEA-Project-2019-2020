Module Selection_Sort_Algorithm

    Function selectionSort(ByVal A As List(Of Double))
        Dim count As Integer = A.Count
        Dim i, j As Integer
        Dim minimum As Integer
        For i = 0 To count - 1
            minimum = i
            For j = i + 1 To count - 1
                If A(minimum) > A(j) Then minimum = j
            Next
            swap(A, i, minimum)
            AnimateSort(A, i)
        Next
        Return A
    End Function

End Module

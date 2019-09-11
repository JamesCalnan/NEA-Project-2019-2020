Module Bogosort_Algorithm
    Function BogoSort(ByRef A As List(Of Double))
        Dim r As New Random
        While Not IsSorted(A)
            Shuffle(A) : End While
        Return A
    End Function
    Function IsSorted(ByVal list As List(Of Double))
        For i = 0 To list.Count - 2
            If list(i) > list(i + 1) Then Return False
        Next
        Return True
    End Function
End Module
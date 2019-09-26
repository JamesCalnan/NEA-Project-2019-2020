Module BogosortAlgorithm
    Function BogoSort(ByRef a As List(Of Double))
        Dim r As New Random
        While Not IsSorted(a)
            Shuffle(a) : End While
        Return a
    End Function
    Function IsSorted(list As List(Of Double))
        For i = 0 To list.Count - 2
            If list(i) > list(i + 1) Then Return False
        Next
        Return True
    End Function
End Module
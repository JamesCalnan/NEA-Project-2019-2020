Module BogosortAlgorithm
    Function BogoSort(ByRef a As List(Of Double), Optional delay As Integer = 0)
        Dim r As New Random
        While Not IsSorted(a)
            If ExitCase() Then Exit Function
            Shuffle(a)
            AnimateSort(a, delay)
        End While
        Return a
    End Function
    Function IsSorted(list As List(Of Double))
        For i = 0 To list.Count - 2
            If list(i) > list(i + 1) Then Return False
        Next
        Return True
    End Function
End Module
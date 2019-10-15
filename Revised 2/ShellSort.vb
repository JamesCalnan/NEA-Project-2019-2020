Module ShellSort
    Sub ShellSort(gaps As List(Of Double), Optional delay As Integer = 0)
        Dim a = gaps
        For k = 0 To gaps.Count - 1
            If IsSorted(a) Then Exit For
            For i = gaps(k) To gaps.Count - 1
                If ExitCase() Then Exit Sub
                Dim temp = a(i)
                Dim j = i
                While j > 0 AndAlso a(j - 1) > a(j)
                    Swap(a, j, j - 1)
                    j -= 1
                End While
                a(j) = temp
                AnimateSort(a, delay)
            Next
        Next
    End Sub
End Module

Module ShellSort
    Sub ShellSort(arr As List(Of Double), delay As Integer)
        Dim n = arr.Count - 1
        Dim gap = n \ 2
        While gap > 0
            If ExitCase() Then Exit Sub
            For i = gap To n
                Dim temp = arr(i)
                Dim j = i
                While j >= gap AndAlso arr(j - gap) > temp
                    AnimateSort(arr, delay)
                    arr(j) = arr(j - gap)
                    j -= gap
                End While
                arr(j) = temp
            Next
            gap \= 2
            AnimateSort(arr, delay)
        End While
        AnimateSort(arr, delay)
    End Sub
End Module

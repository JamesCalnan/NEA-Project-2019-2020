Module Cycle_Sort
    Sub cycleSort(arr As List(Of Double), delay As Integer)
        Dim n = arr.Count - 1
        For cycle_start = 0 To n - 2 Step 1
            If ExitCase() Then Exit Sub
            Dim item = arr(cycle_start)
            Dim pos = cycle_start
            For i = cycle_start + 1 To n
                If arr(i) < item Then pos += 1
            Next
            If pos = cycle_start Then Continue For
            While item = arr(pos)
                pos += 1
            End While
            If pos <> cycle_start Then
                Dim temp = item
                item = arr(pos)
                arr(pos) = temp
                AnimateSort(arr, delay)
            End If
            While pos <> cycle_start
                pos = cycle_start
                For i = cycle_start + 1 To n
                    If arr(i) < item Then pos += 1
                Next
                If item <> arr(pos) Then
                    Dim temp = item
                    item = arr(pos)
                    arr(pos) = temp
                    AnimateSort(arr, delay)
                End If
            End While
            AnimateSort(arr, delay)
        Next
    End Sub
End Module

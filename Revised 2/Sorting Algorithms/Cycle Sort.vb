Module Cycle_Sort
    Sub cycleSort(a As List(Of Double), delay As Integer)
        Dim n = a.Count - 1
        For cycle = 0 To n - 2 Step 1
            If ExitCase() Then Exit Sub
            Dim item = a(cycle)
            Dim pos = cycle
            For i = cycle + 1 To n
                If a(i) < item Then pos += 1
            Next
            If pos = cycle Then Continue For
            While item = a(pos)
                pos += 1
            End While
            If pos <> cycle Then
                Dim temp = item
                item = a(pos)
                a(pos) = temp
                AnimateSort(a, delay)
            End If
            While pos <> cycle
                pos = cycle
                For i = cycle + 1 To n
                    If a(i) < item Then pos += 1
                Next
                If item <> a(pos) Then
                    Dim temp = item
                    item = a(pos)
                    a(pos) = temp
                    AnimateSort(a, delay)
                End If
            End While
            AnimateSort(a, delay)
        Next
    End Sub
End Module

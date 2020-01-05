Module HeapSort
    Sub heapSort(ByRef a As List(Of Double), delay As Integer)
        Dim n = a.Count
        For i = n To 0 Step -1
            heapify(a, n, i, delay)
        Next
        For i = n - 1 To 0 Step -1
            Swap(a, 0, i)
            heapify(a, i, 0, delay)
        Next
        AnimateSort(a)
    End Sub
    Sub heapify(a As List(Of Double), n As Integer, i As Integer, delay As Integer)
        Dim max = i
        Dim l = 2 * i + 1
        Dim r = 2 * i + 2
        If l < n AndAlso a(l) > a(max) Then max = l
        If r < n AndAlso a(r) > a(max) Then max = r
        If max <> i Then
            Swap(a, i, max)
            AnimateSort(a, delay)
            heapify(a, n, max, delay)
        End If
    End Sub
End Module

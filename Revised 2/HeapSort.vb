Module HeapSort
    Sub heapSort(ByRef arr As List(Of Double), delay As Integer)
        Dim n = arr.Count
        For i = n To 0 Step -1
            heapify(arr, n, i, delay)
        Next
        For i = n - 1 To 0 Step -1
            Swap(arr, 0, i)
            heapify(arr, i, 0, delay)
        Next
        AnimateSort(arr)
    End Sub
    Sub heapify(arr As List(Of Double), n As Integer, i As Integer, delay As Integer)
        Dim largest = i
        Dim l = 2 * i + 1
        Dim r = 2 * i + 2
        If l < n AndAlso arr(l) > arr(largest) Then largest = l
        If r < n AndAlso arr(r) > arr(largest) Then largest = r
        If largest <> i Then
            Swap(arr, i, largest)
            AnimateSort(arr, delay)
            heapify(arr, n, largest, delay)
        End If
    End Sub
End Module
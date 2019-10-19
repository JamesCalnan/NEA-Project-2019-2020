Module Bucket_Sort

    Sub BucketSort(a As List(Of Double), delay As Integer)
        Dim max = a.Max()
        Dim buckets As New List(Of List(Of Double))
        For i = 0 To a.Count
            buckets.Add(New List(Of Double))
        Next
        For i = 0 To a.Count - 1
            Dim index As Integer = (a(i) * a.Count - 1) / (max + 1)
            buckets(index).Add(a(i))
        Next
        For Each bucket In buckets
            InsertionsortI(bucket, False)
        Next
        Dim newList(a.Count) As Double
        newList = a.ToArray()
        Dim j = 0
        For Each item In From bucket In buckets From item1 In bucket Select item1
            AnimateSort(newList.ToList(), delay)
            newList(j) = item
            j += 1
        Next
        AnimateSort(newList.ToList(), delay)
    End Sub

End Module
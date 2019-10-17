Module MergeSort
    Function Mergesort(m As List(Of Double), totalLength As Integer, Optional delay As Integer = 0)
        If m.Count <= 1 Then Return m
        Dim left As List(Of Double) = m.GetRange(0, m.Count \ 2)
        Dim right As List(Of Double) = m.GetRange(m.Count \ 2, m.Count - m.Count \ 2)
        AnimateSort(left, delay, 0, totalLength)
        AnimateSort(right, delay, 1, totalLength)
        left = Mergesort(left, totalLength, delay)
        right = Mergesort(right, totalLength, delay)
        Return Merge(left, right, delay, totalLength)
    End Function
    Function Merge(left As List(Of Double), right As List(Of Double), delay As Integer, totalLength As Integer)
        Dim result As New List(Of Double)
        AnimateSort(left, delay, 0, totalLength)
        AnimateSort(right, delay, 1, totalLength)
        While left.Count > 0 And right.Count > 0
            If left.First <= right.First Then
                result.Add(left.First)
                left.Remove(left.First)
            Else
                result.Add(right.First)
                right.Remove(right.First)
            End If
        End While
        While left.Count > 0
            result.Add(left.First)
            left.Remove(left.First)
        End While
        While right.Count > 0
            result.Add(right.First)
            right.Remove(right.First)
        End While
        Return result
    End Function
End Module
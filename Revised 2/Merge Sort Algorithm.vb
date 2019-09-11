Module Merge_Sort
    Function mergesort(ByVal m As List(Of Double))
        AnimateSort(m)
        If m.Count <= 1 Then Return m
        Dim left As List(Of Double) = m.GetRange(0, m.Count \ 2)
        Dim right As List(Of Double) = m.GetRange(m.Count \ 2, m.Count - m.Count \ 2)
        left = mergesort(left)
        right = mergesort(right)
        AnimateSort(m)
        Return merge(left, right)
    End Function
    Function merge(ByVal left As List(Of Double), ByVal right As List(Of Double))
        Dim result As New List(Of Double)
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
        AnimateSort(result)
        Return result
    End Function
End Module

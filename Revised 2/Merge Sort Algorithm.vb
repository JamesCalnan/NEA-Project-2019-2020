﻿Module MergeSort
    Function Mergesort(m As List(Of Double), Optional delay As Integer = 0)
        If m.Count <= 1 Then Return m
        Dim left As List(Of Double) = m.GetRange(0, m.Count \ 2)
        Dim right As List(Of Double) = m.GetRange(m.Count \ 2, m.Count - m.Count \ 2)
        AnimateSort(m)
        left = Mergesort(left)
        right = Mergesort(right)
        AnimateSort(m)
        Return Merge(left, right)
    End Function
    Function Merge(left As List(Of Double), right As List(Of Double))
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
        Return result
        AnimateSort(result)
    End Function
End Module

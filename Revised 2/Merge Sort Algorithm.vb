Module MergeSort
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


    Sub mergeSortIterative(arr As List(Of Double))
        Dim current_size = 1
        While current_size < arr.Count - 1
            Dim left = 0
            While left < arr.Count - 1
                Dim mid = left + current_size - 1

                Dim right = If(2 * current_size + left - 1 > arr.Count - 1, arr.Count - 1, 2 * current_size + left - 1)
                mergeIterative(arr, left, mid, right)
                left = left + current_size * 2
            End While
        End While
        current_size = 2 * current_size
    End Sub

    Sub mergeIterative(a As List(Of Double), l As Integer, m As Integer, r As Integer)
        Dim n1 = m - l + 1
        Dim n2 = r - m
        Dim left(n1) As Double
        Dim right(n2) As Double
        For x = 0 To n1
            left(x) = (a(l + x))
        Next

        For x = 0 To n2
            right(x) = (a(m + x + 1))
        Next

        Dim i = 0
        Dim j = 0
        Dim k = 0

        While i < n1 And j < n2
            If Left(i) > Right(j) Then
                a(k) = Right(j)
                j += 1
            Else
                a(k) = Left(i)
                i += 1
            End If
            k += 1

            While i < n1
                a(k) = Left(i)
                i += 1
                k += 1
            End While
            While j < n2
                a(k) = Right(j)
                j += 1
                k += 1
            End While
        End While
    End Sub


End Module

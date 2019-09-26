Module HeapSortAlgorithm
    Function Heapsort(a As List(Of Double), count As Integer)
        Dim endindex As Integer = count - 1
        While endindex > 0
            Swap(a, endindex, 0)
            endindex -= 1
            SiftDown(a, 0, endindex)
        End While
        Return a
    End Function
    Function Heapify(ByRef a As List(Of Double), count As Integer)
        Dim start As Integer = IParent(count - 1)
        While start >= 0
            SiftDown(a, start, count - 1)
            start -= 1
        End While
    End Function
    Function SiftDown(ByRef a As List(Of Double), start As Integer, endindex As Integer)
        Dim root As Integer = start

        While ILeftChild(root) <= endindex
            Dim child As Integer = ILeftChild(root)
            Dim swap As Integer = root
            If a(swap) < a(child) Then
                swap = child
            End If
            If child + 1 <= endindex And a(swap) < a(child + 1) Then
                swap = child + 1
            End If
            If swap = root Then
                Return a
            Else
                Dim temp As Double = a(root)
                a(root) = a(swap)
                a(swap) = temp

                root = swap
            End If
        End While
    End Function
    Function ILeftChild(i As Integer) As Integer
        Return 2 * i + 1
    End Function
    Function IRightChild(i As Integer) As Integer
        Return 2 * i + 2
    End Function
    Function IParent(count As Integer) As Integer
        Return Math.Floor((count - 1) / 2)
    End Function
    Sub Swap(ByRef a As List(Of Double), index1 As Integer, index2 As Integer)
        Dim temp As Double = a(index1)
        a(index1) = a(index2)
        a(index2) = temp
    End Sub
End Module

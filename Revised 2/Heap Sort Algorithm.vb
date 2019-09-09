Module Heap_Sort_Algorithm
    Function heapsort(ByVal a As List(Of Double), ByVal count As Integer)
        Dim endindex As Integer = count - 1
        While endindex > 0
            swap(a, endindex, 0)
            endindex -= 1
            siftDown(a, 0, endindex)
        End While
        Return a
    End Function
    Function heapify(ByRef a As List(Of Double), ByVal count As Integer)
        Dim start As Integer = iParent(count - 1)
        While start >= 0
            siftDown(a, start, count - 1)
            start -= 1
        End While
    End Function
    Function siftDown(ByRef a As List(Of Double), ByVal start As Integer, ByVal endindex As Integer)
        Dim root As Integer = start

        While iLeftChild(root) <= endindex
            Dim child As Integer = iLeftChild(root)
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
    Function iLeftChild(ByVal i As Integer) As Integer
        Return 2 * i + 1
    End Function
    Function iRightChild(ByVal i As Integer) As Integer
        Return 2 * i + 2
    End Function
    Function iParent(ByVal count As Integer) As Integer
        Return Math.Floor((count - 1) / 2)
    End Function
    Sub swap(ByRef a As List(Of Double), ByVal index1 As Integer, ByVal index2 As Integer)
        Dim temp As Double = a(index1)
        a(index1) = a(index2)
        a(index2) = temp
    End Sub
End Module

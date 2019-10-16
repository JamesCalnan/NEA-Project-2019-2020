Module BubbleSortAlgorithm
    Function BubbleSort(a As List(Of Double))
        Dim n As Integer = a.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    swapped = True
                End If
            Next
            AnimateSort(a, 1)
        Loop Until Not swapped
        Return a
    End Function
    Function BubbleSortOptimisedAlternate(a As List(Of Double), delay As Integer)
        Dim n As Integer = a.Count
        Do
            Dim newn = 0
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    newn = i
                    AnimateSort(a, delay)
                End If
            Next
            n = newn
        Loop Until n <= 1
        Return a
    End Function
    Function BubbleSortOptimised(a As List(Of Double))
        Dim n As Integer = a.Count
        Dim swapped As Boolean
        Do
            swapped = False
            For i = 1 To n - 1
                If a(i - 1) > a(i) Then
                    Swap(a, i - 1, i)
                    swapped = True
                End If
            Next
            n -= 1
        Loop Until Not swapped
        Return a
    End Function

    Sub bubbleSortRecursive(arr As List(Of Double), delay As Integer)
        If arr.Count = 1 Then Return
        For i = 1 To arr.Count - 1
            If arr(i - 1) > arr(i) Then
                Swap(arr, i - 1, i)
            End If
        Next
        AnimateSort(arr, 10)
        bubbleSortRecursive(arr.GetRange(0, arr.Count - 1), delay)
    End Sub

End Module

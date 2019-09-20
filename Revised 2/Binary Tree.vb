




Public Class Binary_Tree
    Public root As TreeItem

    Public Sub add(ByVal value As Integer)
        root = addRecursive(root, value)
    End Sub
    Private Function addRecursive(ByVal current As TreeItem, ByVal value As Integer)
        If IsNothing(current) Then
            Return New TreeItem(value)
        End If
        If value <= current.value Then
            current.left = addRecursive(current.left, value)
        ElseIf value >= current.value Then
            current.right = addRecursive(current.right, value)
        Else
            Return current
        End If
        Return current
    End Function
    Public Sub traverseInOrder(ByVal node As TreeItem)
        If Not IsNothing(node) Then
            traverseInOrder(node.left)
            Console.WriteLine(node.value)
            traverseInOrder(node.right)
        End If
    End Sub
    Private Function containsValueRecursive(ByVal current As TreeItem, ByVal value As Integer) As Boolean
        If IsNothing(current) Then Return False
        If value = current.value Then Return True
        Return If(value < current.value, containsValueRecursive(current.left, value), containsValueRecursive(current.right, value))
    End Function
    Public Function containsValue(ByVal value As Integer)
        Return containsValueRecursive(root, value)
    End Function

    Private Function deleteRecursive(ByVal current As TreeItem, ByVal value As Integer)
        If IsNothing(current) Then
            Return Nothing
        End If
        If value = current.value Then
            If IsNothing(current.left) And IsNothing(current.right) Then
                Return Nothing
            End If
            If IsNothing(current.right) Then
                Return current.left
            End If
            If IsNothing(current.left) Then
                Return current.right
            End If
            Dim smallestValue = findSmallestValue(current.right)
            current.value = smallestValue
            current.right = deleteRecursive(current.right, smallestValue)
            Return current
        End If
        If value < current.value Then
            current.left = deleteRecursive(current.left, value)
            Return current
        End If
        current.right = deleteRecursive(current.right, value)
        Return current
    End Function
    Public Sub delete(ByVal value As Integer)
        root = deleteRecursive(root, value)
    End Sub
    Private Function findSmallestValue(ByVal root As TreeItem)
        Return If(IsNothing(root.left), root.value, findSmallestValue(root.left))
    End Function
    Public Function ExtractMin()
        Return findSmallestValue(root)
    End Function
    Public Function getSize() As Integer
        Return getSizeRecursive(root)
    End Function
    Private Function getSizeRecursive(ByVal current As TreeItem)
        Return If(IsNothing(current), 0, getSizeRecursive(current.left) + 1 + getSizeRecursive(current.right))
    End Function
    Public Function isEmpty() As Boolean
        Return IsNothing(root)
    End Function
End Class
Public Class TreeItem
    Public value As Integer
    Public left As TreeItem
    Public right As TreeItem
    Public Sub New(ByVal val As Integer)
        value = val
        left = Nothing
        right = Nothing
    End Sub
End Class
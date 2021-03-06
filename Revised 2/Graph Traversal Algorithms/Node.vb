﻿Public Class Node
    Inherits Cell
    Public GCost, HCost As Integer
    Public Parent As Node
    Public Function ToCell()
        Return New Cell(Me.X, Me.Y)
    End Function
    Public Function AdjacentToMaze(maze As List(Of Node))
        If maze.Contains(New Node(Me.X, Me.Y + 1)) Or maze.Contains(New Node(Me.X, Me.Y - 1)) Or maze.Contains(New Node(Me.X - 2, Me.Y)) Or maze.Contains(New Node(Me.X + 2, Me.Y)) Then Return True
        Return False
    End Function
    Public Sub New(xPoint As Integer, yPoint As Integer)
        MyBase.New(xPoint, yPoint)
    End Sub
    Function IsDeadEnd(availablePath As List(Of Node))
        Dim neighbours As List(Of Node) = GetNeighbours(New Node(Me.X, Me.Y), availablePath)
        If neighbours.Count = 1 Then Return True
        Return False
    End Function
    Function IsJunction(availablePath As List(Of Node))
        Dim neighbours As List(Of Node) = GetNeighbours(New Node(Me.X, Me.Y), availablePath)
        If neighbours.Count >= 3 Then Return True
        Return False
    End Function
    Public Function FCost()
        Return GCost + HCost
    End Function
    Public Overrides Function Equals(obj As Object) As Boolean
        Dim node = TryCast(obj, Node)
        Return node IsNot Nothing AndAlso
               X = node.X AndAlso
               Y = node.Y
    End Function
    Public Overrides Function GetHashCode() As Integer
        Dim hashCode As Long = 1855483287
        hashCode = (hashCode * -1521134295 + X.GetHashCode()).GetHashCode()
        hashCode = (hashCode * -1521134295 + Y.GetHashCode()).GetHashCode()
        Return hashCode
    End Function
End Class

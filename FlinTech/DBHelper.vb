Imports System.Configuration
Imports System.Data.SqlClient

Public Class GenDBHelper
    Public Function FillDataTable(queryString As String) As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlConnection(GlobalVariables.con)
            Using da As New SqlDataAdapter(queryString, GlobalVariables.con)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Public Function FillDataTableWithParam(cmdTxt As String, params As Dictionary(Of String, Object)) As DataTable
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter
        Using con As New SqlConnection(GlobalVariables.con)
            Using cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = cmdTxt
                AddParameters(cmd, params)
                con.Open()
                da = New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using
        da.Dispose()
        Return dt
    End Function

    Public Sub ExecuteNonQuery(cmdTxt As String, params As Dictionary(Of String, Object))
        Using con As New SqlConnection(GlobalVariables.con)
            Using cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = cmdTxt
                AddParameters(cmd, params)
                con.Open()
                cmd.ExecuteNonQuery()

            End Using
        End Using
    End Sub

    Private Sub AddParameters(ByRef cmd As SqlCommand, params As Dictionary(Of String, Object))
        If Not params Is Nothing Then
            For Each kvp As KeyValuePair(Of String, Object) In params
                cmd.Parameters.AddWithValue(kvp.Key, kvp.Value)
            Next
        End If
    End Sub
End Class

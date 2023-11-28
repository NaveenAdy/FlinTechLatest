
Imports System.Configuration
Imports System.Data.SqlClient
Public Class frmALLTask
    Dim GenDBHelperObj As New GenDBHelper
    Private Sub frmALLTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblUserId.Text = GlobalVariables.userId
        lblLoggedUser.Text = GlobalVariables.userName
        Login()
    End Sub
    Public Sub Login()
        Dim dt As New DataTable()
        Dim data As New Dictionary(Of String, Object)
        data.Add("@UserId", GlobalVariables.userId)
        Try
            dt = GenDBHelperObj.FillDataTableWithParam("GetLoggedUserTask", data)
            dgvTaskDetails.DataSource = dt
            dgvTaskDetails.Columns(1).Width = 400
            dgvTaskDetails.Columns(2).Width = 300
            dgvTaskDetails.Columns(3).Width = 1
            Dim rowCount As Integer = dgvTaskDetails.Rows.Count - 1
            dgvTaskDetails.MultiSelect = False
            dgvTaskDetails.Rows(rowCount).Selected = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub dgvTaskDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTaskDetails.CellClick
        If (e.RowIndex > -1 And (dgvTaskDetails.CurrentCell.Value.ToString() <> "")) Then
            btnStartinTask.Enabled = True
            btnStartinTask.BackColor = Color.LightBlue
            GlobalVariables.selectedTaskId = dgvTaskDetails.CurrentRow.Cells(3).Value.ToString()
            GlobalVariables.selectedTaskName = dgvTaskDetails.CurrentRow.Cells(1).Value.ToString()
        Else
            GlobalVariables.selectedTaskId = ""
            GlobalVariables.selectedTaskName = ""
            btnStartinTask.Enabled = False
            btnStartinTask.BackColor = Color.White
        End If
    End Sub

    Private Sub btnStartinTask_Click(sender As Object, e As EventArgs) Handles btnStartinTask.Click
        If (btnStartinTask.Enabled = False) Then
            MessageBox.Show("Select a Task from the Grid")
        End If
        Dim formProcessTask As New frmProcessTask
        Me.Hide()
        formProcessTask.Show()
    End Sub

    Private Sub frmALLTask_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
        Me.Close()
        Application.Exit()
    End Sub
End Class
Imports System.Timers

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False

        NotifyIcon1.Visible = True
        NotifyIcon1.Text = Application.ProductName

        atualizarDados()

        InitializeTimer()

        Label1.Text = DateTime.Now


    End Sub

    Public Sub Export()
        Dim headers = (From header As DataGridViewColumn In Dg_consultar.Columns.Cast(Of DataGridViewColumn)()
                       Select header.HeaderText).ToArray
        Dim rows = From row As DataGridViewRow In Dg_consultar.Rows.Cast(Of DataGridViewRow)()
                   Where Not row.IsNewRow
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
        Using sw As New IO.StreamWriter("dados.csv")
            sw.WriteLine(String.Join(",", headers))
            For Each r In rows
                sw.WriteLine(String.Join(",", r))
            Next
        End Using
        ' Process.Start("csv.txt")

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True

    End Sub

    Private Sub atualizarDados()
        Dim objAcesso = New Acesso()
        Dim dataset As DataSet
        Dim data_inicio As String

        data_inicio = Format(Now, "yyyy-MM-dd")

        objAcesso.conectar()

        dataset = objAcesso.query(data_inicio, data_inicio)

        Dg_consultar.DataSource = dataset.Tables(0)

        objAcesso.desconectar()

        Export()
    End Sub

    Private Sub InitializeTimer()
        ' Run this procedure in an appropriate event.
        ' Set to 1 second.

        Timer1.Interval = 1000 * 120 '2 min
        ' Enable timer.
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal Sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        ' Set the caption to the current time.
        Label1.Text = DateTime.Now
        atualizarDados()
    End Sub

    Private Sub MinimizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimizarToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
    End Sub

End Class

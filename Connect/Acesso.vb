Public Class Acesso
    Private ObjConexao As SqlClient.SqlConnection

    Public Sub conectar()
        Try
            ObjConexao = New SqlClient.SqlConnection("Data Source = 10.221.230.72;database = db_BrasilCap_Neoflow_PDB;Integrated Security=SSPI")

            ObjConexao.Open()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub desconectar()
        Try
            If IsNothing(ObjConexao) Then
                If ObjConexao.State = ConnectionState.Open Then
                    ObjConexao.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function query(ByVal data_inicio As String, ByVal data_fim As String) As DataSet
        Dim dataset As New DataSet
        Dim dataAdapter As New SqlClient.SqlDataAdapter
        Dim sqlCommand As New SqlClient.SqlCommand

        Try
            sqlCommand = ObjConexao.CreateCommand
            sqlCommand.CommandText = "USP_LISTA_DADOS_PLANEJAMENTO '" + data_inicio + " 09:00:00','" + data_fim + " 21:59:59'"

            dataAdapter = New SqlClient.SqlDataAdapter(sqlCommand)
            dataAdapter.Fill(dataset)

        Catch ex As Exception
            Throw ex
        End Try

        Return dataset
    End Function


End Class

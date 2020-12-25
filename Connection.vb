Imports MongoDB.Driver

Public Class Connection
    Private _host As String = "localhost"
    Private _port As Integer = 27017
    Private _connected As Boolean = False


    ''' <summary>
    ''' Nama Host
    ''' </summary>
    Public Property Host() As String
        Get
            Return _host
        End Get
        Set(ByVal value As String)
            _host = value
        End Set
    End Property

    ''' <summary>
    ''' Username Oracle Express
    ''' </summary>
    Public Property Port() As String
        Get
            Return _port
        End Get
        Set(ByVal value As String)
            _port = value
        End Set
    End Property



    ''' <summary>
    ''' Proses menyambungkan koneksi (SQL Server Express with Integrated Security = True without Username or Password)
    ''' </summary>
    Public Sub Connect()
        Try
            dbClient = "mongodb://" & _host & ":" & _port & "/?readPreference=primary&appname=MongoDB%20Compass&ssl=false"

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            _connected = False
        Finally
            _connected = True

        End Try
    End Sub

    ''' <summary>
    ''' Proses memutuskan koneksi
    ''' </summary>
    Public Sub Disconnect()
        dbClient = vbNull
        _connected = False
    End Sub


End Class
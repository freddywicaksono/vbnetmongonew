Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class Fakultas
    Dim strsql As String
    Dim info As String

    Private _kode_fakultas As String
    Private _nama_fakultas As String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False

    Public Property kode_fakultas()
        Get
            Return _kode_fakultas
        End Get
        Set(ByVal value)
            _kode_fakultas = value
        End Set
    End Property

    Public Property nama_fakultas()
        Get
            Return _nama_fakultas
        End Get
        Set(ByVal value)
            _nama_fakultas = value
        End Set
    End Property

    Public Sub Simpan()
        oTable = db.GetCollection(Of BsonDocument)("fakultas")

        Dim doc = New BsonDocument From {
            {"kode_fakultas", _kode_fakultas},
            {"nama_fakultas", _nama_fakultas}
        }

        Try
            oTable.InsertOne(doc)
            InsertState = True
        Catch ex As MongoDB.Driver.MongoWriteException
            InsertState = False
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Sub Update()
        oTable = db.GetCollection(Of BsonDocument)("fakultas")

        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("kode_fakultas", _kode_fakultas)
        Dim update = Builders(Of BsonDocument).Update.Set(Of String)("kode_fakultas", _kode_fakultas)
        Dim update2 = Builders(Of BsonDocument).Update.Set(Of String)("nama_fakultas", _nama_fakultas)

        oTable.UpdateOne(filter, update)
        oTable.UpdateOne(filter, update2)
        UpdateState = True
    End Sub
    Public Sub CariFakultas(ByVal skode_fakultas As String)
        Dim s As String
        oTable = db.GetCollection(Of BsonDocument)("fakultas")

        'Query:
        '-------------------------------------------
        s = "{kode_fakultas:" & Chr(34) & skode_fakultas & Chr(34) & "}"
        '---------------------------------

        docs = oTable.Find(s).ToList()
            For Each item As MongoDB.Bson.BsonDocument In docs


            Dim kode As BsonElement = item.GetElement("kode_fakultas")
            Dim nama As BsonElement = item.GetElement("nama_fakultas")


                _kode_fakultas = kode.Value
                _nama_fakultas = nama.Value
            Next

            If (docs.Count = 0) Then
                MessageBox.Show("Data Tidak Ditemukan")
                fakultas_baru = True
            Else
                fakultas_baru = False

            End If


    End Sub


    Public Sub Hapus(ByVal skode_fakultas As String)
        Dim s As String
        oTable = db.GetCollection(Of BsonDocument)("fakultas")
        s = "{kode_fakultas:" & Chr(34) & skode_fakultas & Chr(34) & "}"
        Try
            oTable.DeleteOne(s)
            DeleteState = True
        Catch ex As Exception
            DeleteState = False
            'MessageBox.Show(ex.ToString)
        End Try



    End Sub

    Public Sub GetAllData(ByVal dg As DataGridView)
        Try
            oTable = db.GetCollection(Of BsonDocument)("fakultas")
            docs = oTable.Find(New BsonDocument()).ToList()

            Dim dt = New DataTable()

            dt.Columns.Add("kode_fakultas")
            dt.Columns.Add("nama_fakultas")

            For Each item As MongoDB.Bson.BsonDocument In docs
                Dim R As DataRow = dt.NewRow


                Dim kode As BsonElement = item.GetElement("kode_fakultas")
                Dim nama As BsonElement = item.GetElement("nama_fakultas")


                R("kode_fakultas") = kode.Value
                R("nama_fakultas") = nama.Value
                dt.Rows.Add(R)
            Next
            dg.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        End Try
    End Sub

End Class

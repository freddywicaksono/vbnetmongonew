Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reload()
    End Sub

    Private Sub Reload()
        DBConnect()
        oFakultas.GetAllData(DataGridView1)
    End Sub

    Private Sub ClearEntry()
        txtKode_Fakultas.Clear()
        txtNama_Fakultas.Clear()

        txtKode_Fakultas.Focus()
    End Sub

    Private Sub txtKode_Fakultas_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKode_Fakultas.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            oFakultas.CariFakultas(txtKode_Fakultas.Text)
            If (fakultas_baru = False) Then
                TampilFakultas()
            End If
        End If
    End Sub

    Private Sub TampilFakultas()
        txtKode_Fakultas.Text = oFakultas.kode_fakultas
        txtNama_Fakultas.Text = oFakultas.nama_fakultas
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim jawab As Integer

        If (txtKode_Fakultas.Text <> "") Then
            jawab = MessageBox.Show("Apakah data ini akan dihapus", "Konfirmasi", MessageBoxButtons.YesNo)
            If jawab = vbYes Then
                oFakultas.Hapus(txtKode_Fakultas.Text)
                If (oFakultas.DeleteState = True) Then
                    MessageBox.Show("Data telah dihapus")
                Else
                    MessageBox.Show("Data gagal dihapus")
                End If
                ClearEntry()
                Reload()
            Else
                MessageBox.Show("Data batal dihapus")
            End If

        Else
            MessageBox.Show("Kode tidak boleh kosong.")
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (txtKode_Fakultas.Text <> "" And txtNama_Fakultas.Text <> "") Then
            oFakultas.nama_fakultas = txtNama_Fakultas.Text
            oFakultas.kode_fakultas = txtKode_Fakultas.Text
            If (fakultas_baru = True) Then
                oFakultas.Simpan()
                If (oFakultas.InsertState = True) Then
                    MessageBox.Show("Data berhasil disimpan")
                Else
                    MessageBox.Show("Data gagal disimpan")
                End If
            Else
                oFakultas.Update()
                If (oFakultas.UpdateState = True) Then
                    MessageBox.Show("Data berhasil diperbarui")
                Else
                    MessageBox.Show("Data gagal diperbarui")
                End If
            End If

            ClearEntry()
            Reload()
        Else
            MessageBox.Show("Data tidak boleh kosong")
        End If

    End Sub
End Class

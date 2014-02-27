Public Class Form1

#Region "Local Variables"
    Private SelectPath As String
    Private RootPath As String = "c:\"
    Private FileType As ArrayList
#End Region

#Region "Events"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If System.IO.File.Exists(Application.StartupPath & "\RootPath.txt") = True Then
            Dim FR As FileWriterReader.FileReading
            FR = New FileWriterReader.FileReading
            RootPath = FR.FileRead(Application.StartupPath & "\RootPath.txt")
        Else
            mnuFiles1Root_Click(sender, e)
        End If

        FileType = New ArrayList
        FileType.Add("vb")
    End Sub

    Private Sub mnuFiles1Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFiles1Browse.Click
        Me.FolderBrowserDialog1.SelectedPath = RootPath

        If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.clbFiles.Items.Clear()
            SelectPath = Me.FolderBrowserDialog1.SelectedPath
            Dim Files As String() = System.IO.Directory.GetFiles(SelectPath)

            Me.clbFiles.Items.Add("T�m�")
            For Each itm As String In Files
                If Not itm Is Nothing Then
                    Dim FNR As FileNameReader.FileNameReader
                    FNR = New FileNameReader.FileNameReader(itm, FileType)
                    If Not FNR.strFileName Is Nothing Then
                        Me.clbFiles.Items.Add(FNR.strFileName)
                    End If
                End If
            Next

            If Me.clbFiles.Items.Count = 1 Then
                Me.clbFiles.Items.Clear()
                MessageBox.Show("Se�ti�iniz Klas�rde " & Chr(34) & ".vb" & Chr(34) & " uzant�l� dosya bulunamad�..")
                Me.tssFiles1Status.Text = ""
            Else
                Me.tssFiles1Status.Text = Me.clbFiles.Items.Count - 1 & " adet dosya vard�r.."
            End If
        End If
    End Sub

    Private Sub mnuFiles1Root_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFiles1Root.Click
        If Me.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim FW As FileWriterReader.FileCreating = New FileWriterReader.FileCreating
            RootPath = Me.FolderBrowserDialog1.SelectedPath
            FW.WritingCreateFile(Application.StartupPath & "\RootPath.txt", RootPath)
        End If
    End Sub

    Private Sub mnuFiles1Exit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFiles1Exit.Click
        If MessageBox.Show("Programdan ��kmak istiyor musunuz?", "�IKI� !", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub clbFiles_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbFiles.ItemCheck
        If e.Index = 0 Then
            If Me.clbFiles.GetItemCheckState(0) = CheckState.Unchecked Then
                For i As Integer = 1 To Me.clbFiles.Items.Count - 1
                    Me.clbFiles.SetItemChecked(i, True)
                Next
            Else
                For i As Integer = 1 To Me.clbFiles.Items.Count - 1
                    Me.clbFiles.SetItemChecked(i, False)
                Next
            End If
        End If
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        If Not Me.clbFiles.CheckedItems.Count = 0 Then
            For i As Integer = 0 To Me.clbFiles.CheckedItems.Count - 1
                Dim itmName As String = Me.clbFiles.CheckedItems.Item(i).ToString
                If Not itmName = "T�m�" Then
                    '=============
                    'RichTextBox
                    '=============
                    Dim rtb As RichTextBox = New RichTextBox
                    rtb.Dock = DockStyle.Fill
                    rtb.Name = itmName
                    rtb.Font = New System.Drawing.Font("Times New Roman", 12, Drawing.FontStyle.Bold)
                    '=============
                    'TabPage
                    '=============
                    Dim tb As TabPage = New TabPage
                    tb.Name = itmName
                    tb.Text = itmName
                    tb.Controls.Add(rtb)

                    Me.tbcFiles.TabPages.Add(tb)

                    Dim FR As FileWriterReader.FileReading
                    FR = New FileWriterReader.FileReading
                    rtb.Text = FR.FileRead(SelectPath & "\" & itmName)
                End If
            Next
            Me.tssFiles2Status.Text = Me.tbcFiles.TabPages.Count & " adet sayfa vard�r.."
        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Me.clbFiles.Items.Clear()
        Me.tbcFiles.TabPages.Clear()
        Me.tssFiles1Status.Text = ""
        Me.tssFiles2Status.Text = ""
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        If Not Me.tbcFiles.SelectedIndex = -1 Then
            Dim rtb As Control = New Control
            rtb = Me.tbcFiles.TabPages(Me.tbcFiles.SelectedIndex).Controls(0)
            CType(rtb, RichTextBox).Cut()
        End If
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        If Not Me.tbcFiles.SelectedIndex = -1 Then
            Dim rtb As Control = New Control
            rtb = Me.tbcFiles.TabPages(Me.tbcFiles.SelectedIndex).Controls(0)
            CType(rtb, RichTextBox).Copy()
        End If
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        If Not Me.tbcFiles.SelectedIndex = -1 Then
            Dim rtb As Control = New Control
            rtb = Me.tbcFiles.TabPages(Me.tbcFiles.SelectedIndex).Controls(0)
            CType(rtb, RichTextBox).Paste()
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Me.FolderBrowserDialog1.ShowDialog()
        Dim SavePath As String = Me.FolderBrowserDialog1.SelectedPath
        Dim rtb As Control = New Control

        If MessageBox.Show("Sadece se�ilen sayfan�n kaydedilmesini i�in " & Chr(34) & "Evet" & Chr(34) & " ,t�m sayfalar�n kaydedilmesi i�in " & Chr(34) & "Hay�r" & Chr(34) & " se�iniz..", "KATDET !", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then

            rtb = Me.tbcFiles.TabPages(Me.tbcFiles.SelectedIndex).Controls(0)
            CType(rtb, RichTextBox).SaveFile(SavePath & "\" & rtb.Name & ".txt", RichTextBoxStreamType.UnicodePlainText)
        Else
            For Each tbp As TabPage In Me.tbcFiles.TabPages
                rtb = tbp.Controls(0)
                CType(rtb, RichTextBox).SaveFile(SavePath & "\" & rtb.Name & ".txt", RichTextBoxStreamType.UnicodePlainText)
            Next
        End If
    End Sub

    Private Sub Di�erToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DigerToolStripMenuItem.Click
        Me.VbToolStripMenuItem.Checked = False
        Me.DigerToolStripMenuItem.Checked = True
        Dim Filetypes As String
        Dim Types() As String

        Filetypes = InputBox("Listelenmesini istedi�iniz dosya uzant�s�n�z giriniz.." & vbCrLf & "E�er birden �ok uzant� listelenmesini isterseniz aralar�na tire(-) koyarak yaz�n�z.." & vbCrLf & "�rne�in; vb-cs-txt ...", "Dosya Uzant�s� Belirt")

        Types = Filetypes.Split("-")
        FileType.Clear()
        For i As Integer = 0 To Types.Length - 1
            FileType.Add(Types(i).ToString)
        Next
    End Sub

    Private Sub VbToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VbToolStripMenuItem.Click
        Me.VbToolStripMenuItem.Checked = True
        Me.DigerToolStripMenuItem.Checked = False
        FileType.Clear()
        FileType.Add("vb")
    End Sub

    Private Sub cmnuClosingSelectPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuClosingSelectPage.Click
        Me.tbcFiles.SelectedTab.Dispose()
        Dim i As Integer = Me.tbcFiles.TabPages.Count
        If Not i = 0 Then
            Me.tssFiles2Status.Text = i & " adet sayfa vard�r.."
        Else
            Me.tssFiles2Status.Text = ""
        End If
    End Sub

    Private Sub cmnuClosingAllPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuClosingAllPage.Click
        For Each pg As TabPage In Me.tbcFiles.TabPages
            pg.Dispose()
        Next
        Me.tssFiles2Status.Text = ""
    End Sub

    Private Sub cmnuEmptyPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEmptyPage.Click
        Dim PageName As String = ""
        PageName = InputBox("L�tfen sayfa ismi giriniz..", "Sayfa �smi")
        If Not Trim(PageName) = "" Then
            PageName = Trim(PageName)
            'Creating RichTextBox..
            Dim rtb As RichTextBox = New RichTextBox
            rtb.Dock = DockStyle.Fill
            rtb.Name = PageName
            rtb.Font = New System.Drawing.Font("Times New Roman", 12, Drawing.FontStyle.Bold)
            'Creating TabPage..
            Dim tp As TabPage = New TabPage
            tp.Controls.Add(rtb)
            tp.Name = PageName
            tp.Text = PageName

            Me.tbcFiles.TabPages.Add(tp)
        End If
    End Sub
#End Region

End Class

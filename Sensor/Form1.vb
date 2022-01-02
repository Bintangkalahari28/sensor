Imports System
Imports System.IO.Ports
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form1
    Dim comPort As String
    Dim receiveData As String = ""
    Dim chartvisual As New Series
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If (ComboBox1.SelectedItem <> "") Then
            comPort = ComboBox1.SelectedItem
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lblketerangan.Text = ""
        Timer1.Enabled = False
        comPort = ""
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
        chartvis()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Connect") Then
            If (comPort <> "") Then
                SerialPort1.Close()
                SerialPort1.PortName = comPort
                SerialPort1.BaudRate = 9600
                SerialPort1.DataBits = 8
                SerialPort1.Parity = Parity.None
                SerialPort1.StopBits = StopBits.One
                SerialPort1.Handshake = Handshake.None
                SerialPort1.Encoding = System.Text.Encoding.Default
                SerialPort1.ReadTimeout = 1000
                SerialPort1.Open()
                Button1.Text = "Dis-Connect"
                Lblketerangan.Text = "Aktif"
                Timer1.Start()
            Else
                MsgBox("Pilih Port")
            End If
        Else
            SerialPort1.Close()
            Button1.Text = "Connect"
            Lblketerangan.Text = "Tidak Aktif"
            Timer1.Enabled = False
        End If
    End Sub

    Function receiveserialdata() As String
        Dim incoming As String
        Try
            incoming = SerialPort1.ReadExisting()
            If incoming Is Nothing Then
                Return "nothing" & vbCrLf
            Else
                Return incoming
            End If
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        receiveData = receiveserialdata()
        RichTextBox1.Text &= receiveData
        chartvisual.Points.AddY(receiveData)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        If Val(receiveData) >= 1023 Then
            TextBox1.Text = "Tinggi"
            TextBox1.BackColor = Color.Red
        ElseIf Val(receiveData) <= 1019 Then
            TextBox1.Text = "Sedang"
            TextBox1.BackColor = Color.Yellow
        Else
            TextBox1.Text = "Rendah"
            TextBox1.BackColor = Color.Green
        End If
    End Sub
    Sub chartvis()
        chartvisual.Name = "Kelembapan Terdeteksi"
        chartvisual.ChartType = SeriesChartType.Line
        chartvisual.BackSecondaryColor = Color.Red
        chartvisual.Points.AddY(receiveData)
        Chart1.Series.Add(chartvisual)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Timer1.Start()
    End Sub
End Class

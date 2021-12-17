Imports System
Imports System.IO.Ports
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form1
    Dim comPort As String
    Dim receiveData As String = ""
    Dim chartvisual As New Series
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = False
        comPort = ""
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (Button1.Text = "Connect") Then
            If (comPort <> "") Then
                SerialPort1.Close()
                SerialPort1.PortName = comPort
                SerialPort1.BaudRate = 9600
                SerialPort1.DataBits = 8
                SerialPort1.Parity = 
            End If
        End If
    End Sub
End Class

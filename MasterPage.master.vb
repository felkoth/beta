
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Public ReadOnly Property ConStr() As String
        Get
            'Return System.Configuration.ConfigurationManager.ConnectionStrings("cnMetrics").ToString
            Return "Data Source=" & Server.MapPath("Metrics.sdf")
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Notify(ByVal strValue As String)
        If strValue.Contains("Error") Or strValue.Contains("error") Then
            lblError.Text = strValue
            pnlError.CssClass = "errorPopup"
            MPE.Show()
        Else
            lblNotification.Text = strValue
            notification.CssClass = ""
        End If
    End Sub
End Class


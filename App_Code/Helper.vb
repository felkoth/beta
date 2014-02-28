Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Public Class Helper

    Shared strConStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("cnMetrics").ToString

    Shared Function GetAvgLocPerFunctionPoint(ByVal strLangauge As String) As Integer
        'Returns the average number of lines of code per function point for a specified language

        GetAvgLocPerFunctionPoint = 0

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()

            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = "SELECT LocPerFp FROM Languages WHERE Language='" & strLangauge & "'"

            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                GetAvgLocPerFunctionPoint = rdr.GetInt32(0)
            End While

            rdr.Close()
            cmd.Dispose()

        Finally
            conn.Close()
        End Try

        Return GetAvgLocPerFunctionPoint

    End Function

    Shared Function GetProjectClassFactors(ByVal strClass As String) As Collection
        'Returns the Project Class Factors (A,B,C,D) for the Effort Formula

        GetProjectClassFactors = New Collection

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()

            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = "SELECT * FROM ProjectClass WHERE Class='" & strClass & "'"

            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                GetProjectClassFactors.Add(rdr.GetValue(2), "FactorA")
                GetProjectClassFactors.Add(rdr.GetValue(3), "FactorB")
                GetProjectClassFactors.Add(rdr.GetValue(4), "FactorC")
                GetProjectClassFactors.Add(rdr.GetValue(5), "FactorD")
            End While

            rdr.Close()
            cmd.Dispose()

        Finally
            conn.Close()
        End Try

        Return GetProjectClassFactors

    End Function

    Shared Function GetEffortFactor(ByVal strFactorName As String, ByVal strLevelToGet As String) As Double
        'Returns the effort adjustment factor. FactorName is the name of the factor (Programmer Capability, etc) and LevelToGet is Low, Nominal, High, etc.

        GetEffortFactor = 1

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()

            'Remove spaces from strLevelToGet
            strLevelToGet = strLevelToGet.Replace(" ", "")

            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = String.Format("SELECT {0} FROM EffortFactors WHERE FactorName='{1}'", strLevelToGet, strFactorName)

            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                GetEffortFactor = rdr.GetValue(0)
            End While

            rdr.Close()
            cmd.Dispose()

        Finally
            conn.Close()
        End Try

        Return GetEffortFactor

    End Function

    Shared Function GetPreviousProjects() As DataSet
        ' Returns a dataset of all the previous projects saved to the database

        GetPreviousProjects = New DataSet

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()

            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = String.Format("SELECT * FROM Projects ORDER BY IDNo DESC")

            Dim adp As New SqlDataAdapter(cmd)
            adp.Fill(GetPreviousProjects)

            cmd.Dispose()

        Finally
            conn.Close()
        End Try

        Return GetPreviousProjects
    End Function

    Shared Function DeleteProject(ByVal intIDNo As Integer) As String
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()


            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = String.Format("DELETE FROM Projects WHERE IDNo = {0}", intIDNo)

            cmd.ExecuteScalar()

            cmd.Dispose()
        Catch e As Exception
            Return "SQL Error: " & e.ToString
        Finally
            conn.Close()
        End Try

        Return "Delete Successful."
    End Function


    Shared Function InsertProject(ByVal strProjectName As String, ByVal intFunctionPoints As Integer, ByVal strLanguage As String, ByVal strProjectClass As String, ByVal intProgCap As Integer,
                                  ByVal intLangExp As Integer, ByVal intSoftComplex As Integer, ByVal intDocumentation As Integer, ByVal intCostPerPM As Integer, ByVal intLOC As Integer,
                                  ByVal decEffort As Decimal, ByVal decSchedule As Decimal, ByVal intPeople As Integer, ByVal intCost As Integer, ByVal strStartDate As String, ByVal strEndDate As String) As String
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()


            Dim cmd As SqlCommand = conn.CreateCommand()

            Dim sb As New StringBuilder
            sb.AppendFormat("INSERT INTO Projects(ProjectName,FunctionPoints,Language,ProjectClass,ProgCap,LangExp,SoftComplex,Documentation,CostPerPM,LoC,Effort,Schedule,People,Cost,StartDate,EndDate) ")
            sb.AppendFormat("VALUES('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},'{14}','{15}')", strProjectName.Replace("'", "''"), intFunctionPoints, strLanguage, strProjectClass, intProgCap, intLangExp, intSoftComplex,
                            intDocumentation, intCostPerPM, intLOC, decEffort, decSchedule, intPeople, intCost, strStartDate, strEndDate)


            cmd.CommandText = sb.ToString

            cmd.ExecuteScalar()

            cmd.Dispose()
        Catch e As Exception
            Return "SQL Error: " & e.ToString
        Finally
            conn.Close()
        End Try

        Return "Insert Successful."
    End Function

    Shared Function GetSavedProjectData(ByVal intIDNo As Integer) As Collection
        'Returns a collection of all the project data for one project 

        GetSavedProjectData = New Collection

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConStr)
            conn.Open()

            Dim cmd As SqlCommand = conn.CreateCommand()
            cmd.CommandText = "SELECT * FROM Projects WHERE IDNo='" & intIDNo & "'"

            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                GetSavedProjectData.Add(rdr.GetValue(0), "IDNo")
                GetSavedProjectData.Add(rdr.GetValue(1), "ProjectName")
                GetSavedProjectData.Add(rdr.GetValue(2), "FunctionPoints")
                GetSavedProjectData.Add(rdr.GetValue(3), "Language")
                GetSavedProjectData.Add(rdr.GetValue(4), "ProjectClass")
                GetSavedProjectData.Add(rdr.GetValue(5), "ProgCap")
                GetSavedProjectData.Add(rdr.GetValue(6), "LangExp")
                GetSavedProjectData.Add(rdr.GetValue(7), "SoftComplex")
                GetSavedProjectData.Add(rdr.GetValue(8), "Documentation")
                GetSavedProjectData.Add(rdr.GetValue(9), "CostPerPM")
                GetSavedProjectData.Add(rdr.GetValue(10), "LoC")
                GetSavedProjectData.Add(rdr.GetValue(11), "Effort")
                GetSavedProjectData.Add(rdr.GetValue(12), "Schedule")
                GetSavedProjectData.Add(rdr.GetValue(13), "People")
                GetSavedProjectData.Add(rdr.GetValue(14), "Cost")
                ' JAG
                GetSavedProjectData.Add(rdr.GetValue(15), "StartDate")
                GetSavedProjectData.Add(rdr.GetValue(16), "EndDate")
            End While

            rdr.Close()
            cmd.Dispose()

        Finally
            conn.Close()
        End Try

        Return GetSavedProjectData

    End Function
End Class

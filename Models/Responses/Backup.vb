Imports System.Net

Namespace API.Models.Responses

    Public Class Backup
        Public Property BACKUPID As String
        Public Property date_created As String
        Public Property description As String
        Public Property size As String
        Public Property status As String
    End Class

    Public Structure BackupResult
        Public Property Backups As Dictionary(Of String, Backup)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class Schedule
        Public Property enabled As Boolean
        ''' <summary>
        ''' Backup cron type. Can be one of 'daily', 'weekly', 'monthly', 'daily_alt_even', or 'daily_alt_odd'.
        ''' </summary>
        ''' <returns></returns>
        Public Property cron_type As String
        Public Property next_scheduled_time_utc As String
        ''' <summary>
        ''' Hour value (0-23). Applicable to crons: 'daily', 'weekly', 'monthly', 'daily_alt_even', 'daily_alt_odd'
        ''' </summary>
        ''' <returns></returns>
        Public Property hour As Hour
        ''' <summary>
        ''' Day-of-week value (0-6). Applicable to crons: 'weekly'.
        ''' </summary>
        ''' <returns></returns>
        Public Property dow As DOW
        ''' <summary>
        ''' Day-of-month value (1-28). Applicable to crons: 'monthly'.
        ''' </summary>
        ''' <returns></returns>
        Public Property dom As DOM
    End Class

    Public Structure ScheduleResult
        Public Property Schedule As Schedule
        Public Property ApiResponse As HttpWebResponse
    End Structure
    ''' <summary>
    ''' Hour value (0-23). Applicable to crons: 'daily', 'weekly', 'monthly', 'daily_alt_even', 'daily_alt_odd'
    ''' </summary>
    Public Enum Hour
        HOUR00
        HOUR01
        HOUR02
        HOUR03
        HOUR04
        HOUR05
        HOUR06
        HOUR07
        HOUR08
        HOUR09
        HOUR10
        HOUR11
        HOUR12
        HOUR13
        HOUR14
        HOUR15
        HOUR16
        HOUR17
        HOUR18
        HOUR19
        HOUR20
        HOUR21
        HOUR22
        HOUR23
        HOUR24
    End Enum
    ''' <summary>
    ''' Day-of-week value (0-6). Applicable to crons: 'weekly'.
    ''' </summary>
    Public Enum DOW
        Sunday
        Monday
        Tuesday
        Wednesday
        Thursday
        Friday
        Saturday
    End Enum
    ''' <summary>
    ''' Day-of-month value (1-28). Applicable to crons: 'monthly'.
    ''' </summary>
    Public Enum DOM
        DAY01 = 1
        DAY02 = 2
        DAY03 = 3
        DAY04 = 4
        DAY05 = 5
        DAY06 = 6
        DAY07 = 7
        DAY08 = 8
        DAY09 = 9
        DAY10 = 10
        DAY11 = 11
        DAY12 = 12
        DAY13 = 13
        DAY14 = 14
        DAY15 = 15
        DAY16 = 16
        DAY17 = 17
        DAY18 = 18
        DAY19 = 19
        DAY20 = 20
        DAY21 = 21
        DAY22 = 22
        DAY23 = 23
        DAY24 = 24
        DAY25 = 25
        DAY26 = 26
        DAY27 = 27
        DAY28 = 28
    End Enum
End Namespace
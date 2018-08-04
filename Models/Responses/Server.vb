Imports System.Net

Namespace API.Models.Responses
    Public Class Server
        Public Property SUBID As String
        Public Property os As String
        Public Property ram As String
        Public Property disk As String
        Public Property main_ip As String
        Public Property vcpu_count As String
        Public Property location As String
        Public Property DCID As String
        Public Property default_password As String
        Public Property date_created As String
        Public Property pending_charges As String
        Public Property status As String
        Public Property cost_per_month As String
        Public Property current_bandwidth_gb As Double
        Public Property allowed_bandwidth_gb As String
        Public Property netmask_v4 As String
        Public Property gateway_v4 As String
        Public Property power_status As String
        Public Property server_state As String
        Public Property VPSPLANID As String
        Public Property v6_main_ip As String
        Public Property v6_network_size As String
        Public Property v6_network As String
        Public Property v6_networks As V6Networks()
        Public Property label As String
        Public Property internal_ip As String
        Public Property kvm_url As String
        Public Property auto_backups As String
        Public Property tag As String
        Public Property OSID As String
        Public Property APPID As String
        Public Property FIREWALLGROUPID As String
    End Class

    Public Class BandWidth
        Public Property incoming_bytes As String()()
        Public Property outgoing_bytes As String()()
    End Class

    Public Structure BandwidthResult
        Public Property BandWidth As BandWidth
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class V6Networks
        Public Property v6_network As String
        Public Property v6_main_ip As String
        Public Property v6_network_size As String
    End Class

    Public Structure ServerResult
        Public Property Servers As Dictionary(Of String, Server)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace

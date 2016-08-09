<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BieuDoHoatDong.aspx.cs" Inherits="Extention.REPORT.Web.BieuDoHoatDong" %>



<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="include/jquery-3.1.0.min.js"></script>
    <link rel="stylesheet" href="include/wickedpicker.min.css">
    <script type="text/javascript" src="include/wickedpicker.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 229px;
            height: 240px;
        }
        .auto-style3 {
            width: 228px;
            height: 240px;
        }
        .auto-style4 {
            width: 38px;
            height: 240px;
        }
        .auto-style5 {
            height: 240px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="DivBieuDo">
   
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Extention/REPORT/Web/BaoCaoDoanhThu.aspx">Báo cáo doanh thu</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink2" runat="server" style="font-weight: 700">Biểu đồ hoạt động</asp:HyperLink>
&nbsp;<table style="width:100%;">
            <tr>
                <td class="auto-style1">Ngày bắt đầu<asp:Calendar ID="StartDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" SelectedDate="10/05/2015 16:49:38" Width="220px">
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
        <asp:TextBox ID="txtStartTimeOfStartDate" runat="server" CssClass="TimePickerStart" Width="215px">00:00:00</asp:TextBox>



                </td>
                <td class="auto-style4">&gt;&gt;&gt;&gt;</td>
                <td class="auto-style3">Ngày kết thúc<asp:Calendar ID="EndDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" SelectedDate="10/05/2015 16:49:44" Width="220px">
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
        <asp:TextBox ID="txtEndTimeOfEndDate" runat="server" CssClass="TimePickerEnd" Width="215px">23:59:59</asp:TextBox>
                </td>
                <td class="auto-style4">&gt;&gt;&gt;&gt;</td>
                <td class="auto-style5">
                    <asp:CheckBox ID="chkSelectTKTM" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectTKTM_CheckedChanged" Text="Chọn tất thống kê theo máy" />
                    <br />
        &nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk1" runat="server" Text="(1) Biểu đồ thống kê thời gian chơi các máy" />
                    <br />
        &nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk2" runat="server" Text="(2) Biểu đồ thống kê số máy sử dụng theo h/ngày" />
                    <br />
                    <asp:CheckBox ID="chkTKTCN" runat="server" AutoPostBack="True" OnCheckedChanged="chkTKTCN_CheckedChanged" Text="Chọn tất thống kê theo chuỗi ngày" />
                    <br />
        &nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk3" runat="server" Text="(3) Biểu đồ doanh thu tổng theo chuỗi ngày" />
                    <br />
        &nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk4" runat="server" Text="(4) Biểu đồ doanh thu máy theo chuỗi ngày" />
                    <br />
        &nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk5" runat="server" Text="(5) Biểu đồ doanh thu dịch vụ theo chuỗi ngày" />
                    <br />
&nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk6" runat="server" Text="(6) Biểu đồ giờ chơi theo chuỗi ngày" />
                    <br />
&nbsp;&nbsp;&nbsp; -<asp:CheckBox ID="chk7" runat="server" Text="(7) Biểu đồ tiền nạp miễn phí theo chuỗi ngày" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAddChart" runat="server" OnClick="btnAddChart_Click" Text="Thêm" />
        &nbsp;&nbsp;
        <asp:Button ID="btnDel" runat="server" OnClick="btnDel_Click" Text="Xóa" />
        &nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Nhập lại" />
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Width="510px"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Xem các biểu đồ" />
        <asp:Label ID="lblError" runat="server" Font-Italic="True" Font-Size="Large" ForeColor="#CC0000"></asp:Label>
        <br />
        <hr />
        <asp:Panel ID="pnBieuDo" runat="server">
            <asp:Label  ID="lblTemplate" runat="server" Width="100%" Font-Bold="False" Font-Size="Medium" Text="Biều đồ thời gian hoạt động của các máy" Visible="False"></asp:Label>
            <asp:Chart  ID="chartTemplate" runat="server" Width="727px" Visible="False">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
        
    </div>
    </form>
    <script>
        $('.TimePickerStart').wickedpicker({
            now: '<%  Response.Write(this.txtStartTimeOfStartDate.Text); %>', twentyFour: true, title:
                        'Thời gian', showSeconds: true
        });
        $('.TimePickerEnd').wickedpicker({
            now: '<% Response.Write(this.txtEndTimeOfEndDate.Text); %>', twentyFour: true, title:
                        'Thời gian', showSeconds: true
        });
    </script>
</body>
</html>

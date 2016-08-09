<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoCaoDoanhThu.aspx.cs" Inherits="Extention.REPORT.Web.BaoCaoDoanhThu" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="include/jquery-3.1.0.min.js"></script>
    <link rel="stylesheet" href="include/wickedpicker.min.css">
    <script type="text/javascript" src="include/wickedpicker.min.js"></script>

    <style type="text/css">
        .auto-style1 {
            width: 238px;
        }
        .auto-style2 {
            width: 52px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
    <div>
   
        <asp:HyperLink ID="HyperLink1" runat="server" style="font-weight: 700">Báo cáo doanh thu</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Extention/REPORT/Web/BieuDoHoatDong.aspx">Biểu đồ hoạt động</asp:HyperLink>
   
        <table style="width:100%;">
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
                <td class="auto-style2">&gt;&gt;&gt;&gt;</td>
                <td>Ngày kết thúc<asp:Calendar ID="EndDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" SelectedDate="10/05/2015 16:49:44" Width="220px">
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
            </tr>
        </table>
        &nbsp;<strong>Nhân viên:</strong>
        <asp:DropDownList ID="ddNhanVien" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:RadioButton ID="rbBaoCaoMayVaDichVu" runat="server" Checked="True" GroupName="LoaiBaoCao" Text="Báo cáo cả thu máy và dịch vụ" />
        <asp:RadioButton ID="rbChiThuMay" runat="server" GroupName="LoaiBaoCao" Text="Chỉ báo cáo thu máy" />
        <asp:RadioButton ID="rbChiThuDichVu" runat="server" GroupName="LoaiBaoCao" Text="Chỉ báo cáo thu dịch vụ" />
        <br />
        <asp:CheckBox ID="chkHienBaoCaoThuTheoMay" runat="server" Checked="True" Text="Hiện báo cáo thu theo máy (số tiền khách đã dùng). Chỉ hiệu lực khi chọn All trong mục nhân viên!" />
        <br />
        <asp:CheckBox ID="chkShowDetail" runat="server" Text="Hiển thị các bảng thống kê chi tiết" />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Xem báo cáo" />
        <asp:Label ID="lblError" runat="server" Font-Italic="True" Font-Size="Large" ForeColor="#CC0000"></asp:Label>
        <br />
        <hr />
        <strong>BÁO CÁO CHUNG</strong><asp:Panel ID="pnTienMay" runat="server">
            Tổng doanh thu nạp và bật máy:
            <asp:Label ID="lblTongNap" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnTienNapMienPhi" runat="server">
            Tổng nạp miễn phí:
            <asp:Label ID="lblTongNapMienPhi" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnTienDichVu" runat="server">
            Tổng doanh thu dịch vụ:
            <asp:Label ID="lblTongDichVu" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnTongTien" runat="server">
            Tổng doanh thu:
            <asp:Label ID="lblTongThu" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnTienTheoMay" runat="server">
            Tổng doanh thu thực dùng và bật máy:
            <asp:Label ID="lblTongDoanhThuTheoMay" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </asp:Panel>
   
        <br />
   
    </div>
        <br />
        <asp:Panel ID="pnlBaoCaoDoanhThuMay" runat="server">
            <div>
                <strong>CHI TIẾT TIỀN NẠP VÀ BẬT MÁY</strong></div>
            <asp:GridView ID="GvChiTietTienMay" runat="server">
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlBaoCaoNapMienPhi" runat="server">
            <div>
                <strong>CHI TIẾT TIỀN NẠP MIỄN PHÍ</strong></div>
            <asp:GridView ID="GvChiTietTienMienPhi" runat="server">
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlBaoCaoDoanhThuDichVu" runat="server">
            <strong>CHI TIẾT TIỀN DỊCH VỤ</strong><asp:GridView ID="GvChiTietDichVu" runat="server">
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlBaoCaoDoanhThuTheoMay" runat="server">
            <div>
                <strong>
                CHI TIẾT TIỀN THEO MÁY</strong></div>
            <asp:GridView ID="GvChiTietTienTheoMay" runat="server">
            </asp:GridView>
        </asp:Panel>
        <br />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockAppManager.aspx.cs" Inherits="Extention.BL.Web.BlockAppManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Mẫu tệp tin: <asp:FileUpload ID="fileApp" runat="server" /> 
        <br />
        Tên chương trình: <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnUpload" runat="server" Text="Cập nhật" OnClick="btnUpload_Click" />
        <asp:Label ID="lblThongBao" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <br />
        Danh sách các chương trình cấm:<br />
        <asp:GridView ID="GridView1" runat="server" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:CommandField DeleteText="Xóa" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

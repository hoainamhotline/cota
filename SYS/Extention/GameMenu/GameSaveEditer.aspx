<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameSaveEditer.aspx.cs" Inherits="Extention.DOC.Admin.GameSaveEditer" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="SaveManager.xml" >File XML chứa các thông tin saveGame lấy từ mạng</a>
        <asp:Panel ID="pnlEdit" Visible="false" runat="server">
            <asp:Label ID="Label1" runat="server" Text="Đang sửa Game "></asp:Label> ID:<asp:Label ID="lblid"  runat="server" Visible="true"></asp:Label>
            
             <br>
            Name:
            <asp:TextBox ID="txtName" runat="server" Width="529px"></asp:TextBox>
            <br>
            GroupName:
            <asp:TextBox ID="txtGroupName" runat="server" Width="529px"></asp:TextBox>
            <br>
            MainExe:
            <asp:TextBox ID="txtMainExe" runat="server" Width="529px"></asp:TextBox>
            <br>
            LocalPath:
            <asp:TextBox ID="txtLocalPath" runat="server" Width="529px"></asp:TextBox>
            <br>
            LocalArchivePath:
            <asp:TextBox ID="txtLocalArchivePath" runat="server" Width="529px"></asp:TextBox>
            <br>
            ProcessName:
            <asp:TextBox ID="txtProcessName" runat="server" Width="300px"></asp:TextBox>
            <br>
            <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="Cập nhập" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Thêm mới" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button4" runat="server" Text="Xóa trong CSDL" OnClick="Button4_Click" />
            <br>
            <CKEditor:CKEditorControl ID="CKEditor1" runat="server" BasePath="ckeditor" EnterMode="BR" Language="en" UIColor="#BFEE62"></CKEditor:CKEditorControl>
            <br></br>
            </br>
        </asp:Panel>
        <asp:GridView ID="GridView1" runat="server"  OnRowEditing="GridView1_RowEditing"   >
            <Columns>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>

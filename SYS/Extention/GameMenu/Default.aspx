<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Extention.DOC.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HP Game Menu - Nội Dung Game</title>
    <meta charset="utf-8"/>
    <script src="//connect.facebook.net/en_US/all.js"></script>
</head>
<body style="width:550px;min-height:600px;margin:0 auto;overflow-x:scroll;font-family:Arial">
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    </script>
    <form id="form1" runat="server">
        <h2 id="name" style="font-weight:bold;margin:5px 0;" runat="server">&nbsp;</h2>
        <div id="Description" style="width: 550px;margin-bottom:10px" runat="server">
            Game này chưa có nội dung giới thiệu.
            <br />Nếu thấy hay bạn hãy comment bằng facebook của mình để mọi người cùng chơi nhé.
        </div>
        <div class="fb-comments" id="comment" runat="server" style="width: 550px;" data-href="#" data-width="550" data-num-posts="10" data-order-by="time"></div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="plagGarism.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/StyleSheet.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://kit.fontawesome.com/8e1d3b8195.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <section>
                <div class="row">
                    <div class="col-lg-9 col-md-12">
                        <div class="plagarism-hero mt-5">
                            <center>
                                <h1>Palagarism Checker</h1>
                            </center>
                            <div class="plagarism-main p-3">
                                <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Rows="10" Columns="50" CssClass="form-control" placeholder="Please enter your text and press Check Plagiarism"></asp:TextBox>
                                <br />
                                <center>
                                    <asp:Button ID="btnCheck" runat="server" Text="Check for Plagiarism" OnClick="btnCheck_Click" CssClass="btn btn-success" /></center>
                                <asp:Repeater ID="rptResults" runat="server">
                                    <ItemTemplate>
                                        <div class="result-repeater">
                                            <p>
                                                <strong style="color: grey">Similarity:</strong> <%# Eval("Similarity") %>
                                                <p style="color: #2B89E2; line-break: anywhere;"><%# Eval("Link") %></p>
                                                <%# Eval("Snippet") %><br />

                                                <a href='<%# Eval("Link") %>' target="_blank" style="text-decoration: none; color: #3CCA7B; line-break: anywhere;"><%# Eval("Link") %>&nbsp;&nbsp;<span><i class="fa-solid fa-arrow-up-right-from-square"></i></span></a><br />
                                            </p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-12"></div>
                </div>

            </section>
        </div>


    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
</body>
</html>

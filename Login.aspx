<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SecureDataSharing.Login" MasterPageFile="~/MainSite.Master" %>
<asp:Content ID="ContentLogin" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
        <div class="registrationform">
            <div class="form-horizontal">
                <fieldset>
                    <legend>Login <i class="fa fa-pencil pull-right"></i></legend>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="UserName" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="UserName" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUserName"
                            SetFocusOnError="true" ErrorMessage="<b>Username is a required field.</b>" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Password" CssClass="col-lg-2 control-label"></asp:Label>
                        <div class="col-lg-10">
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember Me" />
                                </label>
                            </div>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            SetFocusOnError="true" ErrorMessage="<b>Password is a required field.</b>" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblMsg" runat="server" Text="" Font-Bold="true" ForeColor="Red" CssClass="col-lg-10 control-label"></asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-10 col-lg-offset-2">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>

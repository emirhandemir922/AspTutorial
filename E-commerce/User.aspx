<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="E_commerce.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
<div class="container bootdey flex-grow-1 container-p-y">
    <div class="media align-items-center py-3 mb-3">
        <div class="media-body ml-4">
            <asp:Label ID="Header" runat="server" Text=""></asp:Label>
            <h4 class="font-weight-bold mb-0" id="Header"><span class="text-muted font-weight-normal"></span></h4>
            <div class="text-muted mb-2" id="ID"></div>
                <a href="javascript:void(0)" class="btn btn-default btn-sm">Profile</a>&nbsp;
                <h6 id="User_id" runat="server" class="text text-info"></h6>
            </div>
        </div>

        <div class="card mb-4">
            <div class="card-body">
            <table class="table user-view-table m-0">
                <tbody>
                <tr>
                    <td>Verified:</td>
                    <td><span class="fa fa-check text-primary"></span>&nbsp; Yes</td>
                </tr>
                <tr>
                    <td>Role:</td>
                    <td id="Type">User</td>
                </tr>
                <tr>
                    <td>Status:</td>
                    <td><span class="badge badge-outline-success">Active</span></td>
                </tr>
                </tbody>
            </table>
            </div>
            <hr class="border-light m-0">
        </div>

        <div class="card">
            <div class="row no-gutters row-bordered">
            <div class="d-flex col-md align-items-center">
                <div class="text-muted small line-height-1">Posts</div>
                <div class="text-xlarge">125</div>
            </div>
            </div>
            <hr class="border-light m-0">
            <div class="card-body">

            <table class="table user-view-table m-0">
                <tbody>
                    <tr>
                        <td>Name:</td>
                        <td>
                            <input id="Name" runat="server" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>Sur-Name:</td>
                        <td >
                            <input id="SurName" runat="server" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>E-mail:</td>
                        <td>
                            <input id="Email" runat="server" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td>
                            <input id="Password" runat="server" type="text" />
                        </td>
                    </tr>
                </tbody>
            </table>

            <h6 class="mt-4 mb-3">Personal info</h6>

            <table class="table user-view-table m-0">
                <tbody>
                <tr>
                    <td>Birthday:</td>
                    <td>May 3, 1995</td>
                </tr>
                <tr>
                    <td>Country:</td>
                    <td>Turkey</td>
                </tr>
                <tr>
                    <td>Languages:</td>
                    <td>English</td>
                </tr>
                </tbody>
            </table>
            </div>
        </div>
            
        <hr />
        <asp:Button ID="Button1" class="btn btn-success mb-5" runat="server" Text="Save Changes" OnClick="Save_Click" />
        <br />
        <%--<asp:Button ID="AddProduct" class="btn btn-success mb-5" runat="server" Text="Add a new product |+|" OnClick="AddProduct_Click" />--%>
    </div>
</asp:Content>

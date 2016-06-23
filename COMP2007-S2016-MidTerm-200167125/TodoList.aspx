﻿<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200167125.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <a href="/TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Todo</a>

                <div>
                    <label for="PageSizeDropDownList">Records per page:</label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server" AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle" 
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>
                <asp:GridView ID="TodoGridview" runat="server" CssClass="table table-bordered table-striped table-hover" 
                    AutoGenerateColumns="false" DataKeyNames="TodoID" OnRowDeleting="TodoGridview_RowDeleting"
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="TodoGridview_PageIndexChanging"
                    AllowSorting="true" OnSorting="TodoGridview_Sorting" OnRowDataBound="TodoGridview_RowDataBound" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoName" HeaderText="Todo" Visible="true" SortExpression="TodoName"/>
                        <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true" SortExpression="TodoNotes"/>
                        <asp:BoundField DataField="Completed" HeaderText="Completed" Visible="true" SortExpression="Completed"/>
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/TodoDetails.aspx.cs"
                            runat="server" ControlStyle-CssClass="btn btn-primary btn-sm" DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}"/>
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link" 
                            ControlStyle-CssClass="btn btn-danger btn-sm delete" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

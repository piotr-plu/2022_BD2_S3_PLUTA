<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="status_wyciagow.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <p> Wyciąg: <br />
    <select name="slope">
		<option selected>A1</option>
		<option>A2</option>
        <option>A3</option>
        <option>B1</option>
        <option>B2</option>
        <option>B3</option>
        <option>B3</option>
        <option>C1</option>
        <option>C2</option>
        <option>C3</option>

    </select>
</p>
<p> Status:<br />
    <select name="status">
		<option selected>Otwarty</option>
		<option>Zamknięty</option>
    </select>
</p>

    </div>
                  
    <input type="submit" value="Zatwierdź">


</asp:Content>

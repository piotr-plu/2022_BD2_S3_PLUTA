<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="grupowanie_stoki.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
    <p> Grupa Wyciągów - Stok:</p>
    <p>
        <select name="group">
		<option selected>Zielony</option>
		<option>Niebieski</option>
        <option>Czerwony</option>
    </select>
    </p>

<p>Wyciągi wchodzące w skład grupy:</p>
<p>
<input type="checkbox" name="slope" value="A1">A1<br>
<input type="checkbox" name="slope" value="A2">A2<br>
<input type="checkbox" name="slope" value="A3">A3<br>
<input type="checkbox" name="slope" value="B1">B1<br>
<input type="checkbox" name="slope" value="B2">B2<br>
<input type="checkbox" name="slope" value="B3">B3<br>
<input type="checkbox" name="slope" value="B4">B4<br>
<input type="checkbox" name="slope" value="C1">C1<br>
<input type="checkbox" name="slope" value="C2">C2<br>
<input type="checkbox" name="slope" value="C3">C3<br>
</p>
        
	    <p><input type="submit" value="Zatwierdź"></p>

        </div>

</asp:Content>

<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Zarzad.Master" AutoEventWireup="true" CodeBehind="zmiana_cennika.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<h1>Zmiana Cenny</h1>
   <div>
<p> Karnet / Bilet: <br />
    <select name="type">
		<option selected>Karnet</option>
		<option>Bilet</option>
    </select>
</p>
<p> Okres:<br />
    <select name="period">
		<option selected>6h</option>
		<option>12h</option>
		<option>24h</option>
		<option>Przejazd</option>
    </select>
</p>
   <p>Cena: <br />
	   <input name="price" type="number" min="0.00" step="0.01">zł</p>

	    <p><input type="submit" value="Zatwierdź"></p>

    </div>
</asp:Content>

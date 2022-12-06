<%@ Page Title="Strona Główna" Language="C#" MasterPageFile="Kasa.Master" AutoEventWireup="true" CodeBehind="blokowanie_karnetu.aspx.cs" Inherits="narciarze._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <form>
	<p>Numer karnetu / biletu:</p>
	<input type="text" name="card_id" /><br />
	<p>Okres zablokowania:</p>
	Od: <input type="datetime-local" name="start_date" ><br />
	Do: <input type="datetime-local" name="end_date" >
	<input type="submit" name="submit" value="Zablokuj" >
</form>
</asp:Content>

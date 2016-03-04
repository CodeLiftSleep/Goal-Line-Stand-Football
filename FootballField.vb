
Option Explicit On
Option Strict On

Imports System


' a class containing an array of [blanks] representing the football field, and methods for accessing, 
' manipulating, and identifying that array.
Public Class FootballField
	' initializing field as an integer array as a placeholder
	' could be changed to hold players, strings, whatever
	dim field(120,53) As Integer
	
	Public Sub New()
		MyBase.New
	End Sub
End Class


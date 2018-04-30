README MRZVerifier & MRZFrontDesk v1.0
30 April 2018

===============
Requirements
===============
(a) .NET Framework 4.5 installed
(b) IIS 6.0 or Visual Studio.NET 2015 or higher


================
Instruction
================

Please follow below steps to setup the applications.

(1) 	If using IIS, setup the API & frontend application in IIS Manager pointing to "MRZVerifier" and "MRZFrontDesk" folder respectively. Provide necessary domain name for both.
  	For example - "http://service.cliffdepot.my" & "http://mrzapp.cliffdepot.my". Make sure the local machine host file for that domains are pointing to localhost.
(2)	If using VS.NET, just run both the application as usual.
(3)	In MRZFrontDesk, ensure the web.config "MRZVerifierEndpoint" key value is correct. This is use to enable the frontend application invoke the designated API service. 
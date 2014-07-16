Original code from: http://wsdlgenerator.codeplex.com/

A tool to generate a WSDL file from a c# DLL which contains one more Microsoft WebServices.
WSDLGenerator uses ServiceDescriptionReflector code to retrieve all information from an assembly (dll) to generate a wsdl file.

Modifications:
Original code changed to remove service namespace validation check that expects URLs ending with '/'.

Example:
WSDLGenerator.exe

Usage:
-i, --input ... Input assembly or file which contains the WebServices
-o, --outputfolder ... Output directory
-w, --wsdl {"[optional] ... Generate wsdl file
-s, --spwsdl [optional] ... Generate SharePoint compatible *wsdl.aspx file
-d, --spdisco [optional] ... Generate SharePoint compatible *disco.aspx file
-n, --servicename [optional] ... Specifies the fully qualified name of a service to be exported (when omitted, all services are exported)
-v, --verbose [optional] ... Verbose messages

SAMPLES:

1. WSDLGenerator.exe --input MyWebServices.dll --outputfolder ..\..\Temp --wsdl
2. WSDLGenerator.exe --input MyWebServices.dll --outputfolder ..\..\Temp --wsdl --spwsdl --spdisco
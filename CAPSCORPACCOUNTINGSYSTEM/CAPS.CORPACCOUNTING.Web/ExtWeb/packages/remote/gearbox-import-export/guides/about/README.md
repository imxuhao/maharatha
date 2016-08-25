#Import/Export Gear
## Introduction
Imagine having the possibility to import and export XLS(X) and CSV data directly in ExtJS applications. With no server code and only a handful of lines of client code. The Gearbox Import/Export Gear makes this possible. For an even easier way to import Excel using a wizard interface the Gearbox Import Wizard Gear is available.


### Why do you need this?

Most companies still use Excel to manipulate and disseminate data. Your clients will expect the applications that you build for them to have excel export functionality at the very least, and preferably also a way to easily import data.

While ExtJS applications are great for working with complex and interrelated data, implementing this functionality is complicated and time consuming. A solution typically requires complex client and server code to set up the data structure, read the excel data, create the components, create and edit records and so on. Adding drag-and-drop functionality to the equation complicates matters further.

Implementing Excel importing and exporting requires:

* Creation of client file import functionality, including drag and drop file uploads
* Client upload of file to server
* Server parsing of XLS, XLSX or CSV formats
* Creating JSON data for the client to handle and returning it to the client
* Mapping of Excel data to store/model attributes and Grid columns
* Properly parsing/formatting the record attributes and their types
* Reading of Import data into local app
* Saving imported records to server or storage

Implementing the above in a properly architected and reliable way can take weeks to months of developer effort. Believe us... we've done it! And here we are only talking about Excel import... what about Excel or even PDF export?
Don't waste your precious development time, because the Gearbox Import/Export Gear handles all this for you. Just drop the package into your client application and you're set.


### So how does the Gearbox Import/Export Gear solve this?

The Gearbox Import/Export Gear consists of an import and export component. Both components operate completely client-side without any need for server code or external calls.  The import and export give the developer a complete data layer that is able to natively read and write Excel, CSV and PDF formats. Getting a store or grid to directly work from Excel data or export it's data to Excel, CSV or PDF formats takes just a handful lines of code. 
